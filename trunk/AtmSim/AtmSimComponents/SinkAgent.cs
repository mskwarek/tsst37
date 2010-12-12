using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    class SinkAgent : IAgent
    {
        Sink node;

        public SinkAgent(Sink n)
        {
            node = n;
        }

        public string[] GetParamList()
        {
            string[] param = { "name" } ;
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return node.Name;
            else
                return "";
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                node.Name = value;
        }

        public Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.Receiver.Sources)
            {
                table.Add(element.Key.ToString(), element.Value);
            }
            return table;
        }

        public void AddRoutingEntry(string label, string value)
        {
            if (!node.Receiver.Sources.ContainsKey(new RoutingEntry(label)))
                node.Receiver.Sources.Add(new RoutingEntry(label), value);
        }

        public void RemoveRoutingEntry(string entry)
        {
            if (node.Receiver.Sources.ContainsKey(new RoutingEntry(entry)))
                node.Receiver.Sources.Remove(new RoutingEntry(entry));
        }
  
        public string GetLog()
        {
            return node.Log.GetString();
        }
    }
}
