using System;
using System.Diagnostics;
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
            label.Text = "Otwieranie pliku...";
            progressBar.Value = 10;
            Refresh();
            Config.Network network;
            try { network = Config.Network.fopen(filename); }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Nieprawidłowy plik wejściowy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            label.Text = "Tworzenie węzłów...";
            progressBar.Value = 20;
            Refresh();
            Random random = new Random();
            foreach (Config.Node node in network.Nodes)
            {
                string tempfile = "tmp" + random.Next() + ".xml";
                node.save(tempfile);
                Process process = new Process();
                process.StartInfo.FileName = "AtmSimNode.exe";
                process.StartInfo.Arguments = tempfile + " " + manager.Port + " " + manager.CCPort;
                process.Start();
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
