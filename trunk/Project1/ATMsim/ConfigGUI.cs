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
        private string elementName;
        private Manager.Config localConfig;
        private Routing localRouting;
        // zmodyfikowane wpisy
        private List<string> modifiedConfig;
        private List<string> addedRouting;
        private List<string> removedRouting;
        private List<string> modifiedRouting;

        public ConfigGUI(string name)
        {
            this.elementName = name;
            this.localConfig = new Manager.Config(Manager.GetConfig(this.elementName));
            this.localRouting = new Routing(Manager.GetRouting(this.elementName));
            this.modifiedConfig = new List<string>();
            this.addedRouting = new List<string>();
            this.removedRouting = new List<string>();
            this.modifiedRouting = new List<string>();
            InitializeComponent();
            this.Text += " " + this.elementName;
            this.generalPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localConfig);
            this.routingPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localRouting);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configTabControl.SelectedIndex == this.generalTab.TabIndex)
            {
                this.localConfig = new Manager.Config(Manager.GetConfig(this.elementName));
                this.generalPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localConfig);
                this.generalPropertyGrid.Refresh();
                this.modifiedConfig.Clear();
            }
            else if (this.configTabControl.SelectedIndex == this.routingTab.TabIndex)
            {
                this.localRouting = new Routing(Manager.GetRouting(this.elementName));
                this.routingPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter(this.localRouting);
                this.routingPropertyGrid.Refresh();
                this.addedRouting.Clear();
                this.removedRouting.Clear();
                this.modifiedRouting.Clear();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configTabControl.SelectedIndex == this.generalTab.TabIndex)
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
                Manager.SetConfig(this.elementName, param, this.localConfig[param]);
            }
            modifiedConfig.Clear();
        }

        private void saveRouting()
        {
            foreach (string label in removedRouting)
                Manager.RemoveRouting(this.elementName, label);
            foreach (string label in addedRouting)
                Manager.AddRouting(this.elementName, label, localRouting[label]);
            foreach (string label in modifiedRouting)
                Manager.ModifyRouting(this.elementName, label, label, localRouting[label]);
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
