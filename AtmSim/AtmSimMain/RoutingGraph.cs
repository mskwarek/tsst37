using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;

namespace AtmSim
{
    public class RoutingGraph : BidirectionalGraph<RoutingGraph.Node, RoutingGraph.Link>
    {
        public class Node
        {
            public Topology.Node tNode { get; private set; }
            public int Id { get { return tNode.Id; } }

            public Node(Topology.Node node)
            {
                tNode = node;
            }
        }

        public class Link : Edge<Node>
        {
            public Topology.Link tLink { get; private set; }
            public int Capacity { get { return tLink.Capacity; } }
            public Link(Node source, Node target, Topology.Link link)
                : base(source, target)
            {
                tLink = link;
            }
        }

        public RoutingGraph()
            : base() { }

        public RoutingGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public RoutingGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }

        public static RoutingGraph MapTopology(Topology topology, int minCapacity)
        {
            RoutingGraph graph = new RoutingGraph();
            Dictionary<Topology.Node, Node> vertices = new Dictionary<Topology.Node,Node>();
            foreach (Topology.Node node in topology.Vertices)
                graph.AddVertex(new Node(node));
            foreach (Topology.Link link in topology.Edges)
                if (link.Capacity >= minCapacity)
                    graph.AddEdge(new Link(vertices[link.Source], vertices[link.Target], link));
            return graph;
        }
    }
}
