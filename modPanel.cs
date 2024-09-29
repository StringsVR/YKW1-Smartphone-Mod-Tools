using Newtonsoft.Json;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System;

namespace YKW1S_Mod_Tools
{
    public partial class modPanel : Form
    {
        private int ModAmount = 0;
        private Panel scrollablePanel; // Create a panel for scrolling
        public bool DRM_MODE = false;

        public modPanel()
        {
            InitializeComponent();
        }

        private void modPanel_Load(object sender, EventArgs e)
        {
            // Create the scrollable panel with appropriate size and AutoScroll enabled
            scrollablePanel = new Panel
            {
                AutoScroll = true,
                BackColor = Color.LightGray,
                Location = new Point(0, 0),
                Size = new Size(620, 600) // Adjust the size as needed (ensure it's wide enough for the content)
            };

            // Add the scrollable panel to the form
            Controls.Add(scrollablePanel);

            setModDescriptors();
        }

        private void setModDescriptors()
        {
            if (!Directory.Exists(@".\Mods\")) { Directory.CreateDirectory(@".\Mods\"); }
            string modDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Mods").ToString();
            string[] zipFiles = Directory.GetFiles(modDirectory, "*.ykm");

            if (zipFiles.Length == 0)
            {
                MessageBox.Show("No Mod files found in the Mod Folder");
                return;
            }

            foreach (string zipFilePath in zipFiles)
            {
                ModAmount += 1;
                MetaJson meta = ReadMetaJsonFromZip(zipFilePath, out Image iconImage);
                if (meta != null)
                {
                    int offsetfromTitle = 107;
                    int nextPanelMultiplier = 173;

                    // Create a Panel to hold the mod content (it ensures proper grouping)
                    Panel mdPanel = new Panel
                    {
                        AutoScroll = false,
                        BackColor = Color.White,
                        Visible = true,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new System.Drawing.Point(0, ((nextPanelMultiplier * ModAmount) - offsetfromTitle)),
                        Size = new System.Drawing.Size(700, 174),
                        Tag = ModAmount // Make sure width fits scrollablePanel
                    };

                    // Create PictureBox
                    PictureBox modPicture = new PictureBox
                    {
                        Location = new Point(15, 12),
                        Size = new Size(150, 150),
                        Image = iconImage, // Set the loaded icon image
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Tag = ModAmount// Ensure the image fits
                    };
                    mdPanel.Controls.Add(modPicture); // Add to mdPanel

                    // Create ModTitle Label
                    Label ModTitle = new Label
                    {
                        BackColor = Color.White,
                        Text = meta.Title,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 14, System.Drawing.FontStyle.Regular),
                        Location = new System.Drawing.Point(171, 12), // Adjusted vertical location
                        Size = new Size(400, 30),
                        Tag = ModAmount// Ensure size fits within mdPanel
                    };
                    mdPanel.Controls.Add(ModTitle);

                    // Create ModDescription Label
                    Label ModDescription = new Label
                    {
                        BackColor = Color.White,
                        Text = meta.Description,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 11, System.Drawing.FontStyle.Regular),
                        Location = new System.Drawing.Point(171, 52), // Adjusted vertical location
                        Size = new Size(400, 50),
                        Tag = ModAmount// Ensure size fits within mdPanel
                    };
                    mdPanel.Controls.Add(ModDescription);

                    // Create ModVersion Label
                    Label ModVersion = new Label
                    {
                        BackColor = Color.White,
                        Text = $@"Version: {meta.Version}",
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 11, System.Drawing.FontStyle.Regular),
                        Location = new System.Drawing.Point(171, 100), // Adjusted vertical location
                        Size = new Size(200, 25),
                        Tag = ModAmount
                    };
                    mdPanel.Controls.Add(ModVersion);

                    // Create ModVersion Label
                    Label modFile = new Label
                    {
                        BackColor = Color.White,
                        Text = Path.GetFileName(zipFilePath.ToString()),
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 11, System.Drawing.FontStyle.Regular),
                        Location = new System.Drawing.Point(171, 125), // Adjusted vertical location
                        Size = new Size(230, 25),
                        Tag = ModAmount
                    };
                    mdPanel.Controls.Add(modFile);

                    // Create ModVersion Label
                    Label modAuthor = new Label
                    {
                        BackColor = Color.White,
                        Text = $"Author: {meta.Author}",
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 11, System.Drawing.FontStyle.Regular),
                        Location = new System.Drawing.Point(380, 100), // Adjusted vertical location
                        Size = new Size(230, 25),
                        Tag = ModAmount
                    };
                    mdPanel.Controls.Add(modAuthor);

                    //Create Mod Injection Button
                    Button ModInjectionBtn = new Button
                    {
                        Location = new System.Drawing.Point(415, 140),
                        Size = new Size(85, 28),
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                        Text = "Inject",
                        Tag = ModAmount
                    };
                    mdPanel.Controls.Add(ModInjectionBtn);
                    ModInjectionBtn.Click += (sender, e) => ModInjectBtn_Click(sender, e, modFile.Text);

