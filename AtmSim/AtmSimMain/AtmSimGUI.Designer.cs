namespace AtmSim
{
    partial class AtmSimGUI
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.netToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netTopologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementListBox = new System.Windows.Forms.ListBox();
            this.elementLabel = new System.Windows.Forms.Label();
            this.configButton = new System.Windows.Forms.Button();
            this.logButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netToolStripMenuItem,
            this.configToolStripMenuItem,
            this.logToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(284, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // netToolStripMenuItem
            // 
            this.netToolStripMenuItem.Checked = true;
            this.netToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.netToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netNewToolStripMenuItem,
            this.netTopologyToolStripMenuItem});
            this.netToolStripMenuItem.Name = "netToolStripMenuItem";
            this.netToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.netToolStripMenuItem.Text = "&Sieć";
            // 
            // netNewToolStripMenuItem
            // 
            this.netNewToolStripMenuItem.Name = "netNewToolStripMenuItem";
            this.netNewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.netNewToolStripMenuItem.Text = "&Nowa";
            this.netNewToolStripMenuItem.Click += new System.EventHandler(this.netNewToolStripMenuItem_Click);
            // 
            // netTopologyToolStripMenuItem
            // 
            this.netTopologyToolStripMenuItem.Name = "netTopologyToolStripMenuItem";
            this.netTopologyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.netTopologyToolStripMenuItem.Text = "&Topologia";
            this.netTopologyToolStripMenuItem.Click += new System.EventHandler(this.netTopologyToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.configToolStripMenuItem.Text = "&Konfiguracja";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logSaveToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.logToolStripMenuItem.Text = "&Logi";
            // 
            // logSaveToolStripMenuItem
            // 
            this.logSaveToolStripMenuItem.Name = "logSaveToolStripMenuItem";
            this.logSaveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.logSaveToolStripMenuItem.Text = "&Zapisz";
            // 
            // elementListBox
            // 
            this.elementListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.elementListBox.FormattingEnabled = true;
            this.elementListBox.Location = new System.Drawing.Point(12, 40);
            this.elementListBox.Name = "elementListBox";
            this.elementListBox.Size = new System.Drawing.Size(260, 225);
            this.elementListBox.TabIndex = 1;
            this.elementListBox.SelectedIndexChanged += new System.EventHandler(this.elementListBox_SelectedIndexChanged);
            // 
            // elementLabel
            // 
            this.elementLabel.AutoSize = true;
            this.elementLabel.Location = new System.Drawing.Point(12, 24);
            this.elementLabel.Name = "elementLabel";
            this.elementLabel.Size = new System.Drawing.Size(77, 13);
            this.elementLabel.TabIndex = 2;
            this.elementLabel.Text = "Elementy sieci:";
            // 
            // configButton
            // 
            this.configButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.configButton.Location = new System.Drawing.Point(12, 277);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(75, 23);
            this.configButton.TabIndex = 3;
            this.configButton.Text = "Konfiguracja";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // logButton
            // 
            this.logButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.logButton.Location = new System.Drawing.Point(93, 277);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(75, 23);
            this.logButton.TabIndex = 4;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(174, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "loggme!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshButton.Location = new System.Drawing.Point(255, 277);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(17, 23);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "r";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "\"pliki XML|*.xml|Wszystkie pliki|*.*\"";
            this.openFileDialog.Title = "Otwórz konfigurację";
            // 
            // AtmSimGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 312);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.elementLabel);
            this.Controls.Add(this.elementListBox);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AtmSimGUI";
            this.Text = "ATMsim";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem netToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netTopologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logSaveToolStripMenuItem;
        private System.Windows.Forms.ListBox elementListBox;
        private System.Windows.Forms.Label elementLabel;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

