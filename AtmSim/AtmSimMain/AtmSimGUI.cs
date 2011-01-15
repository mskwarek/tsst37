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

        public AtmSimGUI()
        {
            InitializeComponent();
        }

        private void netNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager.Reset();
            this.InitTestNetwork2(); // **** TODO **** w tym miejscu znajdzie się parsowanie pliku wejściowego
            this.elementListBox.Items.Clear();
            foreach (string element in Manager.GetElements())
                elementListBox.Items.Add(element);
        }

        private void net1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager.Reset();
            this.InitTestNetwork1();
            this.elementListBox.Items.Clear();
            foreach (string element in Manager.GetElements())
                elementListBox.Items.Add(element);
        }

        private void net2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager.Reset();
            this.InitTestNetwork2();
            this.elementListBox.Items.Clear();
            foreach (string element in Manager.GetElements())
                elementListBox.Items.Add(element);
        }

        private void netTopologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*TopologyGUI topologyGUI = new TopologyGUI();
            topologyGUI.Show();*/
            TopologyView topologyView = new TopologyView();
            topologyView.Show();
        }

        private void InitTestNetwork1()
        {
            Components.Node Router1 = new Components.Node(1, 2, "Router1");
            Components.Node Router2 = new Components.Node(3, 1, "Router2");
            Components.Node Router3 = new Components.Node(1, 2, "Router3");
            Components.Source Source4 = new Components.Source("Source4");
            Components.Sink Sink5 = new Components.Sink("Sink5");

            Components.PortIn in21 = new Components.PortIn(0); in21.SetReceiver(Router1.GetMatrix());
            Components.PortIn in12 = new Components.PortIn(0); in12.SetReceiver(Router2.GetMatrix());
            Components.PortIn in32 = new Components.PortIn(1); in32.SetReceiver(Router2.GetMatrix());
            Components.PortIn in13 = new Components.PortIn(0); in13.SetReceiver(Router3.GetMatrix());
            Components.PortIn in35 = new Components.PortIn(0); in35.SetReceiver(Sink5.Receiver);
            Components.PortIn in42 = new Components.PortIn(2); in42.SetReceiver(Router2.GetMatrix());

            Components.PortOut out12 = new Components.PortOut(0); out12.TcpPort = in12.TcpPort; out12.Connect();
            Components.PortOut out13 = new Components.PortOut(1); out13.TcpPort = in13.TcpPort; out13.Connect();
            Components.PortOut out21 = new Components.PortOut(0); out21.TcpPort = in21.TcpPort; out21.Connect();
            Components.PortOut out32 = new Components.PortOut(0); out32.TcpPort = in32.TcpPort; out32.Connect();
            Components.PortOut out42 = new Components.PortOut(0); out42.TcpPort = in42.TcpPort; out42.Connect();
            Components.PortOut out35 = new Components.PortOut(1); out35.TcpPort = in35.TcpPort; out35.Connect();

            Router1.GetPortsIn().SetValue(in21, 0);
            Router2.GetPortsIn().SetValue(in12, 0);
            Router2.GetPortsIn().SetValue(in32, 1);
            Router2.GetPortsIn().SetValue(in42, 2);
            Router3.GetPortsIn().SetValue(in13, 0);

            Router1.GetPortsOut().SetValue(out12, 0);
            Router1.GetPortsOut().SetValue(out13, 1);
            Router2.GetPortsOut().SetValue(out21, 0);
            Router3.GetPortsOut().SetValue(out32, 0);
            Router3.GetPortsOut().SetValue(out35, 1);

            Source4.SetPortOut(out42);
            Sink5.SetPortIn(in35);

            Manager.AddNode("Router1", Router1.Agent);
            Manager.AddNode("Router2", Router2.Agent);
            Manager.AddNode("Router3", Router3.Agent);
            Manager.AddNode("Source4", Source4.Agent);
            Manager.AddNode("Sink5", Sink5.Agent);

            Manager.AddConnection("Router2", "Router1");
            Manager.AddConnection("Router1", "Router2");
            Manager.AddConnection("Router3", "Router2");
            Manager.AddConnection("Router1", "Router3");
            Manager.AddConnection("Source4", "Router2");
            Manager.AddConnection("Router3", "Sink5");

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
            //Manager.AddRouting("Router3",

            Manager.SetConfig("Source4", "message", "s");
            Manager.SetConfig("Source4", "target", "A");
        }

        private void InitTestNetwork2()
        {
            Components.Node Router1 = new Components.Node(1, 2, "Router1");
            Components.Node Router2 = new Components.Node(3, 1, "Router2");
            Components.Node Router3 = new Components.Node(1, 2, "Router3");

            Components.TestPortIn in21 = new Components.TestPortIn(0);
            Components.TestPortIn in12 = new Components.TestPortIn(0);
            Components.TestPortIn in32 = new Components.TestPortIn(1);
            Components.TestPortIn in13 = new Components.TestPortIn(0);
            Components.TestPortIn in35 = new Components.TestPortIn(0);
            Components.TestPortIn in42 = new Components.TestPortIn(2);

            Components.TestPortOut out12 = new Components.TestPortOut(0); out12.Connect(in12);
            Components.TestPortOut out13 = new Components.TestPortOut(1); out13.Connect(in13);
            Components.TestPortOut out21 = new Components.TestPortOut(0); out21.Connect(in21);
            Components.TestPortOut out32 = new Components.TestPortOut(0); out32.Connect(in32);
            Components.TestPortOut out42 = new Components.TestPortOut(0); out42.Connect(in42);
            Components.TestPortOut out35 = new Components.TestPortOut(1); out35.Connect(in35);

            Router1.GetPortsIn().SetValue(in21, 0);
            Router2.GetPortsIn().SetValue(in12, 0);
            Router2.GetPortsIn().SetValue(in32, 1);
            Router2.GetPortsIn().SetValue(in42, 2);
            Router3.GetPortsIn().SetValue(in13, 0);

            Router1.GetPortsOut().SetValue(out12, 0);
            Router1.GetPortsOut().SetValue(out13, 1);
            Router2.GetPortsOut().SetValue(out21, 0);
            Router3.GetPortsOut().SetValue(out32, 0);
            Router3.GetPortsOut().SetValue(out35, 1);

            Components.Source Source4 = new Components.Source("Source4");
            Source4.SetPortOut(out42);
            Components.Sink Sink5 = new Components.Sink("Sink5");

            Manager.AddNode("Router1", Router1.Agent);
            Manager.AddNode("Router2", Router2.Agent);
            Manager.AddNode("Router3", Router3.Agent);
            Manager.AddConnection("Router2", "Router1");
            Manager.AddConnection("Router1", "Router2");
            Manager.AddConnection("Router3", "Router2");
            Manager.AddConnection("Router1", "Router3");

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
            ConfigGUI configGUI = new ConfigGUI(this.selectedName);
            configGUI.Show();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogGUI logGUI = new LogGUI(this.selectedName);
            logGUI.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager.SetConfig(selectedName, "send", "");
        }


    }
}
