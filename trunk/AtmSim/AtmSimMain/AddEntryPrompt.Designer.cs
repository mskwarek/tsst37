namespace AtmSim
{
    partial class AddEntryPrompt
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
            this.label1 = new System.Windows.Forms.Label();
            this.inPortTextBox = new System.Windows.Forms.TextBox();
            this.inVpiTextBox = new System.Windows.Forms.TextBox();
            this.inVciTextBox = new System.Windows.Forms.TextBox();
            this.outPortTextBox = new System.Windows.Forms.TextBox();
            this.outVpiTextBox = new System.Windows.Forms.TextBox();
            this.outVciTextBox = new System.Windows.Forms.TextBox();
            this.inPortLabel = new System.Windows.Forms.Label();
            this.inVpiLabel = new System.Windows.Forms.Label();
            this.inVciLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outPortLabel = new System.Windows.Forms.Label();
            this.outVpiLabel = new System.Windows.Forms.Label();
            this.outVciLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.routingInPanel = new System.Windows.Forms.Panel();
            this.routingOutPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.outConnectionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sinkPanel = new System.Windows.Forms.Panel();
            this.inConnectionTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.routerButton = new System.Windows.Forms.RadioButton();
            this.sourceButton = new System.Windows.Forms.RadioButton();
            this.sinkButton = new System.Windows.Forms.RadioButton();
            this.radioPanel = new System.Windows.Forms.Panel();
            this.routingInPanel.SuspendLayout();
            this.routingOutPanel.SuspendLayout();
            this.sourcePanel.SuspendLayout();
            this.sinkPanel.SuspendLayout();
            this.radioPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dane ramki przychodzącej:";
            // 
            // inPortTextBox
            // 
            this.inPortTextBox.Location = new System.Drawing.Point(3, 32);
            this.inPortTextBox.Name = "inPortTextBox";
            this.inPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.inPortTextBox.TabIndex = 1;
            // 
            // inVpiTextBox
            // 
            this.inVpiTextBox.Location = new System.Drawing.Point(109, 32);
            this.inVpiTextBox.Name = "inVpiTextBox";
            this.inVpiTextBox.Size = new System.Drawing.Size(100, 20);
            this.inVpiTextBox.TabIndex = 2;
            // 
            // inVciTextBox
            // 
            this.inVciTextBox.Location = new System.Drawing.Point(215, 32);
            this.inVciTextBox.Name = "inVciTextBox";
            this.inVciTextBox.Size = new System.Drawing.Size(100, 20);
            this.inVciTextBox.TabIndex = 3;
            // 
            // outPortTextBox
            // 
            this.outPortTextBox.Location = new System.Drawing.Point(3, 32);
            this.outPortTextBox.Name = "outPortTextBox";
            this.outPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.outPortTextBox.TabIndex = 4;
            // 
            // outVpiTextBox
            // 
            this.outVpiTextBox.Location = new System.Drawing.Point(109, 32);
            this.outVpiTextBox.Name = "outVpiTextBox";
            this.outVpiTextBox.Size = new System.Drawing.Size(100, 20);
            this.outVpiTextBox.TabIndex = 5;
            // 
            // outVciTextBox
            // 
            this.outVciTextBox.Location = new System.Drawing.Point(215, 32);
            this.outVciTextBox.Name = "outVciTextBox";
            this.outVciTextBox.Size = new System.Drawing.Size(100, 20);
            this.outVciTextBox.TabIndex = 6;
            // 
            // inPortLabel
            // 
            this.inPortLabel.AutoSize = true;
            this.inPortLabel.Location = new System.Drawing.Point(3, 16);
            this.inPortLabel.Name = "inPortLabel";
            this.inPortLabel.Size = new System.Drawing.Size(29, 13);
            this.inPortLabel.TabIndex = 7;
            this.inPortLabel.Text = "Port:";
            // 
            // inVpiLabel
            // 
            this.inVpiLabel.AutoSize = true;
            this.inVpiLabel.Location = new System.Drawing.Point(106, 16);
            this.inVpiLabel.Name = "inVpiLabel";
            this.inVpiLabel.Size = new System.Drawing.Size(27, 13);
            this.inVpiLabel.TabIndex = 8;
            this.inVpiLabel.Text = "VPI:";
            // 
            // inVciLabel
            // 
            this.inVciLabel.AutoSize = true;
            this.inVciLabel.Location = new System.Drawing.Point(212, 16);
            this.inVciLabel.Name = "inVciLabel";
            this.inVciLabel.Size = new System.Drawing.Size(27, 13);
            this.inVciLabel.TabIndex = 9;
            this.inVciLabel.Text = "VCI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Dane ramki wychodzącej:";
            // 
            // outPortLabel
            // 
            this.outPortLabel.AutoSize = true;
            this.outPortLabel.Location = new System.Drawing.Point(3, 16);
            this.outPortLabel.Name = "outPortLabel";
            this.outPortLabel.Size = new System.Drawing.Size(29, 13);
            this.outPortLabel.TabIndex = 11;
            this.outPortLabel.Text = "Port:";
            // 
            // outVpiLabel
            // 
            this.outVpiLabel.AutoSize = true;
            this.outVpiLabel.Location = new System.Drawing.Point(106, 16);
            this.outVpiLabel.Name = "outVpiLabel";
            this.outVpiLabel.Size = new System.Drawing.Size(27, 13);
            this.outVpiLabel.TabIndex = 12;
            this.outVpiLabel.Text = "VPI:";
            // 
            // outVciLabel
            // 
            this.outVciLabel.AutoSize = true;
            this.outVciLabel.Location = new System.Drawing.Point(212, 16);
            this.outVciLabel.Name = "outVciLabel";
            this.outVciLabel.Size = new System.Drawing.Size(27, 13);
            this.outVciLabel.TabIndex = 13;
            this.outVciLabel.Text = "VCI:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(224, 134);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(50, 23);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // routingInPanel
            // 
            this.routingInPanel.Controls.Add(this.inPortTextBox);
            this.routingInPanel.Controls.Add(this.inVpiTextBox);
            this.routingInPanel.Controls.Add(this.inVciTextBox);
            this.routingInPanel.Controls.Add(this.inPortLabel);
            this.routingInPanel.Controls.Add(this.inVpiLabel);
            this.routingInPanel.Controls.Add(this.inVciLabel);
            this.routingInPanel.Controls.Add(this.label1);
            this.routingInPanel.Location = new System.Drawing.Point(12, 12);
            this.routingInPanel.Name = "routingInPanel";
            this.routingInPanel.Size = new System.Drawing.Size(318, 55);
            this.routingInPanel.TabIndex = 15;
            // 
            // routingOutPanel
            // 
            this.routingOutPanel.Controls.Add(this.outVpiTextBox);
            this.routingOutPanel.Controls.Add(this.outPortTextBox);
            this.routingOutPanel.Controls.Add(this.outVciTextBox);
            this.routingOutPanel.Controls.Add(this.label2);
            this.routingOutPanel.Controls.Add(this.outVciLabel);
            this.routingOutPanel.Controls.Add(this.outPortLabel);
            this.routingOutPanel.Controls.Add(this.outVpiLabel);
            this.routingOutPanel.Location = new System.Drawing.Point(12, 73);
            this.routingOutPanel.Name = "routingOutPanel";
            this.routingOutPanel.Size = new System.Drawing.Size(318, 55);
            this.routingOutPanel.TabIndex = 16;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(280, 134);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(50, 23);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // sourcePanel
            // 
            this.sourcePanel.Controls.Add(this.outConnectionTextBox);
            this.sourcePanel.Controls.Add(this.label3);
            this.sourcePanel.Location = new System.Drawing.Point(12, 12);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(318, 39);
            this.sourcePanel.TabIndex = 16;
            this.sourcePanel.Visible = false;
            // 
            // outConnectionTextBox
            // 
            this.outConnectionTextBox.Location = new System.Drawing.Point(3, 16);
            this.outConnectionTextBox.Name = "outConnectionTextBox";
            this.outConnectionTextBox.Size = new System.Drawing.Size(312, 20);
            this.outConnectionTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nazwa połączenia wychodzącego:";
            // 
            // sinkPanel
            // 
            this.sinkPanel.Controls.Add(this.inConnectionTextBox);
            this.sinkPanel.Controls.Add(this.label4);
            this.sinkPanel.Location = new System.Drawing.Point(12, 73);
            this.sinkPanel.Name = "sinkPanel";
            this.sinkPanel.Size = new System.Drawing.Size(318, 39);
            this.sinkPanel.TabIndex = 17;
            this.sinkPanel.Visible = false;
            // 
            // inConnectionTextBox
            // 
            this.inConnectionTextBox.Location = new System.Drawing.Point(3, 16);
            this.inConnectionTextBox.Name = "inConnectionTextBox";
            this.inConnectionTextBox.Size = new System.Drawing.Size(312, 20);
            this.inConnectionTextBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nazwa połączenia przychodzącego";
            // 
            // routerButton
            // 
            this.routerButton.AutoSize = true;
            this.routerButton.Checked = true;
            this.routerButton.Location = new System.Drawing.Point(3, 3);
            this.routerButton.Name = "routerButton";
            this.routerButton.Size = new System.Drawing.Size(52, 17);
            this.routerButton.TabIndex = 18;
            this.routerButton.TabStop = true;
            this.routerButton.Text = "router";
            this.routerButton.UseVisualStyleBackColor = true;
            this.routerButton.CheckedChanged += new System.EventHandler(this.routerButton_CheckedChanged);
            // 
            // sourceButton
            // 
            this.sourceButton.AutoSize = true;
            this.sourceButton.Location = new System.Drawing.Point(61, 3);
            this.sourceButton.Name = "sourceButton";
            this.sourceButton.Size = new System.Drawing.Size(57, 17);
            this.sourceButton.TabIndex = 19;
            this.sourceButton.Text = "source";
            this.sourceButton.UseVisualStyleBackColor = true;
            this.sourceButton.CheckedChanged += new System.EventHandler(this.sourceButton_CheckedChanged);
            // 
            // sinkButton
            // 
            this.sinkButton.AutoSize = true;
            this.sinkButton.Location = new System.Drawing.Point(124, 3);
            this.sinkButton.Name = "sinkButton";
            this.sinkButton.Size = new System.Drawing.Size(44, 17);
            this.sinkButton.TabIndex = 20;
            this.sinkButton.Text = "sink";
            this.sinkButton.UseVisualStyleBackColor = true;
            this.sinkButton.CheckedChanged += new System.EventHandler(this.sinkButton_CheckedChanged);
            // 
            // radioPanel
            // 
            this.radioPanel.Controls.Add(this.routerButton);
            this.radioPanel.Controls.Add(this.sinkButton);
            this.radioPanel.Controls.Add(this.sourceButton);
            this.radioPanel.Location = new System.Drawing.Point(12, 131);
            this.radioPanel.Name = "radioPanel";
            this.radioPanel.Size = new System.Drawing.Size(173, 26);
            this.radioPanel.TabIndex = 21;
            // 
            // AddEntryPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 164);
            this.Controls.Add(this.radioPanel);
            this.Controls.Add(this.sinkPanel);
            this.Controls.Add(this.sourcePanel);
            this.Controls.Add(this.routingOutPanel);
            this.Controls.Add(this.routingInPanel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "AddEntryPrompt";
            this.Text = "Dodaj wpis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddEntryPrompt_FormClosing);
            this.routingInPanel.ResumeLayout(false);
            this.routingInPanel.PerformLayout();
            this.routingOutPanel.ResumeLayout(false);
            this.routingOutPanel.PerformLayout();
            this.sourcePanel.ResumeLayout(false);
            this.sourcePanel.PerformLayout();
            this.sinkPanel.ResumeLayout(false);
            this.sinkPanel.PerformLayout();
            this.radioPanel.ResumeLayout(false);
            this.radioPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel routingInPanel;
        private System.Windows.Forms.Panel routingOutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inPortTextBox;
        private System.Windows.Forms.TextBox inVpiTextBox;
        private System.Windows.Forms.TextBox inVciTextBox;
        private System.Windows.Forms.TextBox outPortTextBox;
        private System.Windows.Forms.TextBox outVpiTextBox;
        private System.Windows.Forms.TextBox outVciTextBox;
        private System.Windows.Forms.Label inPortLabel;
        private System.Windows.Forms.Label inVpiLabel;
        private System.Windows.Forms.Label inVciLabel;
        private System.Windows.Forms.Label outPortLabel;
        private System.Windows.Forms.Label outVpiLabel;
        private System.Windows.Forms.Label outVciLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel sourcePanel;
        private System.Windows.Forms.TextBox outConnectionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel sinkPanel;
        private System.Windows.Forms.TextBox inConnectionTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton routerButton;
        private System.Windows.Forms.RadioButton sourceButton;
        private System.Windows.Forms.RadioButton sinkButton;
        private System.Windows.Forms.Panel radioPanel;
    }
}