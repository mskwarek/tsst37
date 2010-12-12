using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class SourceAgent : IAgent
    {
        ArrayList informacionlist = new ArrayList();

        Source node;

        public SourceAgent(Source n)
        {
            node = n;
        }

        public string[] GetParamList()
        {
            string[] param = { "name","message","target" } ;
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return node.Name;
            else if (name == "message")
                return node.Message;
            else if (name == "target")
                return node.Target;
            else
                return "";
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                node.Name = value;
            else if (name == "message")
                if (value == "random") { node.Message = null; }
                else { node.Message = value; }
            else if (name == "send")
                node.Send();
            else if (name == "target")
                node.Target = value;

        }

        public Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.Matrix)
            {
                table.Add(element.Key, element.Value.ToString());
            }
            return table;
        }

        public void AddRoutingEntry(string label, string value)
        {
            if (!node.Matrix.ContainsKey(label))
                node.Matrix.Add(label, new RoutingEntry(value));
        }

        public void RemoveRoutingEntry(string entry)
        {
            if (node.Matrix.ContainsKey(entry))
                node.Matrix.Remove(entry);
        }
  
        public string GetLog()
        {
            return node.Log.GetString();
        }
    }
}
