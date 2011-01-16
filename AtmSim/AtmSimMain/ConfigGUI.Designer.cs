namespace AtmSim
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
            this.routingTab = new System.Windows.Forms.TabPage();
            this.routingPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.removeRoutingEntryButton = new System.Windows.Forms.Button();
            this.addRoutingEntryButton = new System.Windows.Forms.Button();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.okButton = new System.Windows.Forms.Button();
            this.configTextBox = new System.Windows.Forms.TextBox();
            this.configTree = new System.Windows.Forms.TreeView();
            this.generalGridTab = new System.Windows.Forms.TabPage();
            this.generalPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.configTabControl = new System.Windows.Forms.TabControl();
            this.menuStrip.SuspendLayout();
            this.routingTab.SuspendLayout();
            this.generalTab.SuspendLayout();
            this.generalGridTab.SuspendLayout();
            this.configTabControl.SuspendLayout();
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
            // routingTab
            // 
            this.routingTab.Controls.Add(this.routingPropertyGrid);
            this.routingTab.Controls.Add(this.removeRoutingEntryButton);
            this.routingTab.Controls.Add(this.addRoutingEntryButton);
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
            this.routingPropertyGrid.HelpVisible = false;
            this.routingPropertyGrid.Location = new System.Drawing.Point(6, 6);
            this.routingPropertyGrid.Name = "routingPropertyGrid";
            this.routingPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.routingPropertyGrid.Size = new System.Drawing.Size(240, 156);
            this.routingPropertyGrid.TabIndex = 3;
            this.routingPropertyGrid.ToolbarVisible = false;
            // 
            // removeRoutingEntryButton
            // 
            this.removeRoutingEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeRoutingEntryButton.Location = new System.Drawing.Point(52, 168);
            this.removeRoutingEntryButton.Name = "removeRoutingEntryButton";
            this.removeRoutingEntryButton.Size = new System.Drawing.Size(40, 23);
            this.removeRoutingEntryButton.TabIndex = 2;
            this.removeRoutingEntryButton.Text = "-";
            this.removeRoutingEntryButton.UseVisualStyleBackColor = true;
            this.removeRoutingEntryButton.Click += new System.EventHandler(this.removeRoutingEntryButton_Click);
            // 
            // addRoutingEntryButton
            // 
            this.addRoutingEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addRoutingEntryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addRoutingEntryButton.Location = new System.Drawing.Point(6, 168);
            this.addRoutingEntryButton.Name = "addRoutingEntryButton";
            this.addRoutingEntryButton.Size = new System.Drawing.Size(40, 23);
            this.addRoutingEntryButton.TabIndex = 1;
            this.addRoutingEntryButton.Text = "+";
            this.addRoutingEntryButton.UseVisualStyleBackColor = true;
            this.addRoutingEntryButton.Click += new System.EventHandler(this.addRoutingEntryButton_Click);
            // 
            // generalTab
            // 
            this.generalTab.Controls.Add(this.okButton);
            this.generalTab.Controls.Add(this.configTextBox);
            this.generalTab.Controls.Add(this.configTree);
            this.generalTab.Location = new System.Drawing.Point(4, 22);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(252, 197);
            this.generalTab.TabIndex = 2;
            this.generalTab.Text = "Ogólne";
            this.generalTab.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(195, 171);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(51, 20);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // configTextBox
            // 
            this.configTextBox.Location = new System.Drawing.Point(6, 171);
            this.configTextBox.Name = "configTextBox";
            this.configTextBox.Size = new System.Drawing.Size(183, 20);
            this.configTextBox.TabIndex = 1;
            // 
            // configTree
            // 
            this.configTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.configTree.Location = new System.Drawing.Point(6, 6);
            this.configTree.Name = "configTree";
            this.configTree.Size = new System.Drawing.Size(240, 159);
            this.configTree.TabIndex = 0;
            this.configTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.configTree_AfterSelect);
            // 
            // generalGridTab
            // 
            this.generalGridTab.Controls.Add(this.generalPropertyGrid);
            this.generalGridTab.Location = new System.Drawing.Point(4, 22);
            this.generalGridTab.Name = "generalGridTab";
            this.generalGridTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalGridTab.Size = new System.Drawing.Size(252, 197);
            this.generalGridTab.TabIndex = 0;
            this.generalGridTab.Text = "Ogólne";
            this.generalGridTab.UseVisualStyleBackColor = true;
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
            this.configTabControl.Controls.Add(this.generalGridTab);
            this.configTabControl.Location = new System.Drawing.Point(12, 27);
            this.configTabControl.Name = "configTabControl";
            this.configTabControl.SelectedIndex = 0;
            this.configTabControl.Size = new System.Drawing.Size(260, 223);
            this.configTabControl.TabIndex = 2;
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
            this.routingTab.ResumeLayout(false);
            this.generalTab.ResumeLayout(false);
            this.generalTab.PerformLayout();
            this.generalGridTab.ResumeLayout(false);
            this.configTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.TabPage routingTab;
        private System.Windows.Forms.PropertyGrid routingPropertyGrid;
        private System.Windows.Forms.Button removeRoutingEntryButton;
        private System.Windows.Forms.Button addRoutingEntryButton;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox configTextBox;
        private System.Windows.Forms.TreeView configTree;
        private System.Windows.Forms.TabPage generalGridTab;
        private System.Windows.Forms.PropertyGrid generalPropertyGrid;
        private System.Windows.Forms.TabControl configTabControl;
    }
}