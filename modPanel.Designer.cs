namespace YKW1S_Mod_Tools
{
    partial class modPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(modPanel));
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bottomUIPanel = new System.Windows.Forms.Panel();
            this.drmButton = new System.Windows.Forms.Button();
            this.ImportBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.TitlePanel.SuspendLayout();
            this.bottomUIPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitlePanel
            // 
            this.TitlePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TitlePanel.Controls.Add(this.label1);
            this.TitlePanel.Location = new System.Drawing.Point(-8, -21);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(945, 109);
            this.TitlePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("FOT-Rowdy Std EB", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(226, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modding Panel";
            // 
            // bottomUIPanel
            // 
            this.bottomUIPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomUIPanel.Controls.Add(this.drmButton);
            this.bottomUIPanel.Controls.Add(this.ImportBtn);
            this.bottomUIPanel.Controls.Add(this.exitBtn);
            this.bottomUIPanel.Location = new System.Drawing.Point(-8, 717);
            this.bottomUIPanel.Name = "bottomUIPanel";
            this.bottomUIPanel.Size = new System.Drawing.Size(842, 66);
            this.bottomUIPanel.TabIndex = 1;
            // 
            // drmButton
            // 
            this.drmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drmButton.Location = new System.Drawing.Point(345, 3);
            this.drmButton.Name = "drmButton";
            this.drmButton.Size = new System.Drawing.Size(169, 32);
            this.drmButton.TabIndex = 2;
            this.drmButton.Text = "DRM MODE (OFF)";
            this.drmButton.UseVisualStyleBackColor = true;
            this.drmButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImportBtn
            // 
            this.ImportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportBtn.Location = new System.Drawing.Point(520, 3);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(169, 32);
            this.ImportBtn.TabIndex = 1;
            this.ImportBtn.Text = "Import Mod (*.ykm)";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.Location = new System.Drawing.Point(695, 3);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(135, 32);
            this.exitBtn.TabIndex = 0;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // modPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 756);
            this.Controls.Add(this.bottomUIPanel);
            this.Controls.Add(this.TitlePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(845, 803);
            this.MinimumSize = new System.Drawing.Size(845, 803);
            this.Name = "modPanel";
            this.Text = "Mod Panel";
            this.Load += new System.EventHandler(this.modPanel_Load);
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.bottomUIPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel bottomUIPanel;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.Button drmButton;
    }
}