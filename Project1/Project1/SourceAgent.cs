using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class SorceAgent : AtmSim.Common.IAgent
    {

        ArrayList informacionlist = new ArrayList();

        Sorce x;

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


        public Agent(Sorce n)
        {
            x = n;
            //this.ResetMassage(x);
        }




        public void SetNodeName(string s) { x.SetName(s); ResetMassage(x); }



        public string[] GetParamList()
        {
            string[] param = { "name","message" } ;
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return x.GetName();
            else if (name =="message")
                return x.GetCurrentData();
 
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                x.SetName(value);
            else if(name=="message")
                if(value=="random"){x.message=null; }
                else {x.message=value;}

        }

        public AtmSim.Common.RoutingTable GetRoutingTable()
        {
            AtmSim.Common.RoutingTable table = new AtmSim.Common.RoutingTable();
            foreach (MatrixElements element in x.GetMatrix().GetRouteTable())
            {
                MatrixElements value = (MatrixElements)x.GetMatrix().GetRouteTable()[element];
                table.Add(new Common.RoutingEntry(element.Port, element.Vpi, element.Vci),
                    new Common.RoutingEntry(value.Port, value.Vpi, value.Vci));
            }
            return table;
        }
        public void AddRoutingEntry(AtmSim.Common.RoutingEntry label, AtmSim.Common.RoutingEntry value)
        {
            x.GetMatrix().AddToMatrix(new MatrixElements(label.Port, label.Vpi, label.Vci),
                new MatrixElements(value.Port, value.Vpi, value.Vci));
        }
        public void RemoveRoutingEntry(AtmSim.Common.RoutingEntry entry)
        {
            x.GetMatrix().DeleteFromMatrix(new MatrixElements(entry.Port, entry.Vpi, entry.Vci));
        }
        public string GetLog()
        {
            return x.Log.GetString();
        }


    }
}
