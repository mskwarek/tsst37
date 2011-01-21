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
}
