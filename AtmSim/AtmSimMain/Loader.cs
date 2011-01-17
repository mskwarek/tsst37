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
    public partial class Loader : Form
    {
        private Manager manager;
        public Loader(Manager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        public void LoadNetwork(string filename)
        {
            Config.Network network = new Config.Network();
            label.Text = "Uruchamianie zarządcy...";
            Refresh();
            manager.Init();
            label.Text = "Otwieranie pliku...";
            progressBar.Value = 10;
            Refresh();
            network.readFile(filename);
            label.Text = "Tworzenie węzłów...";
            progressBar.Value = 20;
            Refresh();
            foreach (Config.Node node in network.Nodes)
            {
                Components.Node cnode = new Components.Node(node, manager.Port);
                manager.AddNode(cnode.Name, cnode.Agent);
                progressBar.Value += (40 / network.Nodes.Count);
                Refresh();
            }
            label.Text = "Oczekiwanie na agentów...";
            Refresh();
            while (manager.ConnectedNodes < network.Nodes.Count)
            {}
            label.Text = "Tworzenie łączy...";
            progressBar.Value = 60;
            Refresh();
            foreach (Config.Link link in network.Links)
            {
                manager.AddLink(link);
                progressBar.Value += (40 / network.Links.Count);
                Refresh();
            }
            this.Close();
        }
    }
}
