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
    public partial class SinkForm : Form
    {
        private Components.Sink sink;
        Thread refresher;
        public SinkForm(Config.Node cNode, int mPort)
        {
            sink = new Components.Sink(cNode, mPort);
            InitializeComponent();
            refresher = new Thread(RefreshTextBox);
            refresher.Start();
        }

        private void RefreshTextBox()
        {
            while (true)
            {
                Thread.Sleep(50);
                messageTextBox.Text = sink.Receiver.Buffer;
            }
        }
    }
}
