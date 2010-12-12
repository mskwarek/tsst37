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

      //  private string massage = "unknown";

       /* private void ResetMassage(Node n)
        {

            massage = "Node Name :" + n.GetName().ToString() + "\n\n";

            int count = 0;
            massage += "All Out Ports :\n";
            foreach (IPortOut p in n.GetPortsOut())
            {
                count++;
                massage += count.ToString() + ": Out Port Number :" + p.portID.ToString() + "\n";

            }
            count = 0;
            massage += "All In Ports :\n";
            foreach (IPortIn i in n.GetPortsIn())
            {
                count++;
                massage += count.ToString() + ": In Port Number :" + i.portID + "\n";

            }
            count = 0;
            massage += "Current Routing Table :\n";
            foreach (DictionaryEntry de in n.GetMatrix().GetRouteTable())
            {
                count++;

                massage += "Row" + count.ToString() + " :    " + " in port number : " + ((MatrixElements)de.Key).GetPortNumber().ToString();
                massage += " VPI : " + ((MatrixElements)de.Key).GetVPI().ToString();
                massage += " VCI : " + ((MatrixElements)de.Key).GetVCI().ToString();
                massage += " out port number : " + ((MatrixElements)de.Value).GetPortNumber().ToString();
                massage += " VPI : " + ((MatrixElements)de.Value).GetVPI().ToString();
                massage += " VCI : " + ((MatrixElements)de.Value).GetVCI().ToString() + "\n";


            }


        }*/






     //   public string GetParametersOfNode() { return massage; }


        public SourceAgent(Source n)
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
            node.Matrix.Add(label, new RoutingEntry(value));
        }
        public void RemoveRoutingEntry(string entry)
        {
            node.Matrix.Remove(entry);
        }
  
        public string GetLog()
        {
            return node.Log.GetString();
        }


    }
}
