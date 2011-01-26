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
            public virtual string SourceRouting { get { return SourcePort + "::"; } }
            public virtual string TargetRouting { get { return TargetPort + "::"; } }

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
}
