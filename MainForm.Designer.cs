namespace YKW1S_Mod_Tools
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installADBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importViaADBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAppPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchForStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDecompilationFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportPanel = new System.Windows.Forms.Panel();
            this.decompilePanel = new System.Windows.Forms.Panel();
            this.importButton = new System.Windows.Forms.Button();
            this.fileLocation = new System.Windows.Forms.Label();
            this.fileLabel = new System.Windows.Forms.Label();
            this.ImportTitle = new System.Windows.Forms.Label();
            this.decompPanel = new System.Windows.Forms.Panel();
            this.decompileBtn = new System.Windows.Forms.Button();
            this.skipBtn = new System.Windows.Forms.Button();
            this.decompileTitle = new System.Windows.Forms.Label();
            this.moddingPanel = new System.Windows.Forms.Panel();
            this.openModPanelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buildBtn = new System.Windows.Forms.Button();
            this.mergeBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.signBtn = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.adbButton = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.ImportPanel.SuspendLayout();
            this.decompPanel.SuspendLayout();
            this.moddingPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(2, 553);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(426, 23);
            this.progressBar.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(431, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installADBToolStripMenuItem,
            this.importViaADBToolStripMenuItem,
            this.importAppPackageToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // installADBToolStripMenuItem
            // 
            this.installADBToolStripMenuItem.Name = "installADBToolStripMenuItem";
            this.installADBToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.installADBToolStripMenuItem.Text = "Install ADB";
            this.installADBToolStripMenuItem.Click += new System.EventHandler(this.installADBToolStripMenuItem_Click);
            // 
            // importViaADBToolStripMenuItem
            // 
            this.importViaADBToolStripMenuItem.Name = "importViaADBToolStripMenuItem";
            this.importViaADBToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.importViaADBToolStripMenuItem.Text = "Import via ADB";
            this.importViaADBToolStripMenuItem.Click += new System.EventHandler(this.importViaADBToolStripMenuItem_Click);
            // 
            // importAppPackageToolStripMenuItem
            // 
            this.importAppPackageToolStripMenuItem.Name = "importAppPackageToolStripMenuItem";
            this.importAppPackageToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.importAppPackageToolStripMenuItem.Text = "Import App Package";
            this.importAppPackageToolStripMenuItem.Click += new System.EventHandler(this.importAppPackageToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchForStringToolStripMenuItem,
            this.resetToPresetToolStripMenuItem,
            this.openDecompilationFolderToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 26);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // searchForStringToolStripMenuItem
            // 
            this.searchForStringToolStripMenuItem.Name = "searchForStringToolStripMenuItem";
            this.searchForStringToolStripMenuItem.Size = new System.Drawing.Size(277, 26);
            this.searchForStringToolStripMenuItem.Text = "Search For String";
            this.searchForStringToolStripMenuItem.Click += new System.EventHandler(this.searchForStringToolStripMenuItem_Click);
            // 
            // resetToPresetToolStripMenuItem
            // 
            this.resetToPresetToolStripMenuItem.Name = "resetToPresetToolStripMenuItem";
            this.resetToPresetToolStripMenuItem.Size = new System.Drawing.Size(277, 26);
            this.resetToPresetToolStripMenuItem.Text = "Reset To Preset";
            this.resetToPresetToolStripMenuItem.Click += new System.EventHandler(this.resetToPresetToolStripMenuItem_Click);
            // 
            // openDecompilationFolderToolStripMenuItem
            // 
            this.openDecompilationFolderToolStripMenuItem.Name = "openDecompilationFolderToolStripMenuItem";
            this.openDecompilationFolderToolStripMenuItem.Size = new System.Drawing.Size(277, 26);
            this.openDecompilationFolderToolStripMenuItem.Text = "Open Decompilation Folder";
            this.openDecompilationFolderToolStripMenuItem.Click += new System.EventHandler(this.openDecompilationFolderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(47, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ImportPanel
            // 
            this.ImportPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImportPanel.Controls.Add(this.decompilePanel);
            this.ImportPanel.Controls.Add(this.importButton);
            this.ImportPanel.Controls.Add(this.fileLocation);
            this.ImportPanel.Controls.Add(this.fileLabel);
            this.ImportPanel.Controls.Add(this.ImportTitle);
            this.ImportPanel.Location = new System.Drawing.Point(-7, -24);
            this.ImportPanel.Name = "ImportPanel";
            this.ImportPanel.Size = new System.Drawing.Size(444, 168);
            this.ImportPanel.TabIndex = 2;
            // 
            // decompilePanel
            // 
            this.decompilePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decompilePanel.Location = new System.Drawing.Point(-1, 173);
            this.decompilePanel.Name = "decompilePanel";
            this.decompilePanel.Size = new System.Drawing.Size(444, 142);
            this.decompilePanel.TabIndex = 3;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(278, 130);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(157, 33);
            this.importButton.TabIndex = 3;
            this.importButton.Text = "Import Package";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // fileLocation
            // 
            this.fileLocation.AutoEllipsis = true;
            this.fileLocation.AutoSize = true;
            this.fileLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLocation.Location = new System.Drawing.Point(56, 93);
            this.fileLocation.Name = "fileLocation";
            this.fileLocation.Size = new System.Drawing.Size(114, 20);
            this.fileLocation.TabIndex = 2;
            this.fileLocation.Text = "Not Seleceted";
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLabel.Location = new System.Drawing.Point(18, 93);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(41, 20);
            this.fileLabel.TabIndex = 1;
            this.fileLabel.Text = "File:";
            // 
            // ImportTitle
            // 
            this.ImportTitle.AutoSize = true;
            this.ImportTitle.Font = new System.Drawing.Font("FOT-Kurokane Std EB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ImportTitle.Location = new System.Drawing.Point(9, 56);
            this.ImportTitle.Name = "ImportTitle";
            this.ImportTitle.Size = new System.Drawing.Size(289, 35);
            this.ImportTitle.TabIndex = 0;
            this.ImportTitle.Text = "Import App Package";
            // 
            // decompPanel
            // 
            this.decompPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decompPanel.Controls.Add(this.decompileBtn);
            this.decompPanel.Controls.Add(this.skipBtn);
            this.decompPanel.Controls.Add(this.decompileTitle);
            this.decompPanel.Location = new System.Drawing.Point(-7, 142);
            this.decompPanel.Name = "decompPanel";
            this.decompPanel.Size = new System.Drawing.Size(444, 128);
            this.decompPanel.TabIndex = 3;
            // 
            // decompileBtn
            // 
            this.decompileBtn.Enabled = false;
            this.decompileBtn.Location = new System.Drawing.Point(115, 89);
            this.decompileBtn.Name = "decompileBtn";
            this.decompileBtn.Size = new System.Drawing.Size(157, 33);
            this.decompileBtn.TabIndex = 5;
            this.decompileBtn.Text = "Decompile";
            this.decompileBtn.UseVisualStyleBackColor = true;
            this.decompileBtn.Click += new System.EventHandler(this.decompileBtn_Click);
            // 
            // skipBtn
            // 
            this.skipBtn.Enabled = false;
            this.skipBtn.Location = new System.Drawing.Point(278, 89);
            this.skipBtn.Name = "skipBtn";
            this.skipBtn.Size = new System.Drawing.Size(157, 33);
            this.skipBtn.TabIndex = 4;
            this.skipBtn.Text = "Skip";
            this.skipBtn.UseVisualStyleBackColor = true;
            this.skipBtn.Click += new System.EventHandler(this.skipBtn_Click);
            // 
            // decompileTitle
            // 
            this.decompileTitle.AutoSize = true;
            this.decompileTitle.Font = new System.Drawing.Font("FOT-Kurokane Std EB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.decompileTitle.Location = new System.Drawing.Point(9, 5);
            this.decompileTitle.Name = "decompileTitle";
            this.decompileTitle.Size = new System.Drawing.Size(276, 35);
            this.decompileTitle.TabIndex = 4;
            this.decompileTitle.Text = "Decompile Package";
            // 
            // moddingPanel
            // 
            this.moddingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.moddingPanel.Controls.Add(this.openModPanelBtn);
            this.moddingPanel.Controls.Add(this.label1);
            this.moddingPanel.Location = new System.Drawing.Point(-7, 269);
            this.moddingPanel.Name = "moddingPanel";
            this.moddingPanel.Size = new System.Drawing.Size(444, 126);
            this.moddingPanel.TabIndex = 4;
            // 
            // openModPanelBtn
            // 
            this.openModPanelBtn.Enabled = false;
            this.openModPanelBtn.Location = new System.Drawing.Point(277, 88);
            this.openModPanelBtn.Name = "openModPanelBtn";
            this.openModPanelBtn.Size = new System.Drawing.Size(157, 33);
            this.openModPanelBtn.TabIndex = 7;
            this.openModPanelBtn.Text = "Open Modding Panel";
            this.openModPanelBtn.UseVisualStyleBackColor = true;
            this.openModPanelBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("FOT-Kurokane Std EB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(9, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 35);
            this.label1.TabIndex = 6;
            this.label1.Text = "Modding";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buildBtn);
            this.panel1.Controls.Add(this.mergeBtn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.signBtn);
            this.panel1.Location = new System.Drawing.Point(-7, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 150);
            this.panel1.TabIndex = 5;
            // 
            // buildBtn
            // 
            this.buildBtn.Enabled = false;
            this.buildBtn.Location = new System.Drawing.Point(8, 112);
            this.buildBtn.Name = "buildBtn";
            this.buildBtn.Size = new System.Drawing.Size(132, 33);
            this.buildBtn.TabIndex = 10;
            this.buildBtn.Text = "Build";
            this.buildBtn.UseVisualStyleBackColor = true;
            this.buildBtn.Click += new System.EventHandler(this.buildBtn_Click);
            // 
            // mergeBtn
            // 
            this.mergeBtn.Enabled = false;
            this.mergeBtn.Location = new System.Drawing.Point(146, 112);
            this.mergeBtn.Name = "mergeBtn";
            this.mergeBtn.Size = new System.Drawing.Size(152, 33);
            this.mergeBtn.TabIndex = 9;
            this.mergeBtn.Text = "Merge";
            this.mergeBtn.UseVisualStyleBackColor = true;
            this.mergeBtn.Click += new System.EventHandler(this.mergeBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("FOT-Kurokane Std EB", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(9, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 35);
            this.label2.TabIndex = 8;
            this.label2.Text = "Build Package";
            // 
            // signBtn
            // 
            this.signBtn.Enabled = false;
            this.signBtn.Location = new System.Drawing.Point(304, 112);
            this.signBtn.Name = "signBtn";
            this.signBtn.Size = new System.Drawing.Size(131, 33);
            this.signBtn.TabIndex = 8;
            this.signBtn.Text = "Sign";
            this.signBtn.UseVisualStyleBackColor = true;
            this.signBtn.Click += new System.EventHandler(this.signBtn_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel.Location = new System.Drawing.Point(354, 580);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(68, 20);
            this.progressLabel.TabIndex = 6;
            this.progressLabel.Text = "000/100";
            // 
            // adbButton
            // 
            this.adbButton.Enabled = false;
            this.adbButton.Location = new System.Drawing.Point(296, 603);
            this.adbButton.Name = "adbButton";
            this.adbButton.Size = new System.Drawing.Size(132, 33);
            this.adbButton.TabIndex = 11;
            this.adbButton.Text = "Send via ADB";
            this.adbButton.UseVisualStyleBackColor = true;
            this.adbButton.Click += new System.EventHandler(this.adbButton_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.Enabled = false;
            this.exportBtn.Location = new System.Drawing.Point(160, 603);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(132, 33);
            this.exportBtn.TabIndex = 12;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 639);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.adbButton);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.moddingPanel);
            this.Controls.Add(this.decompPanel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ImportPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(449, 686);
            this.MinimumSize = new System.Drawing.Size(449, 686);
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.Text = "YKW1 Smartphone Mod Tools";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ImportPanel.ResumeLayout(false);
            this.ImportPanel.PerformLayout();
            this.decompPanel.ResumeLayout(false);
            this.decompPanel.PerformLayout();
            this.moddingPanel.ResumeLayout(false);
            this.moddingPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel ImportPanel;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Label ImportTitle;
        private System.Windows.Forms.Label fileLocation;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Panel decompilePanel;
        private System.Windows.Forms.Panel decompPanel;
        private System.Windows.Forms.Label decompileTitle;
        private System.Windows.Forms.Button skipBtn;
        private System.Windows.Forms.Panel moddingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buildBtn;
        private System.Windows.Forms.Button mergeBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button signBtn;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Button adbButton;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Button decompileBtn;
        private System.Windows.Forms.ToolStripMenuItem installADBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importViaADBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAppPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDecompilationFolderToolStripMenuItem;
        private System.Windows.Forms.Button openModPanelBtn;
    }
}

