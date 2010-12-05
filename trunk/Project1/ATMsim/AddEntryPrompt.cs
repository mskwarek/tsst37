using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtmSim
{
    public partial class AddEntryPrompt : Form
    {
        private ConfigGUI parentConfig;

        public AddEntryPrompt(ConfigGUI parentConfig)
        {
            this.parentConfig = parentConfig;
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string label =
                this.inPortTextBox.Text + ";" +
                this.inVpiTextBox.Text + ";" +
                this.inVciTextBox.Text;
            string value =
                this.outPortTextBox.Text + ";" +
                this.outVpiTextBox.Text + ";" +
                this.outVciTextBox.Text;
            if (label == ";;" || value == ";;")
                MessageBox.Show("Wprowadź wszystkie wartości.");
            else
            {
                this.parentConfig.AddRoutingEntry(label, value);
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEntryPrompt_FormClosing(object sender, EventArgs e)
        {
            this.parentConfig.Show();
        }
    }
}
