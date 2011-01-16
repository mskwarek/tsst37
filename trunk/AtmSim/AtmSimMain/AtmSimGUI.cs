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
        private Manager manager = new Manager();

        public AtmSimGUI()
        {
            InitializeComponent();
        }

        private void netNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.Reset();
            Loading loader = new Loading(manager);
            loader.Show();
            loader.LoadNetwork();
            foreach (string element in manager.GetElements())
                elementListBox.Items.Add(element);
        }

        private void net1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.Reset();
            Loading loader = new Loading(manager);
            loader.Show();
            loader.LoadNetwork();
            this.elementListBox.Items.Clear();
            foreach (string element in manager.GetElements())
                elementListBox.Items.Add(element);
        }

        private void net2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.Reset();
            Loading loader = new Loading(manager);
            loader.Show();
            loader.LoadNetwork();
            this.elementListBox.Items.Clear();
            foreach (string element in manager.GetElements())
                elementListBox.Items.Add(element);
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
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            /*ConfigGUI configGUI = new ConfigGUI(
                Manager.GetConfig(this.selectedName),
                Manager.GetRouting(this.selectedName));*/
            ConfigGUI configGUI = new ConfigGUI(this.manager, this.selectedName);
            configGUI.Show();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogGUI logGUI = new LogGUI(this.manager, this.selectedName);
            logGUI.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manager.SetConfig(selectedName, "send", "");
        }


    }
}
