using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    class LinkConnection
    {
        public int SourceId;
        public int TargetId;
        public string SourceRouting; // Port:Vpi:Vci
        public string TargetRouting; // Port:Vpi:Vci
    }

    class NetworkConnection
    {
        public int Id { get; private set; }
        public List<LinkConnection> Path;
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
        public bool Active;
    }
}
