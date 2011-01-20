using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public class RoutingEntry : System.IEquatable<RoutingEntry>
    {
        private int port;
        private int vpi;
        private bool novci;
        private int vci;
        public int Port { get { return port; } set { port = value; } }
        public int Vpi { get { return vpi; } set { vpi = value; } }
        public bool NoVci { get { return novci; } set { novci = value; } }
        public int Vci { get { return vci; } set { vci = value; } }

        public static IEqualityComparer<RoutingEntry> Default
        { get { return (IEqualityComparer<RoutingEntry>)(new EqualityComparer()); } }

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
            if (pvv.Length != 3) throw new ArgumentException();
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
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            if (novci)
                return this.port.ToString() + ";" + this.vpi.ToString() + ";-";
            else
                return this.port.ToString() + ";" + this.vpi.ToString() + ";" + this.vci.ToString();
        }

        public bool Equals(RoutingEntry other)
        {
            if (this.NoVci || other.NoVci)
            {
                if (this.Port == other.Port && this.Vpi == other.Vpi)
                    return true;
                else
                    return false;
            }
            else if (this.Port == other.Port && this.Vpi == other.Vpi && this.Vci == other.Vci)
                return true;
            else
                return false;

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
    

    public class RoutingTable// : Dictionary<RoutingEntry, RoutingEntry>
    {
        private Dictionary<RoutingEntry, RoutingEntry> table;
        private Dictionary<int, RoutingEntry> map;

        public RoutingEntry this[RoutingEntry entry] { get { return table[entry]; } }
        public RoutingEntry this[int id] { get { return map[id]; } }

        public RoutingTable()// : base(new RoutingEntry.EqualityComparer()) { }
        {
            table = new Dictionary<RoutingEntry,RoutingEntry>(new RoutingEntry.EqualityComparer());
            map = new Dictionary<int, RoutingEntry>();
        }

        public RoutingTable(RoutingTable routingTable)// : base((Dictionary<RoutingEntry, RoutingEntry>)routingTable, new RoutingEntry.EqualityComparer()) { }
        {
            table = new Dictionary<RoutingEntry, RoutingEntry>(routingTable.table, new RoutingEntry.EqualityComparer());
            map = new Dictionary<int, RoutingEntry>();
        }

        public void Add(RoutingEntry e1, RoutingEntry e2)
        {
            if (table.ContainsValue(e2))
                throw new ArgumentException();
            table.Add(e1, e2);
        }

        public void Add(RoutingEntry e1, RoutingEntry e2, int id)
        {
            if (table.ContainsValue(e2))
                throw new ArgumentException();
            table.Add(e1, e2);
            map.Add(id, e1);
        }

        public bool Remove(RoutingEntry e)
        {
            bool a = map.ContainsValue(e);
            if (a)
                return false;
            else
                return table.Remove(e);
        }

        public bool Remove(int id)
        {
            if (map.ContainsKey(id))
                table.Remove(map[id]);
            return map.Remove(id);
        }

        public bool ContainsKey(RoutingEntry e)
        {
            return table.ContainsKey(e);
        }

        public bool ContainsKey(int id)
        {
            return map.ContainsKey(id);
        }

        public bool ContainsValue(RoutingEntry e)
        {
            return table.ContainsValue(e);
        }

        public Dictionary<RoutingEntry,RoutingEntry>.Enumerator GetEnumerator()
        {
            return table.GetEnumerator();
        }
    }

}
