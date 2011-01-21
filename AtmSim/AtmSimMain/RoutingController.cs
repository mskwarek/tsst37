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
        //  this.setupConnection(4, 5, 1);  
        }



        public class SetupStore
        {
            public int source { get; set; }
            public int target { get; set;}
            public Topology ownTopology { get; set; }
            public List<Topology.Link> path { get; set; }
            public List<string> vcivpiList { get; set; }
            public int connectN{ get; set; }
            public SetupStore(int source, int target, Topology ownTopology, int connectN) {
                this.source = source;
                this.target = target;
                this.ownTopology = ownTopology;
                this.connectN = connectN;
                path = new List<Topology.Link>();
                vcivpiList = new List<string>();
               
            }
            public SetupStore() { }
        }


       

         public NetworkConnection setupConnection(int src, int trg,int connectN){
             Topology ownTopology = manager.Topology;//recreating graf for new connection
             SetupStore ss = new SetupStore(src, trg, ownTopology, connectN);
           //  IEnumerable<Topology.Link> path;
          
            int index=-1;
             while(ss.ownTopology.EdgeCount!=0){
                 //ss.path = this.findBestPath(ss);
                 if (this.findBestPath(ss))
                 {
                     index = askLRMs(ss);
                     if (index < 0) //if true we find the best path
                         break;
                 }
                 else break; //it is imposible to make path
                  ss.ownTopology.RemoveEdge(ss.ownTopology.Edges.ElementAt(index));   //delete edge that is full
             }

           //  if(index < 0)    //index =-1 oznacza ze udalo sie zestawic polaczenie
            // this.setupNodes(ss);
          //recreating graf for new connection
             //TODO:
             //wyslanie wiadomosci do loga ze nie udalo sie zesatwic polaczenial;
             return null;
         }



         private Topology.Node IDtoNode(int id, Topology ownTopology)
         { 
 
            return ownTopology.Vertices.ToList().Find(delegate(Topology.Node no) { 
                return no.Id == id; 
            });

        }

         private Boolean findBestPath(SetupStore ss)
        {
          //  Func<Topology.Link, double> edgeCost = e => 1; //koszty lini takie same
            Dictionary<Topology.Link, double> edgeCost = new Dictionary<Topology.Link, double>(ss.ownTopology.EdgeCount);

            foreach (var e in ss.ownTopology.Edges)
            {
                edgeCost.Add(e, e.Capacity);
            }


            // We want to use Dijkstra on this graph
            var dijkstra = new DijkstraShortestPathAlgorithm<Topology.Node, Topology.Link>(ss.ownTopology, e => edgeCost[e]);
         
            // Attach a Vertex Predecessor Recorder Observer to give us the paths
            var predecessorObserver = new VertexPredecessorRecorderObserver<Topology.Node, Topology.Link>();
            predecessorObserver.Attach(dijkstra);
            dijkstra.Compute(this.IDtoNode(ss.source, ss.ownTopology));
            IEnumerable<Topology.Link> path;
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

        public int askLRMs(SetupStore ss){


            string VpiVci = "";
            foreach (var e in ss.path)
            {

                do
                {
                    VpiVci = rand() + "." + rand();
                } while (
                    !doIHaveAmptyPorts(this.Get(e.Source.Id, "PortsOut." + e.SourcePort + ".Available." + VpiVci)) ||
                    !doIHaveAmptyPorts(this.Get(e.Target.Id, "PortsIn." + e.TargetPort + ".Available." + VpiVci))
                    );
                ss.vcivpiList.Add(VpiVci);

            }
            return -1; //no problems
        }

        private void setupNodes(SetupStore ss)
        {
            string label;
            string value;
            int index = 0;
            foreach (var e in ss.path) {
                label =  ss.vcivpiList.ElementAt(index).Split('.')[0];
                value =  ss.vcivpiList.ElementAt(index).Split('.')[1];
                    this.addRouting(e.Source.Id, label, value, ss.connectN);   //to change

                if (index == ss.path.Capacity - 1)
                {
                    label = ss.vcivpiList.ElementAt(index).Split('.')[0];
                    value = ss.vcivpiList.ElementAt(index).Split('.')[1];
                    this.addRouting(e.Source.Id, label, value, ss.connectN);  //to change
                }
                index++;
            }
           
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
