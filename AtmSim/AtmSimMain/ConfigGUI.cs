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
    public partial class ConfigGUI : Form
    {
        private int id;
        private string elementName;
        private Manager manager;
        private Manager.Config localConfig;
        private TreeNode configuration;
        private Routing localRouting;
        // zmodyfikowane wpisy
        private List<string> modifiedConfig;
        private List<string> addedRouting;
        private List<string> removedRouting;
        private List<string> modifiedRouting;

        public ConfigGUI(Manager manager, string name)
        {
            this.manager = manager;
            this.elementName = name;
            this.localConfig = new Manager.Config(manager.GetConfig(this.elementName));
            this.localRouting = new Routing(manager.GetRouting(this.elementName));
            this.modifiedConfig = new List<string>();
            this.addedRouting = new List<string>();
            this.removedRouting = new List<string>();
            this.modifiedRouting = new List<string>();
            InitializeComponent();
            this.Text += " " + this.elementName;
            this.generalPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localConfig);
            this.routingPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localRouting);
        }

        public ConfigGUI(Manager manager, int id)
        {
            this.manager = manager;
            this.id = id;
            this.elementName = manager.Get(this.id, "Name");
            this.configuration = getTree(manager.GetConfig(this.id));
            this.localConfig = new Manager.Config(manager.GetConfig(this.elementName));
            this.localRouting = new Routing(manager.GetRouting(this.id));
            this.modifiedConfig = new List<string>();
            this.addedRouting = new List<string>();
            this.removedRouting = new List<string>();
            this.modifiedRouting = new List<string>();
            InitializeComponent();
            this.Text += " " + this.elementName;
            foreach (TreeNode node in configuration.Nodes)
                this.configTree.Nodes.Add(node);
            this.configTree.PathSeparator = ".";
            this.generalPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localConfig);
            this.routingPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localRouting);
        }

        private TreeNode getTree(Configuration configuration)
        {
            TreeNode treeNode = new TreeNode(configuration.Name);
            foreach (Configuration node in configuration.Nodes)
            {
                treeNode.Nodes.Add(getTree(node));
            }
            return treeNode;
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configTabControl.SelectedIndex == this.generalGridTab.TabIndex)
            {
                this.localConfig = new Manager.Config(manager.GetConfig(this.elementName));
                this.generalPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localConfig);
                this.generalPropertyGrid.Refresh();
                this.modifiedConfig.Clear();
            }
            else if (this.configTabControl.SelectedIndex == this.routingTab.TabIndex)
            {
                this.localRouting = new Routing(manager.GetRouting(this.elementName));
                this.routingPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localRouting);
                this.routingPropertyGrid.Refresh();
                this.addedRouting.Clear();
                this.removedRouting.Clear();
                this.modifiedRouting.Clear();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configTabControl.SelectedIndex == this.generalGridTab.TabIndex)
                saveConfig();
            else if (this.configTabControl.SelectedIndex == this.routingTab.TabIndex)
                saveRouting();
        }

        private void ConfigGUI_FormClosing(object sender, EventArgs e)
        {
            if (modifiedRouting.Count > 0 || addedRouting.Count > 0 || removedRouting.Count > 0)
            {
                DialogResult dlg = MessageBox.Show("Zapisać ustawienia routingu?", "Konfiguracja", MessageBoxButtons.YesNo);
                if (dlg == DialogResult.Yes)
                    saveRouting();
            }
            if (modifiedConfig.Count > 0)
            {
                DialogResult dlg = MessageBox.Show("Zapisać konfigurację?", "Konfiguracja", MessageBoxButtons.YesNo);
                if (dlg == DialogResult.Yes)
                    saveConfig();
            }
        }

        private void configTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            configTextBox.Text = manager.Get(id, configTree.SelectedNode.FullPath);
            if (configTextBox.Text == "")
            {
                configTextBox.Enabled = false;
                okButton.Enabled = false;
            }
            else
            {
                configTextBox.Enabled = true;
                okButton.Enabled = true;
            }
        }

        private void generalPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            modifiedConfig.Add((string)e.ChangedItem.Label);
        }

        private void routingPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            string modifiedEntry = (string)e.ChangedItem.Label;
            modifiedRouting.Add(modifiedEntry);
        }

        private void saveConfig()
        {
            foreach (string param in modifiedConfig)
            {
                manager.SetConfig(this.elementName, param, this.localConfig[param]);
            }
            modifiedConfig.Clear();
        }

        private void saveRouting()
        {
            foreach (string label in removedRouting)
                manager.RemoveRouting(this.elementName, label);
            foreach (string label in addedRouting)
                manager.AddRouting(this.elementName, label, localRouting[label]);
            foreach (string label in modifiedRouting)
                manager.ModifyRouting(this.elementName, label, label, localRouting[label]);
            addedRouting.Clear();
            removedRouting.Clear();
            modifiedRouting.Clear();
        }

        private void addRoutingEntryButton_Click(object sender, EventArgs e)
        {
            AddEntryPrompt prompt = new AddEntryPrompt(this);
            prompt.Show();
            this.Hide();
        }

        public void AddRoutingEntry(string label, string value)
        {
            if (localRouting.ContainsKey(label))
            {
                localRouting[label] = value;
                this.modifiedRouting.Add(label);
            }
            else
            {
                localRouting.Add(label, value);
                this.addedRouting.Add(label);
            }
            routingPropertyGrid.Refresh();
        }

        private void removeRoutingEntryButton_Click(object sender, EventArgs e)
        {
            string selectedEntry = routingPropertyGrid.SelectedGridItem.Label;
            if (localRouting.ContainsKey(selectedEntry))
            {
                localRouting.Remove(selectedEntry);
                removedRouting.Add(selectedEntry);
                if (modifiedRouting.Contains(selectedEntry))
                    modifiedRouting.Remove(selectedEntry);
                if (addedRouting.Contains(selectedEntry))
                    addedRouting.Remove(selectedEntry);
            }
            routingPropertyGrid.Refresh();
        }


    }
}
