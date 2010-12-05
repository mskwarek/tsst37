namespace ATMsim
{
    partial class ConfigGUI
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
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.configTabControl = new System.Windows.Forms.TabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.routingTab = new System.Windows.Forms.TabPage();
            this.routingPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuStrip.SuspendLayout();
            this.configTabControl.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.routingTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.restoreToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(284, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.saveToolStripMenuItem.Text = "&Zapisz";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.restoreToolStripMenuItem.Text = "&Przywróć";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // generalPropertyGrid
            // 
            this.generalPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.generalPropertyGrid.HelpVisible = false;
            this.generalPropertyGrid.Location = new System.Drawing.Point(6, 6);
            this.generalPropertyGrid.Name = "generalPropertyGrid";
            this.generalPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.generalPropertyGrid.Size = new System.Drawing.Size(240, 185);
            this.generalPropertyGrid.TabIndex = 1;
            this.generalPropertyGrid.ToolbarVisible = false;
            this.generalPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.generalPropertyGrid_PropertyValueChanged);
            // 
            // configTabControl
            // 
            this.configTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.configTabControl.Controls.Add(this.generalTab);
            this.configTabControl.Controls.Add(this.routingTab);
            this.configTabControl.Location = new System.Drawing.Point(12, 27);
            this.configTabControl.Name = "configTabControl";
            this.configTabControl.SelectedIndex = 0;
            this.configTabControl.Size = new System.Drawing.Size(260, 223);
            this.configTabControl.TabIndex = 2;
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.generalPropertyGrid);
            this.generalTab.Location = new System.Drawing.Point(4, 22);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(252, 197);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "Ogólne";
            this.generalTab.UseVisualStyleBackColor = true;
            // 
            // routingTab
            // 
            this.routingTab.Controls.Add(this.routingPropertyGrid);
            this.routingTab.Location = new System.Drawing.Point(4, 22);
            this.routingTab.Name = "routingTab";
            this.routingTab.Padding = new System.Windows.Forms.Padding(3);
            this.routingTab.Size = new System.Drawing.Size(252, 197);
            this.routingTab.TabIndex = 1;
            this.routingTab.Text = "Routing";
            this.routingTab.UseVisualStyleBackColor = true;
            // 
            // routingPropertyGrid
            // 
            this.routingPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.routingPropertyGrid.HelpVisible = false;
            this.routingPropertyGrid.Location = new System.Drawing.Point(6, 8);
            this.routingPropertyGrid.Name = "routingPropertyGrid";
            this.routingPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.routingPropertyGrid.Size = new System.Drawing.Size(240, 183);
            this.routingPropertyGrid.TabIndex = 0;
            this.routingPropertyGrid.ToolbarVisible = false;
            this.routingPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(routingPropertyGrid_PropertyValueChanged);
            // 
            // ConfigGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.configTabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ConfigGUI";
            this.Text = "Konfiguracja";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigGUI_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.configTabControl.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.routingTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid generalPropertyGrid;
        private System.Windows.Forms.TabControl configTabControl;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.TabPage routingTab;
        private System.Windows.Forms.PropertyGrid routingPropertyGrid;
    }
}