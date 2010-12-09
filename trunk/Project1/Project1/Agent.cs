using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    public class Agent
    {

        ArrayList informacionlist = new ArrayList();

        Node x;

        private string massage = "unknown";



        private void ResetMassage(Node n)
        {

            massage = "Node Name :" + n.GetName().ToString() + "\n\n";

            int count = 0;
            massage += "All Out Ports :\n";
            foreach (PortOut p in n.GetPortsOut())
            {
                count++;
                massage += count.ToString() + ": Out Port Number :" + p.GetNumber().ToString() + "\n";

            }
            count = 0;
            massage += "All In Ports :\n";
            foreach (PortIn i in n.GetPortsIn())
            {
                count++;
                massage += count.ToString() + ": In Port Number :" + i.GetNumber() + "\n";

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


        }






        public string GetParametersOfNode() { return massage; }


        public Agent(Node n)
        {
            x = n;
            this.ResetMassage(x);
        }


        public void AddRowToRouteTable(MatrixElements m1, MatrixElements m2)
        {

            int count = 0;
            foreach (PortOut po in x.GetPortsOut())
            {

                if (m1.GetPortNumber() == po.GetNumber()) count++;

            }
            foreach (PortIn pi in x.GetPortsIn())
            {

                if (m2.GetPortNumber() == pi.GetNumber()) count++;

            }


            if (count == 2)
            {

                x.GetMatrix().AddToMatrix(m1, m2);

                ResetMassage(x);
            }


        }

        public void DeleteRowFromRouteTable(MatrixElements m)
        {

            x.GetMatrix().DeleteFromMatrix(m);
            ResetMassage(x);



        }

        public void SetNodeName(string s) { x.SetName(s); ResetMassage(x); }





    }
}
