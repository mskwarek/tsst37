using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AtmSim
{
    public partial class SourceForm : Form
    {
        private Components.Source source;
        public SourceForm(Config.Node cNode, int mPort)
        {
            source = new Components.Source(cNode, mPort);
            InitializeComponent();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            connectionComboBox.Items.Clear();
            foreach (string c in source.Matrix.Keys)
            {
                connectionComboBox.Items.Add(c);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (source.Matrix.ContainsKey((string)connectionComboBox.SelectedItem) && messageTextBox.Text != "")
            {
                source.Target = (string)connectionComboBox.SelectedItem;
                source.Message = messageTextBox.Text;
                Thread send = new Thread(source.Send);
                send.Start();
            }
        }
    }
}
