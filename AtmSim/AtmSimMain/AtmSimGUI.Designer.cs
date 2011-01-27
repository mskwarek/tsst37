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
            this.configButton = new System.Windows.Forms.Button();
            this.logButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cmdButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.elementsTabPage = new System.Windows.Forms.TabPage();
            this.elementsListBox = new System.Windows.Forms.ListBox();
            this.linksTabPage = new System.Windows.Forms.TabPage();
            this.linksListBox = new System.Windows.Forms.ListBox();
            this.connectionsTabPage = new System.Windows.Forms.TabPage();
            this.connectionsListBox = new System.Windows.Forms.ListBox();
            this.pathsTabPage = new System.Windows.Forms.TabPage();
            this.pathsListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.elementsTabPage.SuspendLayout();
            this.linksTabPage.SuspendLayout();
            this.connectionsTabPage.SuspendLayout();
            this.pathsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netToolStripMenuItem});
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
            this.netNewToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.netNewToolStripMenuItem.Text = "&Nowa";
            this.netNewToolStripMenuItem.Click += new System.EventHandler(this.netNewToolStripMenuItem_Click);
            // 
            // netTopologyToolStripMenuItem
            // 
            this.netTopologyToolStripMenuItem.Name = "netTopologyToolStripMenuItem";
            this.netTopologyToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.netTopologyToolStripMenuItem.Text = "&Topologia";
            this.netTopologyToolStripMenuItem.Click += new System.EventHandler(this.netTopologyToolStripMenuItem_Click);
            // 
            // configButton
            // 
            this.configButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.configButton.Location = new System.Drawing.Point(12, 323);
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
            this.logButton.Location = new System.Drawing.Point(93, 323);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(35, 23);
            this.logButton.TabIndex = 4;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(212, 323);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(60, 23);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "Odśwież";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "\"pliki XML|*.xml|Wszystkie pliki|*.*\"";
            this.openFileDialog.Title = "Otwórz konfigurację";
            // 
            // cmdButton
            // 
            this.cmdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdButton.Location = new System.Drawing.Point(134, 323);
            this.cmdButton.Name = "cmdButton";
            this.cmdButton.Size = new System.Drawing.Size(35, 23);
            this.cmdButton.TabIndex = 8;
            this.cmdButton.Text = "cmd";
            this.cmdButton.UseVisualStyleBackColor = true;
            this.cmdButton.Click += new System.EventHandler(this.cmdButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.elementsTabPage);
            this.tabControl.Controls.Add(this.linksTabPage);
            this.tabControl.Controls.Add(this.connectionsTabPage);
            this.tabControl.Controls.Add(this.pathsTabPage);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(260, 290);
            this.tabControl.TabIndex = 9;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // elementsTabPage
            // 
            this.elementsTabPage.Controls.Add(this.elementsListBox);
            this.elementsTabPage.Location = new System.Drawing.Point(4, 22);
            this.elementsTabPage.Name = "elementsTabPage";
            this.elementsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.elementsTabPage.Size = new System.Drawing.Size(252, 264);
            this.elementsTabPage.TabIndex = 0;
            this.elementsTabPage.Text = "Elementy sieci";
            this.elementsTabPage.UseVisualStyleBackColor = true;
            // 
            // elementsListBox
            // 
            this.elementsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.elementsListBox.FormattingEnabled = true;
            this.elementsListBox.Location = new System.Drawing.Point(0, 0);
            this.elementsListBox.Name = "elementsListBox";
            this.elementsListBox.Size = new System.Drawing.Size(252, 264);
            this.elementsListBox.TabIndex = 1;
            this.elementsListBox.SelectedIndexChanged += new System.EventHandler(this.elementsListBox_SelectedIndexChanged);
            this.elementsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(elementsListBox_MouseDoubleClick);
            // 
            // linksTabPage
            // 
            this.linksTabPage.Controls.Add(this.linksListBox);
            this.linksTabPage.Location = new System.Drawing.Point(4, 22);
            this.linksTabPage.Name = "linksTabPage";
            this.linksTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.linksTabPage.Size = new System.Drawing.Size(252, 264);
            this.linksTabPage.TabIndex = 1;
            this.linksTabPage.Text = "Łącza";
            this.linksTabPage.UseVisualStyleBackColor = true;
            // 
            // linksListBox
            // 
            this.linksListBox.FormattingEnabled = true;
            this.linksListBox.Location = new System.Drawing.Point(0, 0);
            this.linksListBox.Name = "linksListBox";
            this.linksListBox.Size = new System.Drawing.Size(252, 264);
            this.linksListBox.TabIndex = 0;
            // 
            // connectionsTabPage
            // 
            this.connectionsTabPage.Controls.Add(this.connectionsListBox);
            this.connectionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.connectionsTabPage.Name = "connectionsTabPage";
            this.connectionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.connectionsTabPage.Size = new System.Drawing.Size(252, 264);
            this.connectionsTabPage.TabIndex = 2;
            this.connectionsTabPage.Text = "Połączenia";
            this.connectionsTabPage.UseVisualStyleBackColor = true;
            // 
            // connectionsListBox
            // 
            this.connectionsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionsListBox.FormattingEnabled = true;
            this.connectionsListBox.Location = new System.Drawing.Point(0, 0);
            this.connectionsListBox.Name = "connectionsListBox";
            this.connectionsListBox.Size = new System.Drawing.Size(252, 264);
            this.connectionsListBox.TabIndex = 0;
            this.connectionsListBox.SelectedIndexChanged += new System.EventHandler(this.connectionsListBox_SelectedIndexChanged);
            this.connectionsListBox.MouseDoubleClick +=new System.Windows.Forms.MouseEventHandler(connectionsListBox_MouseDoubleClick);
            // 
            // pathsTabPage
            // 
            this.pathsTabPage.Controls.Add(this.pathsListBox);
            this.pathsTabPage.Location = new System.Drawing.Point(4, 22);
            this.pathsTabPage.Name = "pathsTabPage";
            this.pathsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pathsTabPage.Size = new System.Drawing.Size(252, 264);
            this.pathsTabPage.TabIndex = 3;
            this.pathsTabPage.Text = "Ścieżki";
            this.pathsTabPage.UseVisualStyleBackColor = true;
            // 
            // pathsListBox
            // 
            this.pathsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathsListBox.FormattingEnabled = true;
            this.pathsListBox.Location = new System.Drawing.Point(0, 0);
            this.pathsListBox.Name = "pathsListBox";
            this.pathsListBox.Size = new System.Drawing.Size(252, 264);
            this.pathsListBox.TabIndex = 0;
            this.pathsListBox.SelectedIndexChanged += new System.EventHandler(this.pathsListBox_SelectedIndexChanged);
            this.pathsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(pathsListBox_MouseDoubleClick);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(175, 323);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(31, 23);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // AtmSimGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 358);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AtmSimGUI";
            this.Text = "ATMsim";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AtmSimGUI_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.elementsTabPage.ResumeLayout(false);
            this.linksTabPage.ResumeLayout(false);
            this.connectionsTabPage.ResumeLayout(false);
            this.pathsTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem netToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netTopologyToolStripMenuItem;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button cmdButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage elementsTabPage;
        private System.Windows.Forms.ListBox elementsListBox;
        private System.Windows.Forms.TabPage connectionsTabPage;
        private System.Windows.Forms.ListBox connectionsListBox;
        private System.Windows.Forms.TabPage pathsTabPage;
        private System.Windows.Forms.ListBox pathsListBox;
        private System.Windows.Forms.TabPage linksTabPage;
        private System.Windows.Forms.ListBox linksListBox;
        private System.Windows.Forms.Button addButton;
    }
}

