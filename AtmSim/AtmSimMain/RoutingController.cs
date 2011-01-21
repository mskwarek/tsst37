using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms;

namespace AtmSim
{
    class RoutingController
    {

        private Manager manager;
        private String strDebug="";
        private Random random = new Random();
        public RoutingController(Manager manager) 
        {
            this.manager = manager;
            //musi byc kopiowany, bo jak porty beda zajete to trzeba bedzie usunac dana krawedz
            
            //MyOwnLog.save( this.findBestPath(4, 5).ToString() );
            this.setupConnection(4, 5, 2, 2);
        }



        public class SetupStore
        {
            public int source { get; set; }
            public int target { get; set;}
            public RoutingGraph ownTopology { get; set; }
            public List<RoutingGraph.Link> path { get; set; }
            public List<string> vcivpiList { get; set; }
            public int connectN{ get; set; }
            public int requieredCapacity { get; set; }
            public SetupStore(int source, int target, RoutingGraph ownTopology, int connectN, int requieredCapacity)
            {
                this.source = source;
                this.target = target;
                this.ownTopology = ownTopology;
                this.connectN = connectN;
                path = new List<RoutingGraph.Link>();
                vcivpiList = new List<string>();
                this.requieredCapacity = requieredCapacity;
               
            }
            public SetupStore() { }
        }




        public NetworkConnection setupConnection(int src, int trg, int connectN, int requieredCapacity)
        {
            RoutingGraph ownTopology = RoutingGraph.MapTopology(manager.Topology, requieredCapacity); //TODO make clone method
             SetupStore ss = new SetupStore(src, trg, ownTopology, connectN, requieredCapacity);
             if(ss.ownTopology.EdgeCount!=0){
                 if (this.findBestPath(ss))  //if true w
                 {
                     this.askLRMs(ss); //creating list vcivpi
                 }

                 return this.parseToNetworConnection(ss);
             
                 //networkConnection.
                
             }

            
             return null;
         }


        private NetworkConnection parseToNetworConnection(SetupStore ss)
        { NetworkConnection networkConnection = new NetworkConnection(ss.connectN);
          //  List<LinkConnection> links = new List<LinkConnection>();
            LinkConnection link; 
      
             for(int i = 0 ; i < ss.path.Count; i++)
             {
               link = new LinkConnection();
               link.SourceId = ss.path[i].Source.Id;
               link.TargetId = ss.path[i].Target.Id;
               link.SourceRouting = ss.path[i].tLink.SourcePort + ":" + ss.vcivpiList[i];
               link.TargetRouting = ss.path[i].tLink.TargetPort + ":" + ss.vcivpiList[i];
               networkConnection.Path.Add(link);
             
             }
             return networkConnection;
            
            }




        private RoutingGraph.Node IDtoNode(int id, RoutingGraph ownTopology)
         {

             return ownTopology.Vertices.ToList().Find(delegate(RoutingGraph.Node no)
             { 
                return no.Id == id; 
            });

        }

         private Boolean findBestPath(SetupStore ss)
        {
          //  Func<Topology.Link, double> edgeCost = e => 1; //koszty lini takie same
            Dictionary<RoutingGraph.Link, double> edgeCost = new Dictionary<RoutingGraph.Link, double>(ss.ownTopology.EdgeCount);
        
            int index = 0;
            int max = ss.ownTopology.EdgeCount;
            while (index < max)
            {       //free capisity < requierd
                if (ss.ownTopology.Edges.ElementAt(index).Capacity < ss.requieredCapacity)
                {
                    ss.ownTopology.RemoveEdge(ss.ownTopology.Edges.ElementAt(index));
                    max = ss.ownTopology.EdgeCount;
                } else
                index++;
            }
           foreach (var e in ss.ownTopology.Edges)
            {
                edgeCost.Add(e, e.Capacity);
     
            }


            // We want to use Dijkstra on this graph
           var dijkstra = new DijkstraShortestPathAlgorithm<RoutingGraph.Node, RoutingGraph.Link>(ss.ownTopology, e => edgeCost[e]);
         
            // Attach a Vertex Predecessor Recorder Observer to give us the paths
            var predecessorObserver = new VertexPredecessorRecorderObserver<RoutingGraph.Node, RoutingGraph.Link>();
            predecessorObserver.Attach(dijkstra);
            dijkstra.Compute(this.IDtoNode(ss.source, ss.ownTopology));
            IEnumerable<RoutingGraph.Link> path;
            //List<Topology.Link> ddd = new List<Topology.Link>();
            if (predecessorObserver.TryGetPath(this.IDtoNode(ss.target, ss.ownTopology), out path))
            { 
                ss.path.AddRange(path);
                return true;
            }
            else return false;  
        }

        private Boolean doIHaveAmptyPorts(String response){

            switch (response) {
                case "true": return true;
                case "false": return false;
                default: return false;
            }
           
        }

        public void askLRMs(SetupStore ss){


            string VpiVci = "";
            foreach (var e in ss.path)
            {

                do
                {
                    VpiVci = rand() + "." + rand();
                } while (
                    !doIHaveAmptyPorts(this.Get(e.Source.Id, "PortsOut." + e.tLink.SourcePort + ".Available." + VpiVci)) ||
                    !doIHaveAmptyPorts(this.Get(e.Target.Id, "PortsIn." + e.tLink.TargetPort + ".Available." + VpiVci))
                    );
                ss.vcivpiList.Add(VpiVci);

            }
            //no problems
        }

    

        private String Get(int id,  String str) //debugging
        {
            return "true";
        }


        private void addRouting(int id, String lable, String value, int idNumber){} //debugging


        private String rand(){
           
            int num = random.Next()%100;
            return Convert.ToString(num);
        }
    }
}