                    //Create Mod Delete Button
                    Button ModDeleteBtn = new Button
                    {
                        Location = new System.Drawing.Point(515, 140),
                        Size = new Size(85, 28),
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                        Text = "Delete",
                        Tag = ModAmount
                    };
                    mdPanel.Controls.Add(ModDeleteBtn);
                    ModDeleteBtn.Click += (sender, e) => ModDeleteBtn_Click(sender, e, modFile.Text);
                    scrollablePanel.Controls.Add(mdPanel);

                }
            }
        }

        private void ModInjectBtn_Click(object sender, EventArgs e, string ModFile)
        {
            InjectAPK(Path.Combine(Directory.GetCurrentDirectory(), @"Mods\", ModFile).ToString());
        }

        private void ModDeleteBtn_Click(object sender, EventArgs e, string ModFile)
        {
            DialogResult result = MessageBox.Show($"Would You Like To Delete {ModFile}?", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), @"Mods\" ,ModFile));
                scrollablePanel.Controls.Clear();
                ModAmount = 0; // Reset ModAmount

                // Re-fetch and set the mod descriptors
                setModDescriptors();
            }
        }

        private void InjectAPK(string filePath)
        {
            string directoryPath = @".\Mods\";
            string outputDirectory = Path.Combine(directoryPath, "UnzippedMods");

            try
            {
                // Create a separate folder for each unzipped file
                ZipFile.ExtractToDirectory(filePath, outputDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error unzipping {filePath}: {ex.Message}");
            }
            string[] modfiles = Directory.GetFiles(outputDirectory, "*", SearchOption.AllDirectories);
            File.Delete(@".\Mods\UnzippedMods\icon.png");
            File.Delete(@".\Mods\UnzippedMods\meta.json");
            // Copy each file to the destination directory
            foreach (var file in modfiles)
            {
                try
                {
                    // Get the relative path of the file from the source directory
                    string relativePath = file.Substring(outputDirectory.Length + 1);

                    // Construct the full destination path
                    string destinationPath = Path.Combine(@".\Decompiled\split_asset_pack_install_time\", relativePath);
                    string baseDestinationPath = Path.Combine(@".\Decompiled\base\", relativePath);

                    // Ensure the destination subdirectory exists
                    string destinationSubdirectory = Path.GetDirectoryName(destinationPath);
                    string baseDestinationSubdirectory = Path.GetDirectoryName(baseDestinationPath);
                    if (!Directory.Exists(destinationSubdirectory))
                    {
                        Directory.CreateDirectory(destinationSubdirectory);
                    }
                    if (!Directory.Exists(baseDestinationSubdirectory) && DRM_MODE)
                    {
                        Directory.CreateDirectory(baseDestinationPath);
                    }

                    // Copy the file to the destination directory
                    File.Copy(file, destinationPath, true);  // Overwrite if file exists
                    if (DRM_MODE) { File.Copy(file, baseDestinationPath, true); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying {file}: {ex.Message}");
                }
            }

            MessageBox.Show("Files copied successfully.");
        }

        private void ImportModFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Yo-Kai Watch Mod file (*.ykm)|*.ykm";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.Copy(openFileDialog.FileName, $@".\Mods\{Path.GetFileName(openFileDialog.FileName)}", true);
            }
        }

        private MetaJson ReadMetaJsonFromZip(string zipFilePath, out Image iconImage)
        {
            iconImage = null; // Initialize the output image variable
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    ZipArchiveEntry metaEntry = archive.GetEntry("meta.json");
                    ZipArchiveEntry iconEntry = archive.GetEntry("icon.png");

                    if (metaEntry != null)
                    {
                        using (StreamReader reader = new StreamReader(metaEntry.Open(), Encoding.UTF8))
                        {
                            string jsonContent = reader.ReadToEnd();
                            MetaJson metaData = JsonConvert.DeserializeObject<MetaJson>(jsonContent);

                            // If icon.png is found, load it into an Image object
                            if (iconEntry != null)
                            {
                                using (var stream = iconEntry.Open())
                                {
                                    iconImage = Image.FromStream(stream); // Load the icon image
                                }
                            }

                            return metaData; // Return the metadata
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading {zipFilePath}: {ex.Message}");
            }

            return null; // Return null if something goes wrong
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            ImportModFile();
            scrollablePanel.Controls.Clear();
            ModAmount = 0; // Reset ModAmount

            // Re-fetch and set the mod descriptors
            setModDescriptors();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DRM_MODE)
            {
                DRM_MODE = false;
                drmButton.Text = "DRM MODE (OFF)";
            }
            else
            {
                DRM_MODE = true;
                drmButton.Text = "DRM MODE (ON)";
            }
        }
    }

    public class MetaJson
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
    }
}
