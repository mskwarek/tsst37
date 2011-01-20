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
    public partial class ConsoleGUI : Form
    {
        private Manager manager;
        private int id;

        public ConsoleGUI(Manager manager, int id)
        {
            this.manager = manager;
            this.id = id;
            InitializeComponent();
            if (manager.Ping(this.id))
                outputTextBox.Text += "Connected to " + manager.Get(id, "Name") + "." + Environment.NewLine;
        }

        private void inputTextBox_Enter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                outputTextBox.Text += ">> " + inputTextBox.Text + Environment.NewLine;
                outputTextBox.Text += "<< " + manager.Query(id, inputTextBox.Text) + Environment.NewLine;
                inputTextBox.Text = "";
                Refresh();
            }
        }
    }
}
