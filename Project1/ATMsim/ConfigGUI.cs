using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATMsim
{
    public partial class ConfigGUI : Form
    {
        // przechowujemy dwie wersje konfiguracji i routingu:
        // globalConfig - referencja do konfiguracji globalnej
        private Dictionary<string, string> globalConfig;
        private Dictionary<string, string> globalRouting;
        // localConfig - kopia konfiguracji globalnej, na ktorej dokonujemy zmian
        private Dictionary<string, string> localConfig;
        private Dictionary<string, string> localRouting;
        // zmodyfikowane wpisy
        private List<string> changedConfig;
        private List<string> changedRouting;

        public ConfigGUI(Dictionary<string, string> config, Dictionary<string, string> routing)
        {
            this.globalConfig = config;
            this.globalRouting = routing;
            this.localConfig = new Dictionary<string,string>(globalConfig);
            this.localRouting = new Dictionary<string, string>(globalRouting);
            this.changedConfig = new List<string>();
            this.changedRouting = new List<string>();
            InitializeComponent();
            this.generalPropertyGrid.SelectedObject = new Utils.DictionaryPropertyGridAdapter(this.localConfig);
            this.routingPropertyGrid.SelectedObject = new Utils.DictionaryPropertyGridAdapter(this.localRouting);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configTabControl.SelectedIndex == this.generalTab.TabIndex)
            {
                //this.localConfig.Clear();
                foreach (string param in this.changedConfig)
                {
                    this.localConfig[param] = this.globalConfig[param];
                }
                this.generalPropertyGrid.Refresh();
            }
            else if (this.configTabControl.SelectedIndex == this.generalTab.TabIndex)
            {
                //this.localRouting.Clear();
                foreach (string param in this.changedRouting)
                {
                    this.localRouting[param] = this.globalRouting[param];
                }
                this.routingPropertyGrid.Refresh();
            
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
            if (changedRouting.Count > 0)
            {
                DialogResult dlg = MessageBox.Show("Zapisać ustawienia routingu?", "Konfiguracja", MessageBoxButtons.YesNo);
                if (dlg == DialogResult.Yes)
                    saveRouting();
            }
            if (changedConfig.Count > 0)
            {
                DialogResult dlg = MessageBox.Show("Zapisać konfigurację?", "Konfiguracja", MessageBoxButtons.YesNo);
                if (dlg == DialogResult.Yes)
                    saveConfig();
            }
        }

        private void generalPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            changedConfig.Add((string)e.ChangedItem.Label);
        }

        private void routingPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            changedRouting.Add((string)e.ChangedItem.Label);
        }

        private void saveConfig()
        {
            foreach (string param in changedConfig)
            {
                globalConfig[param] = localConfig[param];
            }
            changedConfig.Clear();
        }

        private void saveRouting()
        {
            foreach (string param in changedRouting)
            {
                globalRouting[param] = localRouting[param];
            }
            changedRouting.Clear();
        }

    }
}
