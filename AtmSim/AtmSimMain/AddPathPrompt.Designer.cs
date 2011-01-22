namespace AtmSim
{
    partial class AddPathPrompt
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
            this.srcComboBox = new System.Windows.Forms.ComboBox();
            this.trgComboBox = new System.Windows.Forms.ComboBox();
            this.capNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.findButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.capLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.capNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // srcComboBox
            // 
            this.srcComboBox.FormattingEnabled = true;
            this.srcComboBox.Location = new System.Drawing.Point(53, 12);
            this.srcComboBox.Name = "srcComboBox";
            this.srcComboBox.Size = new System.Drawing.Size(78, 21);
            this.srcComboBox.TabIndex = 0;
            this.srcComboBox.SelectedIndexChanged += new System.EventHandler(this.srcComboBox_SelectedIndexChanged);
            // 
            // trgComboBox
            // 
            this.trgComboBox.FormattingEnabled = true;
            this.trgComboBox.Location = new System.Drawing.Point(53, 39);
            this.trgComboBox.Name = "trgComboBox";
            this.trgComboBox.Size = new System.Drawing.Size(78, 21);
            this.trgComboBox.TabIndex = 1;
            this.trgComboBox.SelectedIndexChanged += new System.EventHandler(this.trgComboBox_SelectedIndexChanged);
            // 
            // capNumericUpDown
            // 
            this.capNumericUpDown.Location = new System.Drawing.Point(53, 66);
            this.capNumericUpDown.Name = "capNumericUpDown";
            this.capNumericUpDown.Size = new System.Drawing.Size(78, 20);
            this.capNumericUpDown.TabIndex = 2;
            this.capNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // findButton
            // 
            this.findButton.Enabled = false;
            this.findButton.Location = new System.Drawing.Point(137, 12);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 3;
            this.findButton.Text = "Znajdź";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(137, 63);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Zestaw";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(12, 15);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(17, 13);
            this.fromLabel.TabIndex = 5;
            this.fromLabel.Text = "Z:";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(12, 42);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(24, 13);
            this.toLabel.TabIndex = 6;
            this.toLabel.Text = "Do:";
            // 
            // capLabel
            // 
            this.capLabel.AutoSize = true;
            this.capLabel.Location = new System.Drawing.Point(12, 68);
            this.capLabel.Name = "capLabel";
            this.capLabel.Size = new System.Drawing.Size(40, 13);
            this.capLabel.TabIndex = 7;
            this.capLabel.Text = "Przep.:";
            // 
            // resultLabel
            // 
            this.resultLabel.Location = new System.Drawing.Point(137, 38);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(75, 22);
            this.resultLabel.TabIndex = 8;
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AddPathPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 99);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.capLabel);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.capNumericUpDown);
            this.Controls.Add(this.trgComboBox);
            this.Controls.Add(this.srcComboBox);
            this.Name = "AddPathPrompt";
            this.Text = "Nowa ścieżka wirtualna";
            ((System.ComponentModel.ISupportInitialize)(this.capNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox srcComboBox;
        private System.Windows.Forms.ComboBox trgComboBox;
        private System.Windows.Forms.NumericUpDown capNumericUpDown;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Label capLabel;
        private System.Windows.Forms.Label resultLabel;
    }
}