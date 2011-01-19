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
       private Topology ownTopology;
       private IEnumerable<Topology.Link> path;
       private String stringToSave = "";
        public RoutingController(Manager manager) 
        {
            this.manager = manager;
            //musi byc kopiowany, bo jak porty beda zajete to trzeba bedzie usunac dana krawedz
            ownTopology = manager.Topology;
            //MyOwnLog.save( this.findBestPath(4, 5).ToString() );
         //   this.setupConnection(4, 5);  for debuging
        }


         public void setupConnection(int src, int trg){
            int index=-1;
             while(ownTopology.EdgeCount!=0){
                 if (this.findBestPath(src, trg))
                 {
                     index = this.askLRMs();
                     if (index < 0) //if true we find the best path
                         break;
                 }
                 else break; //it is imposible to make path
                  ownTopology.RemoveEdge(ownTopology.Edges.ElementAt(index));   //delete edge that is full
             }

             ownTopology = manager.Topology; //recreating graf for new connection
             //TODO:
             //wyslanie wiadomosci do loga ze nie udalo sie zesatwic polaczenia
         }



        private Topology.Node IDtoNode(int id) { 
 
            return ownTopology.Vertices.ToList().Find(delegate(Topology.Node no) { 
                return no.Id == id; 
            });

        }
        
        private Boolean findBestPath(int idSource, int idDestination) {
            Func<Topology.Link, double> edgeCost = e => 1; //koszty lini takie same

            // We want to use Dijkstra on this graph
            var dijkstra = new DijkstraShortestPathAlgorithm<Topology.Node, Topology.Link>(ownTopology, edgeCost);

            // Attach a Vertex Predecessor Recorder Observer to give us the paths
            var predecessorObserver = new VertexPredecessorRecorderObserver<Topology.Node, Topology.Link>();
            predecessorObserver.Attach(dijkstra);
            dijkstra.Compute(this.IDtoNode(idSource));
           
            if (predecessorObserver.TryGetPath(this.IDtoNode(idDestination), out path))
                return true;
            else return false;  
        }

        private Boolean doIHaveAmptyPorts(String response){

            switch (response) {
                case "Yes": return true;
                case "No": return false;
                default: return false;
            }

        }

        public int askLRMs(){
                       
            foreach (var e in path)
            {
              if( true)                               //for debuging
                  return path.ToList().IndexOf(e);   //
            //    if(! doIHaveAmptyPorts( manager.Get(e.Source, "Do you have ampty port IN from ID.source")))
            //    return path.ToList().IndexOf(e);
            //    if(! doIHaveAmptyPorts( manager.Get(e.Target.Id, "Do you have ampty port IN from ID.source")))
            //    return path.ToList().IndexOf(e);
            }

            return -1; //no problems
        }



    }
}
