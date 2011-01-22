using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public class LinkConnection
    {
        public int SourceId;
        public int TargetId;
        public string SourceRouting; // Port:Vpi:Vci
        public string TargetRouting; // Port:Vpi:Vci
        public Topology.Link Link;
    }

    public class NetworkConnection
    {
        public int Id { get; private set; }
        public int Capacity;
        public List<LinkConnection> Path { get; private set; }
        public bool Active = false;

        public List<int> Nodes
        {
            get
            {
                List<int> nodes = new List<int>();
                if (Path.Count > 0)
                    nodes.Add(Path.First().SourceId);
                foreach (var link in Path)
                    nodes.Add(link.TargetId);
                return nodes;
            }
        }

        public int Source
        {
            get { return Path.First().SourceId; }
        }

        public int Target
        {
            get { return Path.Last().TargetId; }
        }

        public NetworkConnection(int id)
        { 
            Id = id; 
            Path = new List<LinkConnection>();
        }
    }

    public class VirtualPath : Topology.Link
    {
        public int Id { get; private set; }
        public int SourceVpi { get; private set; }
        public int TargetVpi { get; private set; }
        new public string SourceRouting { get { return SourcePort + ":" + SourceVpi + ":"; } }
        new public string TargetRouting { get { return TargetPort + ":" + TargetVpi + ":"; } }
        public List<LinkConnection> Path;

        public VirtualPath(int id, Topology.Node source, Topology.Node target, int sourcePort, int targetPort, int sourceVpi, int targetVpi, int capacity)
            : base(source, target, sourcePort, targetPort, capacity)
        {
            this.Id = id;
            this.SourceVpi = sourceVpi;
            this.TargetVpi = targetVpi;
            this.Path = new List<LinkConnection>();
        }

        new public int Capacity
        {
            get
            {
                int cap = Int32.MaxValue;
                foreach (var link in Path)
                    if (link.Link.Capacity < cap)
                        cap = link.Link.Capacity;
                return cap;
            }
            set
            {
                int diff = value - Capacity;
                foreach (var link in Path)
                    link.Link.Capacity += diff;
            }
        }

    }

}
