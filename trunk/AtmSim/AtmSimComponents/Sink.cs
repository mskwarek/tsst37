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
        public int Id;
        public string Name;
        private Log log;
        public Log Log { get { return log; } set { log = value; } }

        private SinkAgent agent;
        public IAgent Agent { get { return agent; } }
        
        private PortIn portIn;
        public PortIn PortIn { get { return portIn; } }

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

        public Sink(Config.Node node, int managerPort)
        {
            this.Id = node.Id;
            this.Name = (string)node.Name.Clone();
            this.log = new Log("Log źródła " + Name);
            this.agent = new SinkAgent(this, managerPort);
            portIn = new PortIn(0);
        }
    }
}
