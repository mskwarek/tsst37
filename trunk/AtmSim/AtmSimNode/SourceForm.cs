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
    public partial class SourceForm : Form
    {
        private Components.Source source;
        public SourceForm(Config.Node cNode, int mPort)
        {
            source = new Components.Source(cNode, mPort);
            InitializeComponent();
        }
    }
}
