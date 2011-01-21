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
            public int SourcePort;
            public int TargetPort;
            public int Capacity;
            public int MaxCapacity;
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

    public class VirtualPath// : Topology.Link
    {
        public int SourceVpi;
        public int TargetVpi;
    }
}
