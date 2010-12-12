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
        public IAgent Agent { get { return agent; } }
        
        private IPortIn firstport;

        public  class SinkReceiver : IFrameReceiver
        {  
            private Sink sink;
            private Dictionary<RoutingEntry, string> sources = new Dictionary<RoutingEntry, string>(new RoutingEntry.EqualityComparer());
            public Dictionary<RoutingEntry, string> Sources { get { return sources; } set { sources = value; } }

            public SinkReceiver(Sink sink)
            {
                this.sink = sink;    
            }

            public void ReceiveFrame(ProtocolUnit pu, int port)
            {   
                RoutingEntry source = new RoutingEntry(port, pu.Vpi, pu.Vci);
                if (sources.ContainsKey(source))
                    sink.log.LogMsg("Wiadomosc " + pu.DataUnit.Id + " z polaczenia " + sources[source]);
                else
                    sink.Log.LogMsg("Ramka: " + source.ToString());
           }
        }

        private SinkReceiver receiver;
        public SinkReceiver Receiver { get { return receiver; } }

        public void SetPortIn(IPortIn po) { firstport = po; } //skojarzenie Sorcea z odpowiednim portem wyjsciowym
        public IPortIn GetPortIn() { return firstport; }
    
        public Sink(String name)
        {
            this.Name = name;
            this.log = new Log("Log źródła " + name);
            agent = new SinkAgent(this);
            receiver = new SinkReceiver(this);
            firstport = new TestPortIn(0);
        }
    }
}
