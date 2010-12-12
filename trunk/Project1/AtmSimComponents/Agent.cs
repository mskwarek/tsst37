using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components 
{

    public class Agent : IAgent

    {
        // Referencja wezwa zarzadzanego przez agenta
        Node node;

        public Agent(Node n)
        {
            node = n;
        }

        public string[] GetParamList()
        {
            string[] param = { "name", "portsIn", "portsOut" };
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return node.GetName();
            else if (name == "portsIn")
                return "0-" + (node.GetPortsIn().Length - 1);
            else if (name == "portsOut")
                return "0-" + (node.GetPortsOut().Length - 1);
            else return "";
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                node.SetName(value);
        }

        public Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.GetMatrix().GetRouteTable())
            {
                table.Add(element.Key.ToString(),element.Value.ToString());
            }
            return table;
        }
        public void AddRoutingEntry(string label, string value)
        {
            node.GetMatrix().AddToMatrix(new RoutingEntry(label), new RoutingEntry(value));
        }
        public void RemoveRoutingEntry(string entry)
        {
            node.GetMatrix().DeleteFromMatrix(new RoutingEntry(entry));
        }
        public string GetLog()
        {
            return node.Log.GetString();
        }
    }
}
