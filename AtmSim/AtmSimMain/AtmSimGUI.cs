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
    public partial class AtmSimGUI : Form
    {
        private string selectedName = "";
        private int SelectedId
        {
            get
            {
                string[] tokens = selectedName.Split(new char[]{'[', ']'}, StringSplitOptions.RemoveEmptyEntries);
                int id = 0, i=0;
                while (id == 0 && i<tokens.Length)
                {
                    id = Convert.ToInt32(tokens[i]);
                    i++;
                }
                return id;
            }
        }
        private Manager manager = new Manager();

        public AtmSimGUI()
        {
            manager.Init();
            InitializeComponent();
            mToolStripMenuItem.Text = "M: " + manager.Port;
        }

        private void netNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                manager.Reset();
                Loader loader = new Loader(manager);
                loader.Show();
                loader.LoadNetwork(openFileDialog.FileName);
                RefreshList();
            }
        }

        private void netTopologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*TopologyGUI topologyGUI = new TopologyGUI();
            topologyGUI.Show();*/
            TopologyView topologyView = new TopologyView(manager);
            topologyView.Show();
        }

        private void elementListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.elementListBox.SelectedIndex >= 0)
                this.selectedName = (string)this.elementListBox.Items[this.elementListBox.SelectedIndex];
            if (manager.Ping(this.SelectedId) == false)
                RefreshList();
            string type = manager.Get(this.SelectedId, "type") ;
            /*if (type != "Switch")
                configButton.Enabled = false;
            else
                configButton.Enabled = true;
            Refresh();*/
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            /*ConfigGUI configGUI = new ConfigGUI(
                Manager.GetConfig(this.selectedName),
                Manager.GetRouting(this.selectedName));*/
            ConfigGUI configGUI = new ConfigGUI(this.manager, this.SelectedId);
            configGUI.Show();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogGUI logGUI = new LogGUI(this.manager, this.SelectedId);
            logGUI.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            elementListBox.Items.Clear();
            foreach (string element in manager.GetElements())
                elementListBox.Items.Add(element);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Manager.AddRouting("Router1", "0:3:-", "1:2:-");
            Manager.AddRouting("Router2", "2:1:2", "0:3:2");
            Manager.AddRouting("Router2", "2:2:1", "0:3:1");
            Manager.AddRouting("Router2", "1:1:3", "0:3:3");
            Manager.AddRouting("Router3", "0:2:1", "0:1:3");
            Manager.AddRouting("Router3", "0:2:2", "1:1:2");
            Manager.AddRouting("Router3", "0:2:3", "1:1:1");
            Manager.AddRouting("Source4", "A", "0:1;2");
            Manager.AddRouting("Source4", "B", "0:2:1");
            Manager.AddRouting("Sink5", "0:1:1", "B");
            Manager.AddRouting("Sink5", "0:1:2", "A");
             */
            manager.AddRouting(1, "0:3:-", "1:2:-");
            manager.AddRouting(2, "2:1:2", "0:3:2");
            manager.AddRouting(2, "2:2:1", "0:3:1");
            manager.AddRouting(2, "1:1:3", "0:3:3");
            manager.AddRouting(3, "0:2:1", "0:1:3");
            manager.AddRouting(3, "0:2:2", "1:1:2");
            manager.AddRouting(3, "0:2:3", "1:1:1");
            manager.AddRouting(4, "A", "0:1;2");
            manager.AddRouting(4, "B", "0:2:1");
            manager.AddRouting(5, "0:1:1", "B");
            manager.AddRouting(5, "0:1:2", "A");            
        }
    }
}
