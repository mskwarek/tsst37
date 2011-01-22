using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using GraphSharp.Controls;

namespace AtmSim
{
    public class Topology : BidirectionalGraph<Topology.Node, Topology.Link>
    {
        public class Node
        {
            public int Id = 0;
            public string Name = "";
            public string Type = "";

            public Node()
            { }

            public Node(int id, string name, string type)
            {
                Id = id;
                Name = name;
                Type = type;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class Link : Edge<Node>
        {
            public int SourcePort { get; private set; }
            public int TargetPort { get; private set; }
            public int Capacity;
            public int MaxCapacity { get; private set; }
            public string SourceRouting { get { return SourcePort + "::"; } }
            public string TargetRouting { get { return TargetPort + "::"; } }

            public Link(Node source, Node target, int sourcePort, int targetPort, int capacity)
                : base(source, target)
            {
                this.SourcePort = sourcePort;
                this.TargetPort = targetPort;
                this.Capacity = this.MaxCapacity = capacity;
            }


        }

        public Topology()
            : base() { }

        public Topology(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public Topology(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }

    public class VirtualPath : Topology.Link
    {
        public int SourceVpi { get; private set; }
        public int TargetVpi { get; private set; }
        new public string SourceRouting { get { return SourcePort + ":" + SourceVpi + ":"; } }
        new public string TargetRouting { get { return TargetPort + ":" + TargetVpi + ":"; } }
        public List<Topology.Link> path;

        public VirtualPath(Topology.Node source, Topology.Node target, int sourcePort, int targetPort, int sourceVpi, int targetVpi, int capacity)
            : base(source, target, sourcePort, targetPort, capacity)
        {
            this.SourceVpi = sourceVpi;
            this.TargetVpi = targetVpi;
        }

        new public int Capacity
        {
            get
            {
                int cap = Int32.MaxValue;
                foreach (var link in path)
                    if (link.Capacity < cap)
                        cap = link.Capacity;
                return cap;
            }
            set
            {
                int diff = value - Capacity;
                foreach (var link in path)
                    link.Capacity += diff;
            }
        }

    }
}
