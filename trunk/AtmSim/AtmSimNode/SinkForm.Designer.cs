namespace AtmSim
{
    partial class SinkForm
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.callerMessageTextBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(260, 29);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(6, 41);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageTextBox.Size = new System.Drawing.Size(265, 114);
            this.messageTextBox.TabIndex = 2;
            // 
            // callerMessageTextBox
            // 
            this.callerMessageTextBox.Location = new System.Drawing.Point(6, 9);
            this.callerMessageTextBox.Name = "callerMessageTextBox";
            this.callerMessageTextBox.Size = new System.Drawing.Size(265, 29);
            this.callerMessageTextBox.TabIndex = 3;
            this.callerMessageTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 167);
            this.Controls.Add(this.callerMessageTextBox);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.nameLabel);
            this.Name = "SinkForm";
            this.Text = "Sink";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SinkForm_FormClosing);
        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label callerMessageTextBox;
    }
}