﻿using ImGui.Forms;
using ImGui.Forms.Modals;
using ImGui.Forms.Modals.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YKW1_Smartphone_Mod_Tools
{

    public static class ClickHandling
    {
        private static bool mode = true;

        //MainForm MainMenuBar
        public static async void installADB_Click()
        {
            Process[] processes = Process.GetProcessesByName("adb");
            if (processes.Length > 0)
            {
                DialogResult result = await MessageBox.ShowYesNoAsync("Error Occured", "ADB Running Cannot Install! \nWould you like to close adb?");
                if (result == DialogResult.Yes)
                {
                    foreach (Process adbProcess in processes)
                    {
                        try
                        {
                            adbProcess.Kill(); // Kill the ADB process
                            await adbProcess.WaitForExitAsync(); // Wait for the process to exit
                        }
                        catch (Exception ex)
                        {
                            await MessageBox.ShowErrorAsync("Error", $"Failed to close ADB: {ex.Message}");
                            return;
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }

            DirectoryUtils.DeleteDirectoryIfExists("platform-tools");

            string url = "https://dl.google.com/android/repository/platform-tools-latest-windows.zip";
            string zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), "platform-tools.zip");
            string extractPath = Directory.GetCurrentDirectory();
            using HttpClient client = new HttpClient();
            try
            {
                // the ZIP file
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    await using (FileStream fs = new FileStream(zipFilePath, FileMode.Create))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }

                // Extract the ZIP file
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
            }
            catch (Exception ex)
            {
                await MessageBox.ShowErrorAsync("Error Occured", $"An error occurred: {ex.Message}");
            }
            finally
            {
                // Cleanup: Delete the downloaded zip file
                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }
            }

            string directoryPath = @".\";
            string outputDirectory = Path.Combine(directoryPath, "platform-tools");

            await FileUtils.CopyAsync($@"{outputDirectory}\adb.exe", @"adb.exe");
            await FileUtils.CopyAsync($@"{outputDirectory}\AdbWinApi.dll", @"AdbWinApi.dll");
            await FileUtils.CopyAsync($@"{outputDirectory}\AdbWinUsbApi.dll", @"AdbWinUsbApi.dll");
            Directory.Delete(@"platform-tools", true);
            await MessageBox.ShowInformationAsync("ADB Installed Succesfully!", "ADB Installed!");
        }

        public static async void importPackageViaADB_Click()
        {
            try
            {
                if (!(Logic.IsADBConnected()))
                {
                    await Logic.ExecuteADBCommand("adb connect");
                }

                string apkPathsOutput = await Logic.ExecuteADBCommand("adb shell pm path jp.co.level5.yws1");

                if (string.IsNullOrWhiteSpace(apkPathsOutput))
                {
                    await MessageBox.ShowErrorAsync("Error Occured", "Unable to find APK paths. Make sure the package name is correct.");
                    return;
                }

                string[] apkPaths = apkPathsOutput.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                DirectoryUtils.CreateDirectoryIfNotExists("Unmodified APKS");
                DirectoryUtils.CreateDirectoryIfNotExists(@"Unmodified APKS\ykw1_unzipped");

                string localDirectory = @"Unmodified APKS\ykw1_unzipped";

                foreach (string apkPathLine in apkPaths)
                {
                    string apkPath = apkPathLine.Replace("package:", "").Trim();
                    string apkFileName = Path.GetFileName(apkPath);
                    string localFilePath = Path.Combine(localDirectory, apkFileName);
                    string commandPullApk = $@"adb pull {apkPath} "".\Unmodified APKS\ykw1_unzipped""";
                    Console.WriteLine(commandPullApk);

                    Console.WriteLine($"Pulled {apkFileName} to {localFilePath}");
                    await Logic.ExecuteCommand(commandPullApk, false, -1);
                }

                DirectoryUtils.DeleteFileIfExists(@"Unmodified APKS\ykw1.apks");
                await MessageBox.ShowInformationAsync("Compiling", "Compiling .APKS Files... please wait.");
                await Task.Run(() => ZipFile.CreateFromDirectory(@"Unmodified APKS\ykw1_unzipped", @"Unmodified APKS\ykw1.apks"));
                await MessageBox.ShowInformationAsync("ADB Pull Successful!", "Succesfully Pulled All APKS");
                var res = Path.Combine(Directory.GetCurrentDirectory(), @".\Unmodified APKS\ykw1.apks");
                Logic.SetFileLocation(res);


            }
            catch (Exception ex)
            {
                await MessageBox.ShowErrorAsync("Error Occured", $"Error occured when importing: {ex.Message}");
            }
        }

        //MainForm Buttons
        public static async void importPackageBtn_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileFilters =
                {
                    new FileFilter("APKS Package", "apks"),
                    new FileFilter("Zip Archive", "zip")
                }

            };
            var result = await openFileDialog.ShowAsync();

            if (result == DialogResult.Ok)
            {
                if (Path.GetExtension(openFileDialog.SelectedPath) == ".apks" || Path.GetExtension(openFileDialog.SelectedPath) == ".zip")
                {
                    var path = openFileDialog.SelectedPath;
                    Logic.SetFileLocation(path);
                }
                else
                {
                    await MessageBox.ShowErrorAsync("Error Occured", "file must be a .APKS or .ZIP");
                }
            }
        }

        public static async void decompilePackageBtn_ClickAsync()
        {
            if (Logic.isImported())
            {
                if (Logic.apkToolsFound())
                {
                    if (!(Logic.processInTransit()))
                    {
                        DirectoryUtils.CreateDirectoryIfNotExists(@"Unmodified APKS\");
                        await FileUtils.CopyAsync(Logic.fileLocation, @"Unmodified APKS\ykw1.zip");

                        Logic.setProgressBar(1);
                        DirectoryUtils.DeleteDirectoryIfExists(@"Unmodified APKS\ykw1_unzipped\");

                        await Task.Run(() => ZipFile.ExtractToDirectory(@"Unmodified APKS\ykw1.zip", @"Unmodified APKS\ykw1_unzipped\"));
                        Logic.setProgressBar(5);

                        await Logic.DecompileFileAsync(@"Unmodified APKS\ykw1_unzipped\base.apk", @"Decompiled\base", 1);
                        await Logic.DecompileFileAsync(@"Unmodified APKS\ykw1_unzipped\split_asset_pack_install_time.apk", @"Decompiled\split_asset_pack_install_time", 2);
                        await MessageBox.ShowInformationAsync("Task Finished", "Decompilation has completed succesfully!");
                    }
                    else
                    {
                        await MessageBox.ShowErrorAsync("Error Occured", "A task is already in process please wait until it is finished");
                    }
                }
                else
                {
                    await MessageBox.ShowErrorAsync("Error: File Not Found", "The Files Necessary For Compilation and Decompilation\nare missing. Try reinstalling.");
                }
            }
            else
            {
                await MessageBox.ShowErrorAsync("Error: File Not Found", "No file has been imported!");
            }
        }

        public static void modButton_Click()
        {

        }

        public static async void buildButton_Click()
        {
            if (Logic.isDecompiled())
            {
                if (Logic.apkToolsFound())
                {
                    if (!(Logic.processInTransit()))
                    {
                        DirectoryUtils.CreateDirectoryIfNotExists(@"Build\");
                        await Logic.BuildApkAsync(@"Decompiled\base", @"Build\base.apk", 3);
                        await Logic.BuildApkAsync(@"Decompiled\split_asset_pack_install_time", @"Build\split_asset_pack_install_time.apk", 4);
                        await DirectoryUtils.CopyFilesToBuild(@"Unmodified APKS\ykw1_unzipped\");
                        await MessageBox.ShowInformationAsync("Task Finished", "Compilation has completed succesfully!");
                    }
                    else
                    {
                        await MessageBox.ShowErrorAsync("Error Occured", "A task is already in process please wait until it is finished");
                    }
                }
                else
                {
                    await MessageBox.ShowErrorAsync("Error: File Not Found", "The Files Necessary For Compilation and Decompilation \n \are missing. Try reinstalling.");
                }
            }
            else
            {
                await MessageBox.ShowErrorAsync("Error", "Cannot Find Decompiled Files.");
            }
        }

        public static async Task mergeButton_ClickAsync()
        {
            if (Logic.apkToolsFound())
            {
                if (!(Logic.processInTransit()))
                {
                    await Logic.MergeAPKAsync();
                    await MessageBox.ShowInformationAsync("Task Finished", "Merging has completed succesfully!");
                }
                else
                {
                    await MessageBox.ShowErrorAsync("Error Occured", "A task is already in process please wait until it is finished");
                }
            }
            else
            {
                await MessageBox.ShowErrorAsync("Error: File Not Found", "The Files Necessary For Compilation and Decompilation \n \are missing. Try reinstalling.");
            }
        }

        public static async Task signButton_ClickAsync()
        {
            if (Logic.apkToolsFound())
            {
                if (!(Logic.processInTransit()))
                {
                    await Logic.SignAPKAsync();
                    await MessageBox.ShowInformationAsync("Task Finished", "Signing has completed succesfully!");
                }
                else
                {
                    await MessageBox.ShowErrorAsync("Error Occured", "A task is already in process please wait until it is finished");
                }
            }
            else
            {
                await MessageBox.ShowErrorAsync("Error: File Not Found", "The Files Necessary For Compilation and Decompilation \n \are missing. Try reinstalling.");
            }
        }

        public static async void exportButton_Click()
        {
            if (File.Exists("Build_merged-aligned-debugSigned.apk"))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                var result = await saveFileDialog.ShowAsync();

                if (result == DialogResult.Ok)
                {
                    var savePath = saveFileDialog.SelectedPath;
                    byte[] apkData = await File.ReadAllBytesAsync("Build_merged-aligned-debugSigned.apk");

                    try
                    {
                        await File.WriteAllBytesAsync(savePath, apkData);
                        await MessageBox.ShowInformationAsync("Success", $"APK file saved successfully at {savePath}");
                    }
                    catch (Exception ex)
                    {
                        await MessageBox.ShowInformationAsync("Error", $"An error occurred while saving the APK file: {ex.Message}");
                    }
                }
            }
            else
            {
                await MessageBox.ShowErrorAsync("Cannot find signed filed.");
            }
        }

        public static async void exportViaADB_Click()
        {
            if (Logic.IsADBConnected())
            {
                await Logic.ExecuteCommand("adb install Build_merged-aligned-debugSigned.apk", false, 6);
            }
            else
            {
                await MessageBox.ShowErrorAsync("ADB is not connected to a device");
            }
        }

        public static async void ResetAsync()
        {
            if (!(Logic.processInTransit()))
            {
                try
                {
                    DirectoryUtils.DeleteDirectoryIfExists("Unmodified APKS");
                    DirectoryUtils.DeleteDirectoryIfExists("Mods");
                    DirectoryUtils.DeleteFileIfExists("Build_merged.apk");
                    DirectoryUtils.DeleteFileIfExists("Build_merged-aligned-debugSigned.apk");
                    DirectoryUtils.DeleteFileIfExists("Build_merged-aligned-debugSigned.apk.idsig");
                }
                catch (Exception ex)
                {
                    await MessageBox.ShowInformationAsync("Error Occured", ex.Message);
                }
            }
            else
            {
                await MessageBox.ShowErrorAsync("Error Occured", "A task is already in process please wait until it is finished");
            }
        }

        public static void OpenDecompFolder()
        {
            if (Directory.Exists("Decompiled"))
            {
                Process.Start("explorer.exe", "Decompiled");
            }
        }
        public static async void ImportModBtn_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FileFilters =
                {
                    new FileFilter("Yo-Kai Mod", "ykm")
                }
            };

            var result = await openFileDialog.ShowAsync();

            if (result == DialogResult.Ok)
            {
                DirectoryUtils.CreateDirectoryIfNotExists("Mods");
                await FileUtils.CopyAsync(openFileDialog.SelectedPath, @$"Mods\{Path.GetFileName(openFileDialog.SelectedPath)}");
            }
        }

        public async static void InjectModBtn_Click()
        {
            DirectoryUtils.CreateDirectoryIfNotExists("Mods");
            string[] files = Directory.GetFiles(@"Mods\");
            var ykmFiles = files.Where(file => Path.GetExtension(file).Equals(".ykm", StringComparison.OrdinalIgnoreCase));

            if (!ykmFiles.Any())
            {
                await MessageBox.ShowErrorAsync("No Mods Found", "No Mod Files Found.");
                return;
            }

            string outputDirectory = Path.Combine("Mods", "UnzippedMods");
            DirectoryUtils.DeleteDirectoryIfExists(outputDirectory);
            Directory.CreateDirectory(outputDirectory);

            foreach (var ykmFile in ykmFiles)
            {
                try
                {
                    ZipFile.ExtractToDirectory(ykmFile, outputDirectory);
                }
                catch (Exception ex)
                {
                    await MessageBox.ShowErrorAsync("Error Occured", $"Error unzipping {ykmFile}: {ex.Message}");
                }
            }

            string[] modfiles = Directory.GetFiles(outputDirectory, "*", SearchOption.AllDirectories);

            foreach (var file in modfiles)
            {
                try
                {
                    string relativePath = file.Substring(outputDirectory.Length + 1);
                    string destinationPath = Path.Combine(@"Decompiled\split_asset_pack_install_time\", relativePath);

                    string? destinationSubdirectory = Path.GetDirectoryName(destinationPath);
                    if (destinationSubdirectory == null)
                    {
                        continue;
                    }

                    if (!Directory.Exists(destinationSubdirectory))
                    {
                        Directory.CreateDirectory(destinationSubdirectory);
                    }

                    await FileUtils.CopyAsync(file, destinationPath);
                }
                catch (Exception ex)
                {
                    await MessageBox.ShowErrorAsync("Error Occured", $"Error copying {file}: {ex.Message}");
                }
            }

            await MessageBox.ShowInformationAsync("Task Completed Succesfully!", "The mod has succesfully been injected");
        }

        public async static void ChangeMODE()
        {
            mode = !mode;
            if (mode)
                MainForm.modeOption.Text = "Mode Option: SPLIT_ASSET";
            else
                MainForm.modeOption.Text = "Mode Option: BASE";
        }
    }
}
