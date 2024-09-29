using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YKW1S_Mod_Tools
{
    public partial class MainForm : Form
    {
        string filePath = " ";

        public MainForm()
        {
            InitializeComponent();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            ImportPackage();
        }

        private void decompileBtn_Click(object sender, EventArgs e)
        {
            DecompileAPKS();
        }

        private void skipBtn_Click(object sender, EventArgs e)
        {
            EnableModnBuild();
        }


        private void buildBtn_Click(object sender, EventArgs e)
        {
            BuildAPKS();
        }

        private void mergeBtn_Click(object sender, EventArgs e)
        {
            MergeAPKS();
        }

        private void signBtn_Click(object sender, EventArgs e)
        {
            SignAPK();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            ExportAPK();
        }

        private void installADBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstallADB();
        }
        private void importViaADBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importViaADBAsync();
        }
        private void importAppPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportPackage();
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAPK();
        }

        private void searchForStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForStrings searchForStrings = new SearchForStrings();

            searchForStrings.Show();
        }

        private void resetToPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (progressBar.Value == 0 || progressBar.Value == 100)
            {
                if (Directory.Exists(@".\Unmodified APKS\")) { Directory.Delete(@".\Unmodified APKS\", true); }
                if (Directory.Exists(@".\Mods\")) { Directory.Delete(@".\Mods\", true); }
                if (File.Exists(@".\merged_app.apk")) { File.Delete(@".\merged_app.apk"); }
                if (File.Exists(@".\merged_app-aligned-debugSigned.apk")) { File.Delete(@".\merged_app-aligned-debugSigned.apk"); }
                if (File.Exists(@".\merged_app-aligned-debugSigned.apk.idsig")) { File.Delete(@".\merged_app-aligned-debugSigned.apk.idsig"); }


                Process[] processes = Process.GetProcessesByName("adb");
                if (processes.Length > 0)
                {
                    foreach (Process adbProcess in processes)
                    {
                        try
                        {
                            adbProcess.Kill(); // Kill the ADB process
                            adbProcess.WaitForExit(); // Wait for the process to exit
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to close ADB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (File.Exists(@".\adb.exe")) { File.Delete(@".\adb.exe"); }
                if (File.Exists(@".\AdbWinApi.dll")) { File.Delete(@".\AdbWinApi.dll"); }
                if (File.Exists(@".\AdbWinUsbApi.dll")) { File.Delete(@".\AdbWinUsbApi.dll"); }
                MessageBox.Show("Succesfully Reset Everything.", "Reset Complete", MessageBoxButtons.OK);

            }
            else
            {
                MessageBox.Show("Currently In Middle Of Process Cannot Reset", "Error", MessageBoxButtons.OK);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ImportPackage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "APKS files (*.apks)|*.apks|ZIP files (*.zip)|*.zip";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                fileLocation.Text = filePath;
                //Setup Decompile Panel
                EnableDecompilePanel();
            }
            else
            {
                return;
            }
        }

        private async void DecompileAPKS()
        {
            skipBtn.Enabled = false;
            if (filePath == null) { return; }
            if (!Directory.Exists(@".\Unmodified APKS\ykw1.zip")) { Directory.CreateDirectory(@".\Unmodified APKS\"); }
            File.Copy(filePath, @".\Unmodified APKS\ykw1.zip", true);
            setProgressBar(1);
            if (Directory.Exists(@".\Unmodified APKS\ykw1_unzipped\")) { Directory.Delete(@".\Unmodified APKS\ykw1_unzipped\", true); }
            await Task.Run(() => ZipFile.ExtractToDirectory(@".\Unmodified APKS\ykw1.zip", @".\Unmodified APKS\ykw1_unzipped\"));
            setProgressBar(5);
            await Task.Run(() => ExecuteCommand(@"apktool.bat d -f "".\Unmodified APKS\ykw1_unzipped\base.apk"" -o "".\Decompiled\base""", true, 1, false));
            await Task.Run(() => ExecuteCommand(@"apktool.bat d -f "".\Unmodified APKS\ykw1_unzipped\split_asset_pack_install_time.apk"" -o "".\Decompiled\split_asset_pack_install_time""", true, 1, true));
            EnableModnBuild();
        }

        private async void BuildAPKS()
        {
            if (!Directory.Exists(@".\Build\")) { Directory.CreateDirectory(@".\Build\"); }
            await Task.Run(() => ExecuteCommand(@"apktool.bat b .\Decompiled\base", true, 2, false));
            await Task.Run(() => ExecuteCommand(@"apktool.bat b .\Decompiled\split_asset_pack_install_time", true, 2, true));
            File.Copy(@".\Decompiled\base\dist\base.apk", @".\Build\base.apk", true);
            File.Copy(@".\Decompiled\split_asset_pack_install_time\dist\split_asset_pack_install_time.apk", @".\Build\split_asset_pack_install_time.apk", true);
            File.Copy(@".\Unmodified APKS\ykw1_unzipped\icon.png", @".\Build\icon.png", true);
            File.Copy(@".\Unmodified APKS\ykw1_unzipped\split_config.arm64_v8a.apk", @".\Build\split_config.arm64_v8a.apk", true);
            File.Copy(@".\Unmodified APKS\ykw1_unzipped\split_config.en.apk", @".\Build\split_config.en.apk", true);
            File.Copy(@".\Unmodified APKS\ykw1_unzipped\split_config.ja.apk", @".\Build\split_config.ja.apk", true);
            File.Copy(@".\Unmodified APKS\ykw1_unzipped\split_config.xxhdpi.apk", @".\Build\split_config.xxhdpi.apk", true);
            mergeBtn.Enabled = true;
        }

        private async void MergeAPKS()
        {
            if (File.Exists(@".\merged_app.apk")) { File.Delete(@".\merged_app.apk"); }
            await Task.Run(() => Process.Start("cmd.exe", $@"/c cd {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)} && java -jar APKEditor.jar m -i "".\Build"" -o "".\merged_app.apk"""));
            signBtn.Enabled = true;
        }

        private async void SignAPK()
        {
            if (File.Exists(@".\merged_app-aligned-debugSigned.apk")) { File.Delete(@".\merged_app-aligned-debugSigned.apk"); }
            if (File.Exists(@".\merged_app-aligned-debugSigned.apk.idsig")) { File.Delete(@".\merged_app-aligned-debugSigned.apk.idsig"); }
            await Task.Run(() => Process.Start("cmd.exe", $@"/c cd {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)} && java -jar uber-apk-signer.jar --apks .\merged_app.apk && del .\merged_app.apk"));
            exportBtn.Enabled = true;
            if (ADBInstalled())
            {
                adbButton.Enabled = true;
            }
        }

        private void ExportAPK()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Android Package (*.apk)|*.apk"; // File types to save
            saveFileDialog.Title = "Save Package"; // Dialog title
            saveFileDialog.DefaultExt = "txt"; // Default file extension
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] apkData = null;

                if (File.Exists(@".\merged_app-aligned-debugSigned.apk"))
                {
                    apkData = File.ReadAllBytes(@".\merged_app-aligned-debugSigned.apk");

                    // Get the file path selected by the user
                    string apkPath = saveFileDialog.FileName;

                    try
                    {
                        // Save the byte[] apkData to the file path
                        File.WriteAllBytes(apkPath, apkData);

                        // Inform the user that the file has been saved
                        MessageBox.Show($"APK file saved successfully at {apkPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Show an error message if something goes wrong
                        MessageBox.Show($"An error occurred while saving the APK file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: No Signed File Found", "Unhandled Exception");
                }
            }
        }

        private async Task InstallADB()
        {
            Process[] processes = Process.GetProcessesByName("adb");
            if (processes.Length > 0) 
            {
                DialogResult result = MessageBox.Show("ADB Running Cannot Install! \nWould you like to close adb?", "Error Occured", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    foreach (Process adbProcess in processes)
                    {
                        try
                        {
                            adbProcess.Kill(); // Kill the ADB process
                            adbProcess.WaitForExit(); // Wait for the process to exit
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to close ADB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (Directory.Exists(@".\platform-tools")) { Directory.Delete(@".\platform-tools"); }

            string url = "https://dl.google.com/android/repository/platform-tools-latest-windows.zip";
            string zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), "platform-tools.zip");
            string extractPath = Directory.GetCurrentDirectory();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // the ZIP file
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();
                        using (FileStream fs = new FileStream(zipFilePath, FileMode.Create))
                        {
                            await response.Content.CopyToAsync(fs);
                        }
                    }

                    // Extract the ZIP file
                    ZipFile.ExtractToDirectory(zipFilePath, extractPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
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

                File.Copy($@"{outputDirectory}\adb.exe", @".\adb.exe", true);
                File.Copy($@"{outputDirectory}\AdbWinApi.dll", @".\AdbWinApi.dll", true);
                File.Copy($@"{outputDirectory}\AdbWinUsbApi.dll", @".\AdbWinUsbApi.dll", true);
                Directory.Delete(@".\platform-tools", true);
                MessageBox.Show("ADB Installed!");
                if (ADBInstalled() && exportBtn.Enabled)
                {
                    adbButton.Enabled = true;
                }
            }
        }

        private async Task importViaADBAsync()
        {
            try
            {
                string apkPathsOutput = ExecuteADBCommand("adb shell pm path jp.co.level5.yws1");

                if (string.IsNullOrWhiteSpace(apkPathsOutput))
                {
                    MessageBox.Show("Unable to find APK paths. Make sure the package name is correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] apkPaths = apkPathsOutput.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                // Create a directory to store pulled APK files locally
                if (!Directory.Exists(@".\Unmodified APKS\")) { Directory.CreateDirectory(@".\Unmodified APKS\"); }
                if (!Directory.Exists(@".\Unmodified APKS\ykw1_unzipped")) { Directory.CreateDirectory(@".\Unmodified APKS\ykw1_unzipped"); }
                string localDirectory = @".\Unmodified APKS\ykw1_unzipped";

                foreach (string apkPathLine in apkPaths)
                {
                    // Extract the APK path (removing 'package:' prefix)
                    string apkPath = apkPathLine.Replace("package:", "").Trim();
                    // Construct the local file path for saving the APK
                    string apkFileName = Path.GetFileName(apkPath); // Get just the file name (e.g., base.apk)
                    string localFilePath = Path.Combine(localDirectory, apkFileName);

                    // Step 3: Use adb to pull each APK file to the local machine
                    string commandPullApk = $@"adb pull {apkPath} "".\Unmodified APKS\ykw1_unzipped""";
                    //MessageBox.Show(commandPullApk);
                    Console.WriteLine(commandPullApk);
                    ;

                    // Optional: Provide feedback for each pulled file
                    Console.WriteLine($"Pulled {apkFileName} to {localFilePath}");
                    await Task.Run(() => ExecuteCommandWithWindow(commandPullApk));
                }

                if (File.Exists(@".\Unmodified APKS\ykw1.apks")) { await Task.Run(() => File.Delete(@".\Unmodified APKS\ykw1.apks")); }
                MessageBox.Show("Compiling .APKS Files... please wait.", "Compiling");
                await Task.Run(() => ZipFile.CreateFromDirectory(@".\Unmodified APKS\ykw1_unzipped\", @".\Unmodified APKS\ykw1.apks"));
                MessageBox.Show("Succesfully Pulled All APKS", "ADB Pull Successful!");
                filePath = Path.Combine(Directory.GetCurrentDirectory(), @".\Unmodified APKS\ykw1.apks");
                fileLocation.Text = Path.Combine(Directory.GetCurrentDirectory(), @".\Unmodified APKS\ykw1.apks").ToString();
                EnableDecompilePanel();
            }
            catch
            {

            }
        }

        private void EnableDecompilePanel()
        {
            decompileBtn.Enabled = true;
            if (Directory.Exists(@".\Decompiled\base\") && Directory.Exists(@".\Decompiled\split_asset_pack_install_time\")) { skipBtn.Enabled = true; }
        }

        private void EnableModnBuild()
        {
            if (!Directory.Exists(@".\Mods\")) { Directory.CreateDirectory(@".\Mods\"); }
            openModPanelBtn.Enabled = true;
            buildBtn.Enabled = true;
        }

        private void setProgressBar(int progress)
        {
            if (progressBar.InvokeRequired)
            {
                // We're on a background thread, so we need to use Invoke to update the UI thread
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Value = progress;
                    progressLabel.Text = progress.ToString() + "/100";
                }));
            }
            else
            {
                // We're on the UI thread, so we can update the control directly
                progressBar.Value = progress;
                progressLabel.Text = progress.ToString() + "/100";
            }
        }

        private void ExecuteCommandWithWindow(string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                process.WaitForExit();
            }
        }

        private void ExecuteCommand(string command, bool progress, int type, bool split_asset)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;

                // Event handler for standard output
                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        Console.WriteLine(args.Data);  // Print each line of output as it's received
                        if (progress)
                        {
                            if (type == 1)
                            {
                                if (!split_asset)
                                {
                                    if (args.Data.Contains("Using Apktool 2.9.3")) { setProgressBar(9); }
                                    else if (args.Data.Contains("Loading resource table...")) { setProgressBar(13); }
                                    else if (args.Data.Contains("Decoding file-resources")) { setProgressBar(17); }
                                    else if (args.Data.Contains("Loading resource table from file:")) { setProgressBar(19); }
                                    else if (args.Data.Contains("Decoding values */*")) { setProgressBar(22); }
                                    else if (args.Data.Contains("Decoding AndroidManifest.xml")) { setProgressBar(25); }
                                    else if (args.Data.Contains("Regular manifest package")) { setProgressBar(30); }
                                    else if (args.Data.Contains("Baksmaling classes.dex")) { setProgressBar(35); }
                                    else if (args.Data.Contains("Copying assets and libs")) { setProgressBar(39); }
                                    else if (args.Data.Contains("Copying unknown files")) { setProgressBar(45); }
                                    else if (args.Data.Contains("Copying original files")) { setProgressBar(51); }
                                }
                                else
                                {
                                    if (args.Data.Contains("Using Apktool 2.9.3")) { setProgressBar(51); }
                                    else if (args.Data.Contains("Loading resource table...")) { setProgressBar(55); }
                                    else if (args.Data.Contains("Decoding file-resources")) { setProgressBar(61); }
                                    else if (args.Data.Contains("Loading resource table from file:")) { setProgressBar(64); }
                                    else if (args.Data.Contains("Decoding values */*")) { setProgressBar(67); }
                                    else if (args.Data.Contains("Decoding AndroidManifest.xml")) { setProgressBar(70); }
                                    else if (args.Data.Contains("Regular manifest package")) { setProgressBar(74); }
                                    else if (args.Data.Contains("Baksmaling classes.dex")) { setProgressBar(79); }
                                    else if (args.Data.Contains("Copying assets and libs")) { setProgressBar(81); }
                                    else if (args.Data.Contains("Copying unknown files")) { setProgressBar(91); }
                                    else if (args.Data.Contains("Copying original files")) { setProgressBar(100); }
                                }
                            }
                            else if (type == 2)
                            {
                                if (!split_asset)
                                {
                                    if (args.Data.Contains("Using Apktool 2.9.3")) { setProgressBar(9); }
                                    else if (args.Data.Contains("Checking whether sources has changed")) { setProgressBar(13); }
                                    else if (args.Data.Contains("Smaling smali folder into classes.dex")) { setProgressBar(17); }
                                    else if (args.Data.Contains("Checking whether resources has changed")) { setProgressBar(19); }
                                    else if (args.Data.Contains("Building resources")) { setProgressBar(22); }
                                    else if (args.Data.Contains("Copying libs... (/kotlin)")) { setProgressBar(25); }
                                    else if (args.Data.Contains("Copying libs... (/META-INF/services)")) { setProgressBar(30); }
                                    else if (args.Data.Contains("Building apk file...")) { setProgressBar(35); }
                                    else if (args.Data.Contains("Copying unknown files/dir...")) { setProgressBar(39); }
                                    else if (args.Data.Contains("Built apk into:")) { setProgressBar(51); }
                                }
                                else
                                {
                                    if (args.Data.Contains("Using Apktool 2.9.3")) { setProgressBar(51); }
                                    else if (args.Data.Contains("Checking whether sources has changed")) { setProgressBar(55); }
                                    else if (args.Data.Contains("Smaling smali folder into classes.dex")) { setProgressBar(61); }
                                    else if (args.Data.Contains("Checking whether resources has changed")) { setProgressBar(64); }
                                    else if (args.Data.Contains("Building resources")) { setProgressBar(67); }
                                    else if (args.Data.Contains("Copying libs... (/kotlin)")) { setProgressBar(78); }
                                    else if (args.Data.Contains("Copying libs... (/META-INF/services)")) { setProgressBar(84); }
                                    else if (args.Data.Contains("Building apk file...")) { setProgressBar(87); }
                                    else if (args.Data.Contains("Copying unknown files/dir...")) { setProgressBar(91); }
                                    else if (args.Data.Contains("Built apk into:")) { setProgressBar(100); }
                                }
                            }
                        }
                    }
                };

                // Event handler for standard error
                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data) && !args.Data.Contains("Could not find sources") && type != 3)
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(args.Data);  // Print each line of error as it's received
                        MessageBox.Show($"Error: {args.Data}", "Error Occured");
                    }
                };

                process.Start();

                // Begin asynchronous reading
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
        }

        private string ExecuteADBCommand(string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                // Read the output
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }

                Console.WriteLine(output);
                return output;
            }
        }

        private void openDecompilationFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentDirectory = Directory.GetCurrentDirectory().ToString();
            if (Directory.Exists(@".\Decompiled"))
            {
                Process.Start("explorer.exe", Path.Combine(currentDirectory, "Decompiled"));
            }
            else
            {
                Process.Start("explorer.exe", currentDirectory);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modPanel modPanel = new modPanel();
            modPanel.Show();
        }

        private bool ADBInstalled()
        {
            return File.Exists(@".\adb.exe");
        }

        private void adbButton_Click(object sender, EventArgs e)
        {
            ExecuteCommandWithWindow(@"adb install .\merged_app-aligned-debugSigned.apk");
        }
    }
}
