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
        private Components.Caller caller;
        private Thread refresher;
        private bool refloop = true;
        public SourceForm(Config.Node cNode, int mPort, int cPort)
        {
            source = new Components.Source(cNode, mPort);
            caller = new Components.Caller(cNode.Name, cPort);
            InitializeComponent();
            refresher = new Thread(RefreshContent);
            refresher.Start();
            caller.Init(cNode.Id);
            this.Text = cNode.Name;
        }

        private void RefreshContent()
        {
            while (refloop)
            {
                Thread.Sleep(50);
                callerMessageTextBox.Text = caller.Message;
            }
        }


        private void sendButton_Click(object sender, EventArgs e)
        {
            string target = ((string)connectionComboBox.SelectedItem).Split(new char[]{'[', ']'}, StringSplitOptions.RemoveEmptyEntries)[0];
            if (source.Matrix.ContainsKey(target) && messageTextBox.Text != "")
            {
                source.Target = target;
                source.Message = messageTextBox.Text;
                Thread send = new Thread(source.Send);
                send.Start();
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            caller.BeginCall(targetTextBox.Text, Convert.ToInt32(capNumeric.Value));
        }

        private void SourceForm_FormClosing(object sender, EventArgs e)
        {
            refloop = false;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            connectionComboBox.Items.Clear();
            foreach (int c in caller.Connections.Keys)
            {
                connectionComboBox.Items.Add(
                    String.Format("[{0}]->{1}", c, caller.Connections[c]));
            }

        }
    }
}
