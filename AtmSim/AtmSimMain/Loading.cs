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
    public partial class Loading : Form
    {
        private Manager manager;
        public Loading(Manager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        public void LoadNetwork()
        {
            InitTestNetwork1();
            this.Close();
        }

        private void InitTestNetwork1()
        {
            label.Text = "Uruchamianie zarządcy...";
            manager.Init();
            label.Text = "Tworzenie węzłów...";
            progressBar.Value = 20;
            Components.Node Router1 = new Components.Node(1, 2, "Router1", manager.Port);
            Components.Node Router2 = new Components.Node(3, 1, "Router2", manager.Port);
            Components.Node Router3 = new Components.Node(1, 2, "Router3", manager.Port);
            Components.Source Source4 = new Components.Source("Source4");
            Components.Sink Sink5 = new Components.Sink("Sink5");

            label.Text = "Tworzenie łączy...";
            progressBar.Value = 40;
            Components.PortIn in21 = new Components.PortIn(0); in21.SetReceiver(Router1.Matrix);
            Components.PortIn in12 = new Components.PortIn(0); in12.SetReceiver(Router2.Matrix);
            Components.PortIn in32 = new Components.PortIn(1); in32.SetReceiver(Router2.Matrix);
            Components.PortIn in13 = new Components.PortIn(0); in13.SetReceiver(Router3.Matrix);
            Components.PortIn in35 = new Components.PortIn(0); in35.SetReceiver(Sink5.Receiver);
            Components.PortIn in42 = new Components.PortIn(2); in42.SetReceiver(Router2.Matrix);

            Components.PortOut out12 = new Components.PortOut(0); out12.TcpPort = in12.TcpPort; out12.Connect();
            Components.PortOut out13 = new Components.PortOut(1); out13.TcpPort = in13.TcpPort; out13.Connect();
            Components.PortOut out21 = new Components.PortOut(0); out21.TcpPort = in21.TcpPort; out21.Connect();
            Components.PortOut out32 = new Components.PortOut(0); out32.TcpPort = in32.TcpPort; out32.Connect();
            Components.PortOut out42 = new Components.PortOut(0); out42.TcpPort = in42.TcpPort; out42.Connect();
            Components.PortOut out35 = new Components.PortOut(1); out35.TcpPort = in35.TcpPort; out35.Connect();

            Router1.PortsIn.SetValue(in21, 0);
            Router2.PortsIn.SetValue(in12, 0);
            Router2.PortsIn.SetValue(in32, 1);
            Router2.PortsIn.SetValue(in42, 2);
            Router3.PortsIn.SetValue(in13, 0);

            Router1.PortsOut.SetValue(out12, 0);
            Router1.PortsOut.SetValue(out13, 1);
            Router2.PortsOut.SetValue(out21, 0);
            Router3.PortsOut.SetValue(out32, 0);
            Router3.PortsOut.SetValue(out35, 1);

            Source4.SetPortOut(out42);
            Sink5.SetPortIn(in35);

            manager.AddNode("Router1", Router1.Agent);
            manager.AddNode("Router2", Router2.Agent);
            manager.AddNode("Router3", Router3.Agent);
            manager.AddNode("Source4", Source4.Agent);
            manager.AddNode("Sink5", Sink5.Agent);

            manager.AddLink("Router2", "Router1");
            manager.AddLink("Router1", "Router2");
            manager.AddLink("Router3", "Router2");
            manager.AddLink("Router1", "Router3");
            manager.AddLink("Source4", "Router2");
            manager.AddLink("Router3", "Sink5");

            label.Text = "Konfiguracja początkowa...";
            progressBar.Value = 80;
            manager.AddRouting("Router1", "0:3:-", "1:2:-");
            manager.AddRouting("Router2", "2:1:2", "0:3:2");
            manager.AddRouting("Router2", "2:2:1", "0:3:1");
            manager.AddRouting("Router2", "1:1:3", "0:3:3");
            manager.AddRouting("Router3", "0:2:1", "0:1:3");
            manager.AddRouting("Router3", "0:2:2", "1:1:2");
            manager.AddRouting("Router3", "0:2:3", "1:1:1");
            manager.AddRouting("Source4", "A", "0:1;2");
            manager.AddRouting("Source4", "B", "0:2:1");
            manager.AddRouting("Sink5", "0:1:1", "B");
            manager.AddRouting("Sink5", "0:1:2", "A");

            manager.SetConfig("Source4", "message", "s");
            manager.SetConfig("Source4", "target", "A");
        }
    }
}
