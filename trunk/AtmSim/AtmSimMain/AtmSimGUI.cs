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

        private Thread refresher;
        private bool refloop = true;

        public AtmSimGUI()
        {
            manager.Init();
            InitializeComponent();
            refresher = new Thread(RefreshThread);
            refresher.Start();
        }

        private void RefreshThread()
        {
            while (refloop)
            {
                Thread.Sleep(2000);
                RefreshList();
            }
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
            TopologyView topologyView = new TopologyView(manager);
            topologyView.Show();
        }

        private void elementsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.elementsListBox.SelectedIndex >= 0)
                this.selectedName = (string)this.elementsListBox.Items[this.elementsListBox.SelectedIndex];
            if (manager.Ping(this.SelectedId) == false)
                RefreshList();
            string type = manager.Get(this.SelectedId, "type") ;
        }

        private void connectionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.connectionsListBox.SelectedIndex >= 0)
                this.selectedName = (string)this.connectionsListBox.Items[this.connectionsListBox.SelectedIndex];
        }

        private void pathsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.pathsListBox.SelectedIndex >= 0)
                this.selectedName = (string)this.pathsListBox.Items[this.pathsListBox.SelectedIndex];
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == elementsTabPage.TabIndex)
            {
                ConfigGUI configGUI = new ConfigGUI(this.manager, this.SelectedId);
                configGUI.Show();
            }
            if (tabControl.SelectedIndex == connectionsTabPage.TabIndex)
            {
                MessageBox.Show(manager.GetDetails(this.SelectedId));
            }
            if (tabControl.SelectedIndex == pathsTabPage.TabIndex)
            {
                MessageBox.Show(manager.GetDetails(this.SelectedId));
            }
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

        private delegate void RefreshDelegate();
        private void RefreshList()
        {
            if (tabControl.InvokeRequired)
            {
                if (refloop)
                    this.Invoke(new RefreshDelegate(RefreshList));
            }
            else
            {
                if (tabControl.SelectedIndex == elementsTabPage.TabIndex)
                {
                    string selected = "";
                    if (elementsListBox.SelectedIndex >= 0)
                        selected = (string)elementsListBox.Items[elementsListBox.SelectedIndex];
                    elementsListBox.Items.Clear();
                    foreach (string item in manager.GetElements())
                        elementsListBox.Items.Add(item);
                    if (elementsListBox.Items.Contains(selected))
                        elementsListBox.SelectedIndex = elementsListBox.Items.IndexOf(selected);
                }
                if (tabControl.SelectedIndex == linksTabPage.TabIndex)
                {
                    string selected = "";
                    if (linksListBox.SelectedIndex >= 0)
                        selected = (string)linksListBox.Items[linksListBox.SelectedIndex];
                    linksListBox.Items.Clear();
                    foreach (string item in manager.GetLinks())
                        linksListBox.Items.Add(item);
                    if (linksListBox.Items.Contains(selected))
                        linksListBox.SelectedIndex = linksListBox.Items.IndexOf(selected);
                }
                if (tabControl.SelectedIndex == connectionsTabPage.TabIndex)
                {
                    string selected = "";
                    if (connectionsListBox.SelectedIndex >= 0)
                        selected = (string)connectionsListBox.Items[connectionsListBox.SelectedIndex];
                    connectionsListBox.Items.Clear();
                    foreach (string item in manager.GetConnections())
                        connectionsListBox.Items.Add(item);
                    if (connectionsListBox.Items.Contains(selected))
                        connectionsListBox.SelectedIndex = connectionsListBox.Items.IndexOf(selected);
                }
                if (tabControl.SelectedIndex == pathsTabPage.TabIndex)
                {
                    string selected = "";
                    if (pathsListBox.SelectedIndex >= 0)
                        selected = (string)pathsListBox.Items[pathsListBox.SelectedIndex];
                    pathsListBox.Items.Clear();
                    foreach (string item in manager.GetVPaths())
                        pathsListBox.Items.Add(item);
                    if (pathsListBox.Items.Contains(selected))
                        pathsListBox.SelectedIndex = pathsListBox.Items.IndexOf(selected);
                }
            }
        }

        private void cmdButton_Click(object sender, EventArgs e)
        {
            ConsoleGUI consoleGUI = new ConsoleGUI(manager, SelectedId);
            consoleGUI.Show();
        }

        private void elementsListBox_MouseDoubleClick(object sender, EventArgs e)
        {
            ConfigGUI configGUI = new ConfigGUI(this.manager, this.SelectedId);
            configGUI.Show();
        }

        private void connectionsListBox_MouseDoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(manager.GetDetails(this.SelectedId));
        }

        private void pathsListBox_MouseDoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(manager.GetDetails(this.SelectedId));
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == elementsTabPage.TabIndex)
            {
                configButton.Enabled = true;
                configButton.Text = "Konfiguracja";
                logButton.Enabled = true;
                cmdButton.Enabled = true;
                addButton.Enabled = false;
            }
            if (tabControl.SelectedIndex == linksTabPage.TabIndex)
            {
                configButton.Enabled = false;
                configButton.Text = "Szczegóły";
                logButton.Enabled = false;
                cmdButton.Enabled = false;
                addButton.Enabled = false;
            }
            if (tabControl.SelectedIndex == connectionsTabPage.TabIndex)
            {
                configButton.Enabled = true;
                configButton.Text = "Szczegóły";
                logButton.Enabled = false;
                cmdButton.Enabled = false;
                addButton.Enabled = false;
            }
            if (tabControl.SelectedIndex == pathsTabPage.TabIndex)
            {
                configButton.Enabled = true;
                configButton.Text = "Szczegóły";
                logButton.Enabled = false;
                cmdButton.Enabled = false;
                addButton.Enabled = true;
            }
            Refresh();
            RefreshList();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == pathsTabPage.TabIndex)
            {
                AddPathPrompt prompt = new AddPathPrompt(this.manager);
                prompt.Show();
            }
        }

        private void AtmSimGUI_FormClosing(object sender, EventArgs e)
        {
            refloop = false;
            refresher.Join();
        }
    }
}
