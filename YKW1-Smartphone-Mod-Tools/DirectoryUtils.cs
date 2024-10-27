using ImGui.Forms.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YKW1_Smartphone_Mod_Tools
{
    public static class DirectoryUtils
    {
        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void DeleteDirectoryIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static async Task CopyFilesToBuild(string sourceDir)
        {
            string[] filesToCopy = new[]
            {
                    "split_config.arm64_v8a.apk",
                    "split_config.en.apk",
                    "split_config.ja.apk",
                    "split_config.xxhdpi.apk"
            };

            foreach (var file in filesToCopy)
            {
                string combinedPath = Path.Combine(sourceDir, file);
                if (!File.Exists(combinedPath)) { await MessageBox.ShowErrorAsync($@"Warning: {file} not found."); return; }
                await FileUtils.CopyAsync(combinedPath, Path.Combine(@".\Build", file));
            }
        }

        public static async Task CopyFilesRecursively(string sourcePath, string destinationPath)
        {
            // Check if source directory exists
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException($"Source directory not found: {sourcePath}");
            }

            // Ensure the destination directory exists
            Directory.CreateDirectory(destinationPath);

            // Copy each file in the source directory
            foreach (var filePath in Directory.GetFiles(sourcePath))
            {
                string fileName = Path.GetFileName(filePath);
                string destinationFilePath = Path.Combine(destinationPath, fileName);

                // Copy file and overwrite if it exists
                await FileUtils.CopyAsync(filePath, destinationFilePath);
            }

            // Recursively copy each subdirectory in the source directory
            foreach (var directoryPath in Directory.GetDirectories(sourcePath))
            {
                string directoryName = Path.GetFileName(directoryPath);
                string destinationDirectoryPath = Path.Combine(destinationPath, directoryName);

                // Recursively call the function for each subdirectory
                await CopyFilesRecursively(directoryPath, destinationDirectoryPath);
            }
        }
    }
}
