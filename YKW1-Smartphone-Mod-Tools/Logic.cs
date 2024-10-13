
using ImGui.Forms.Controls;
using ImGui.Forms.Modals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YKW1_Smartphone_Mod_Tools
{

    public static class Logic
    {
        public static string fileLocation;
        public static string fileName;

        //Functions
        public static void SetFileLocation(string location)
        {
            MainForm.fileLocationLabel.Text = $"   File Location: {location}";
            fileLocation = location;
            fileName = Path.GetFileName(location);
        }

        public static async Task DecompileFileAsync(string apkPath, string outputPath, int phase)
        {
            try
            {
                await Task.Run(() => ExecuteCommand($"apktool.bat d -f \"{apkPath}\" -o \"{outputPath}\"", true, phase));
            }
            catch (Exception ex)
            {
                await MessageBox.ShowErrorAsync($"Failed to decompile {apkPath}: {ex.Message}", "Error");
            }
        }

        public static async Task BuildApkAsync(string decompiledPath, string outputApkPath, int phase)
        {
            try
            {
                await Task.Run(() => ExecuteCommand($"apktool.bat b \"{decompiledPath}\"", true, phase));
                File.Copy(Path.Combine(decompiledPath, "dist", Path.GetFileName(outputApkPath)), outputApkPath, true);
            }
            catch (Exception ex)
            {
                await MessageBox.ShowErrorAsync("Error", $"Failed to compile {decompiledPath}: {ex.Message}");
            }
        }

        public static async Task MergeAPKAsync()
        {
            try
            {
                DirectoryUtils.DeleteFileIfExists("Build_merged.apk");
                await Task.Run(() => ExecuteCommand(@$"java -jar APKEditor.jar m -i .\Build", true, 5));
            }
            catch (Exception ex)
            {
                await MessageBox.ShowErrorAsync("Error", $"Failed to merge: {ex.Message}");
            }
        }

        public static async Task SignAPKAsync()
        {
            try
            {
                await Task.Run(() => ExecuteCommand($@"java -jar uber-apk-signer.jar  --apks .\Build_merged.apk", true, 6));
            }
            catch (Exception ex)
            {
                await MessageBox.ShowErrorAsync("Error", $"Failed to merge: {ex.Message}");
            }
        }

        public static void ExecuteCommand(string command, bool progress, int type)
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
                            UpdateProgress(type, args.Data);
                        }
                    }
                };


                // Event handler for standard error
                process.ErrorDataReceived += async (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data) && !args.Data.Contains("Could not find sources"))
                    {
                        if (!(command.Contains("java -jar")))
                        {
                            Console.WriteLine("Error:");
                            Console.WriteLine(args.Data);  // Print each line of error as it's received
                            await MessageBox.ShowErrorAsync("Error Occured", $"Error: {args.Data}");
                        }
                        else
                        {
                            Console.WriteLine(args.Data);
                            UpdateProgress(type, args.Data);
                        }
                    }
                };

                process.Start();

                // Begin asynchronous reading
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
        }

        public static string ExecuteADBCommand(string command)
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

        private static Dictionary<string, int> progressMap;
        public static void UpdateProgress(int phase, string output)
        {
            if (phase == 1)
            {
                progressMap = ApkToolsDictionary._progressMapType1;
            }
            else if (phase == 2)
            {
                progressMap = ApkToolsDictionary._progressMapType1Split;
            }
            else if (phase == 3)
            {
                progressMap = ApkToolsDictionary._progressMapType2;
            }
            else if (phase == 4)
            {
                progressMap = ApkToolsDictionary._progressMapType2Split;
            }
            else if (phase == 5)
            {
                progressMap = APKEditorDictionary._progressMapType5;
            }

            else if (phase == 6)
            {
                progressMap = UberSignerDictionary._progressMapType6;
            }

            foreach (var entry in progressMap)
            {
                if (output.Contains(entry.Key))
                {
                    var progress = entry.Value;
                    setProgressBar(progress);
                    
                    break; // Exit after setting progress for the first match
                }
            }
        }

        public static void setProgressBar(int progress)
        {
            MainForm.progressBar.Value = progress;
        }


        //Booleans
        public static bool isImported()
        {
            return fileLocation != null;
        }

        public static bool isDecompiled()
        {
            return Directory.Exists(@"Decompiled\base") && Directory.Exists(@"Decompiled\split_asset_pack_install_time");
        }

        public static bool apkToolsFound()
        {
            var FileRequiredList = new List<string> { "APKEditor.jar", "apktool.bat", "apktool_2.9.3.jar", "certificate.pem", "certificate.pem", "key.pk8", "uber-apk-signer.jar" };

            foreach (var file in FileRequiredList)
            {
                if (!File.Exists($@"{file}"))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsADBConnected()
        {
            try
            {
                // Create a new process to run the ADB command
                var process = new Process();
                process.StartInfo.FileName = "adb";
                process.StartInfo.Arguments = "devices";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                // Start the process
                process.Start();

                // Read the output
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Check if there are any connected devices
                return output.Contains("device");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool processInTransit()
        {
            return MainForm.progressBar.Value != 100 && MainForm.progressBar.Value != 0;
        }
    }
}
