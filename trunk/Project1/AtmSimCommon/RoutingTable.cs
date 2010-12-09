using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Common
{
    public class RoutingEntry
    {
        private int port;
        private int vpi;
        private int vci;
        public int Port { get { return port; } set { port = value; } }
        public int Vpi { get { return vpi; } set { vpi = value; } }
        public int Vci { get { return vci; } set { vci = value; } }
        public RoutingEntry(int port, int vpi, int vci)
        {
            this.port = port; this.vpi = vpi; this.vci = vci;
        }
    }
    public class RoutingTable : Dictionary<RoutingEntry, RoutingEntry>
    {
        public RoutingTable() : base() { }
        public RoutingTable(RoutingTable routingTable) : base((Dictionary<RoutingEntry, RoutingEntry>)routingTable) { }
    }
}
