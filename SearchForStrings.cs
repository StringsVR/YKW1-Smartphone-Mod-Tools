using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YKW1S_Mod_Tools
{
    public partial class SearchForStrings : Form
    {
        private string output;
        public SearchForStrings()
        {
            InitializeComponent();
        }

        private void SearchForStrings_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"Decompiled\")))
            {
                locationTextBox.Text = Path.Combine(Directory.GetCurrentDirectory(), @"Decompiled\").ToString();
            }
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select directory to search";

                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedDirectory = folderBrowserDialog.SelectedPath;
                    locationTextBox.Text = selectedDirectory;
                }
            }
        }

        private void searchStringBtn_Click(object sender, EventArgs e)
        {
            SearchForString();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void SearchForString()
        {
            await Task.Run(() => output = ExecuteCommand($@"powershell -Command ""Get-ChildItem -Path '{locationTextBox.Text}' -Recurse -File | Select-String -Pattern '{stringTextBox.Text}'"""));
            MessageBox.Show(output, "Results");
            output = null;
        }

        private string ExecuteCommand(string command)
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
                    return "Process Was Either Closed Or Crashed";
                }

                Console.WriteLine(output);
                return output;
            }
        }
    }
}
