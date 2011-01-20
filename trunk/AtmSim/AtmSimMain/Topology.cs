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

            public Node()
            { }

            public Node(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class Link : Edge<Node>
        {
            public int SourcePort;
            public int TargetPort;
            public Link(Node source, Node target, int sourcePort, int targetPort)
                : base(source, target)
            {
                this.SourcePort = sourcePort;
                this.TargetPort = targetPort;
            }
        }

        public Topology() { }

        public Topology(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public Topology(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }

}
