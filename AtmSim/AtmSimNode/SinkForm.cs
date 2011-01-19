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
    public partial class SinkForm : Form
    {
        private Components.Sink sink;
        public SinkForm(Config.Node cNode, int mPort)
        {
            sink = new Components.Sink(cNode, mPort);
            InitializeComponent();
        }
    }
}
