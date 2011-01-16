using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim.Components 
{
    public class NodeAgent : IAgent
    {
        // Referencja wezla zarzadzanego przez agenta
        Node node;
        // Zawartosc konfiguracji wezla
        Configuration config;
        Socket managerSocket;

        public NodeAgent(Node n, int port)
        {
            node = n;
            config = new Configuration(n.Name);
            Configuration pIn = new Configuration("PortsIn");
            for (int i = 0; i<node.PortsIn.Length; i++ ) {
                pIn.Add("Open");
                pIn.Add("Connected");
                pIn.Add("_port");
            }
            Configuration pOut = new Configuration("PortsIn");
            for (int i = 0; i < node.PortsIn.Length; i++)
            {
                pOut.Add("Open");
                pOut.Add("Connected");
                pOut.Add("_port");
            }
            config.Add("ID");
            config.Add("Name");
            config.Add(pIn);
            config.Add(pOut);

            managerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint server = new IPEndPoint(IPAddress.Loopback, port);
            managerSocket.Connect(server);

        }

        public string[] GetParamList()
        {
            string[] param = { "name", "portsIn", "portsOut" };
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return node.Name;
            else if (name == "portsIn")
                return "0-" + (node.PortsIn.Length - 1);
            else if (name == "portsOut")
                return "0-" + (node.PortsOut.Length - 1);
            else return "";
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                node.Name = value;
        }

        public Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.Matrix.RoutingTable)
            {
                table.Add(element.Key.ToString(),element.Value.ToString());
            }
            return table;
        }

        public void AddRoutingEntry(string label, string value)
        {
            node.Matrix.AddToMatrix(new RoutingEntry(label), new RoutingEntry(value));
        }

        public void RemoveRoutingEntry(string entry)
        {
            node.Matrix.DeleteFromMatrix(new RoutingEntry(entry));
        }

        public string GetLog()
        {
            return node.Log.GetString();
        }
    }
}
