using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{

    /**
     * Urządzenie odbierające ze swojego portu ruch sieciowy. 
     **/
    public class Sink : INetworkNode
    {
        

        public string Name;
        private Log log;
        public Log Log { get { return log; } set { log = value; } }

        private SinkAgent agent;
        public IAgent Agent
        {
            get { return agent; }
        }
        
        private IPortIn firstport;
        private string message = "";
        private string target = "";
        public string Message { get { return message; } set { message = value; } }
        public string Target { get { return target; } set { target = value; } }
        public  class SinkReceiver : IFrameReceiver
        {   private Sink sink;
            private RoutingTable RouteTable = new RoutingTable();
            public RoutingTable GetRouteTable() { return RouteTable; }
            public SinkReceiver(Sink sink)
                 {
                 this.sink = sink;
                 }
            public void ReceiveFrame(ProtocolUnit pu, int port)
            {   
                RoutingEntry source = new RoutingEntry(port, pu.Vpi, pu.Vci);
                if (RouteTable.ContainsKey(source))
                {
                   
                   sink.Log.LogMsg("Ramka: " + source.ToString());
                }
            }
        }

        

        public SinkReceiver receiver;

        public void SetPortIn(IPortIn po) { firstport = po; } //skojarzenie Sorcea z odpowiednim portem wyjsciowym

        public IPortIn GetPortIn() { return firstport; }
    

        public Sink(String name)
        {
            this.Name = name;
            this.log = new Log("Log źródła " + name);
            agent = new SinkAgent(this);
        
        }
    }
}
