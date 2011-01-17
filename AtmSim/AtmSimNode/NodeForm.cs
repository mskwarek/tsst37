using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AtmSim
{
    public partial class NodeForm : Form
    {
        private Components.Node node;
        
        public NodeForm(string[] args)
        {
            if (!File.Exists(args[0]))
                Close();
            Config.Node cNode = Config.Node.fopen(args[0]);
            File.Delete(args[0]);
            int mPort = Int32.Parse(args[1]);
            node = new Components.Node(cNode, mPort);
            InitializeComponent();
        }
    }
}
