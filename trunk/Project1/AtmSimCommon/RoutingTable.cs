using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Common
{
    public class RoutingEntry// : IEquatable<RoutingEntry>
    {
        private int port;
        private int vpi;
        private int vci;
        public int Port { get { return port; } set { port = value; } }
        public int Vpi { get { return vpi; } set { vpi = value; } }
        public int Vci { get { return vci; } set { vci = value; } }

        public RoutingEntry(RoutingEntry entry)
        {
            this.port = entry.Port;
            this.vpi = entry.Vpi;
            this.vci = entry.Vci;
        }

        public RoutingEntry(int port, int vpi, int vci)
        {
            this.port = port; this.vpi = vpi; this.vci = vci;
        }

        public RoutingEntry(string entry)
        {
            string[] pvv = entry.Split(';');
            try
            {
                this.port = int.Parse(pvv[0]);
                this.vpi = int.Parse(pvv[1]);
                this.vci = int.Parse(pvv[2]);
            }
            catch (System.FormatException)
            {
            }
        }

        public override string ToString()
        {
            return this.port.ToString() + ";" + this.vpi.ToString() + ";" + this.vci.ToString();
        }

    }

    public class RoutingTable : Dictionary<string, string>
    {
        public RoutingTable() : base() { }
        public RoutingTable(RoutingTable routingTable) : base((Dictionary<string, string>)routingTable) { }
    }

}
