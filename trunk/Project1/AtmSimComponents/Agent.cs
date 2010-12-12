using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components 
{

    public class Agent : IAgent

    {


        ArrayList informacionlist = new ArrayList();

        Node node;

        private string massage = "unknown";

        private void ResetMassage(Node n)
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
            //foreach (var e in n.GetMatrix().GetRouteTable())
            //{
            //    count++;
            //    var de = new Common.RoutingEntry(e);
            //    massage += "Row" + count.ToString() + " :    " + " in port number : " + ((Common.RoutingEntry)de.Key).Port.ToString();
            //    massage += " VPI : " + ((Common.RoutingEntry)de.Key).Vpi.ToString();
            //    massage += " VCI : " + ((Common.RoutingEntry)de.Key).Vci.ToString();
            //    massage += " out port number : " + ((Common.RoutingEntry)de.Value).Port.ToString();
            //    massage += " VPI : " + ((Common.RoutingEntry)de.Value).Vpi.ToString();
            //    massage += " VCI : " + ((Common.RoutingEntry)de.Value).Vci.ToString() + "\n";


            //}


        }






        public string GetParametersOfNode() { return massage; }


        public Agent(Node n)
        {
            node = n;
            this.ResetMassage(node);
        }


        public void AddRowToRouteTable(string m1, string m2)
        {
            /*
            int count = 0;
            foreach (PortOut po in x.GetPortsOut())
            {

                if (m1.Port == po.GetNumber()) count++;

            }
            foreach (PortIn pi in x.GetPortsIn())
            {

                if (m2.Port == pi.GetNumber()) count++;

            }
            

            if (count == 2)
            {
            */
                node.GetMatrix().AddToMatrix(new RoutingEntry(m1), new RoutingEntry(m2));

                ResetMassage(node);
            //}


        }

        public void DeleteRowFromRouteTable(string m)
        {

            node.GetMatrix().DeleteFromMatrix(new RoutingEntry(m));
            ResetMassage(node);



        }

        public void SetNodeName(string s) { node.SetName(s); ResetMassage(node); }




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
