using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;

namespace AtmSim
{
    public class RoutingGraph : BidirectionalGraph<RoutingGraph.Vertex, RoutingGraph.Edge>
    {
        public class Vertex
        {
            public Topology.Node Node { get; private set; }
            public int Id { get { return Node.Id; } }

            public Vertex(Topology.Node node)
            {
                Node = node;
            }
        }

        public class Edge : Edge<Vertex>
        {
            public Topology.Link Link { get; private set; }
            public int Capacity { get { return Link.Capacity; } }
            public Edge(Vertex source, Vertex target, Topology.Link link)
                : base(source, target)
            {
                Link = link;
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
            Dictionary<Topology.Node, Vertex> vertices = new Dictionary<Topology.Node,Vertex>();
            foreach (Topology.Node node in topology.Vertices)
                graph.AddVertex(new Vertex(node));
            foreach (Topology.Link link in topology.Edges)
                if (link.Capacity >= minCapacity)
                    graph.AddEdge(new Edge(vertices[link.Source], vertices[link.Target], link));
            return graph;
        }
    }
}
