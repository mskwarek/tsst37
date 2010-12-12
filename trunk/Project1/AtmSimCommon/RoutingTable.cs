using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public class RoutingEntry
    {
        private int port;
        private int vpi;
        private bool novci;
        private int vci;
        public int Port { get { return port; } set { port = value; } }
        public int Vpi { get { return vpi; } set { vpi = value; } }
        public bool NoVci { get { return novci; } set { novci = value; } }
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
            string[] pvv = entry.Split(';', ':', ',', '.');
            try
            {
                this.port = int.Parse(pvv[0]);
                this.vpi = int.Parse(pvv[1]);
                if (pvv[2] == "-" || pvv[2] == "")
                {
                    this.novci = true;
                    this.vci = 0;
                }
                else
                    this.vci = int.Parse(pvv[2]);
            }
            catch (System.FormatException)
            {
            }
        }

        public override string ToString()
        {
            if (novci)
                return this.port.ToString() + ";" + this.vpi.ToString() + ";-";
            else
                return this.port.ToString() + ";" + this.vpi.ToString() + ";" + this.vci.ToString();
        }

        public class EqualityComparer : IEqualityComparer<RoutingEntry>
        {
            public bool Equals(RoutingEntry e1, RoutingEntry e2)
            {
                if (e1.NoVci || e2.NoVci)
                {
                    if (e1.Port == e2.Port && e1.Vpi == e2.Vpi)
                        return true;
                    else
                        return false;
                }
                else if (e1.Port == e2.Port && e1.Vpi == e2.Vpi && e1.Vci == e2.Vci)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(RoutingEntry entry)
            {
                return entry.Port ^ entry.Vpi;
            }
        }
    }
    

    public class RoutingTable : Dictionary<RoutingEntry, RoutingEntry>
    {
        public RoutingTable() : base(new RoutingEntry.EqualityComparer()) { }
        public RoutingTable(RoutingTable routingTable) : base((Dictionary<RoutingEntry, RoutingEntry>)routingTable, new RoutingEntry.EqualityComparer()) { }
    }

}
