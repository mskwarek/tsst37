using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    class SinkAgent : Common.IAgent
    {
        ArrayList informacionlist = new ArrayList();

        Sink node;

          public SinkAgent(Sink n)
        {
            node = n;
            //this.ResetMassage(x);
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
 
            else if (name == "target")
                node.Target = value;

        }

        public Common.RoutingTable GetRoutingTable()
        {
            Common.RoutingTable table = new Common.RoutingTable();
           
            return table;
        }
        public void AddRoutingEntry(string label, string value)
        {
           
        }
        public void RemoveRoutingEntry(string entry)
        {
            
        }
  
        public string GetLog()
        {
            return node.Log.GetString();
        }
    }
}
