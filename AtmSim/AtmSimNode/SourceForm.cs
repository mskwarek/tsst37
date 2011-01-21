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
        public SourceForm(Config.Node cNode, int mPort, int cPort)
        {
            source = new Components.Source(cNode, mPort);
            caller = new Components.Caller(cNode.Name, cPort);
            InitializeComponent();
            refresher = new Thread(RefreshContent);
            refresher.Start();
            caller.Init(cNode.Id);
        }

        private void RefreshContent()
        {
            while (true)
            {
                Thread.Sleep(50);
                foreach (int c in caller.Connections.Keys)
                {
                    connectionComboBox.Items.Add(
                        String.Format("[{0}]->{1}", c, caller.Connections[c]));
                }
                callerMessageTextBox.Text = caller.Message;
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

        private void connectButton_Click(object sender, EventArgs e)
        {
            caller.BeginCall(targetTextBox.Text);
        }


    }
}
