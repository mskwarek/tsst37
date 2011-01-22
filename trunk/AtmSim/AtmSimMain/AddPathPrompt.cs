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
    public partial class AddPathPrompt : Form
    {
        Manager manager;
        RoutingController rc;
        VirtualPath path;

        public AddPathPrompt(Manager manager)
        {
            this.manager = manager;
            this.rc = manager.CallController.RC;
            InitializeComponent();
            foreach (string item in manager.GetElements())
            {
                srcComboBox.Items.Add(item);
                trgComboBox.Items.Add(item);
            }
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            int id = manager.GetFreeId();
            path = rc.setupPath(
                Int32.Parse(((string)srcComboBox.SelectedItem).Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries)[0]),
                Int32.Parse(((string)trgComboBox.SelectedItem).Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries)[0]),
                id, Convert.ToInt32(capNumericUpDown.Value));
            if (path == null)
            {
                resultLabel.Text = "Brak";
                connectButton.Enabled = false;
            }
            else
            {
                resultLabel.Text = "OK";
                connectButton.Enabled = true;
            }
            Refresh();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            this.manager.AddPath(path);
        }

        private void srcComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxes();
        }

        private void trgComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxes();
        }

        private void checkBoxes()
        {
            if ((string)srcComboBox.SelectedItem != "" && (string)trgComboBox.SelectedItem != "" &&
                (string)srcComboBox.SelectedItem != (string)trgComboBox.SelectedItem)
                findButton.Enabled = true;
            else
                findButton.Enabled = false;
            Refresh();
        }

    }
}
