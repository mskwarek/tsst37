using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    /**
     * Urządzenie odbierające ze swojego portu ruch sieciowy. 
     **/
    public class Sink
    {
        private int id;
        public int Id { get { return id; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private Log log;
        public Log Log { get { return log; } }

        private SinkAgent agent;
        public SinkAgent Agent { get { return agent; } }
        
        private PortIn portIn;
        public PortIn PortIn { get { return portIn; } }

        public class SinkReceiver : IFrameReceiver
        {  
            private Sink sink;
            private Dictionary<RoutingEntry, string> sources = new Dictionary<RoutingEntry, string>(new RoutingEntry.EqualityComparer());
            public Dictionary<RoutingEntry, string> Sources { get { return sources; } set { sources = value; } }
            public String Buffer;

            public SinkReceiver(Sink sink)
            {
                this.sink = sink;    
            }

            public void ReceiveFrame(ProtocolUnit pu, int port)
            {   
                RoutingEntry source = new RoutingEntry(port, pu.Vpi, pu.Vci);
                if (sources.ContainsKey(source))
                {
                    sink.log.LogMsg("Wiadomosc " + pu.DataUnit.Id + " z polaczenia " + sources[source]);
                    Buffer += pu.DataUnit.Id;
                }
                else
                    sink.Log.LogMsg("Ramka: " + source.ToString());
           }
        }

        private SinkReceiver receiver;
        public SinkReceiver Receiver { get { return receiver; } }

        public Sink(Config.Node node, int managerPort)
        {
            this.id = node.Id;
            this.name = (string)node.Name.Clone();
            this.log = new Log("Log urzadzenia " + Name);
            this.agent = new SinkAgent(this, managerPort);
            portIn = new PortIn(0);
            receiver = new SinkReceiver(this);
            portIn.SetReceiver(receiver);
        }
    }
}
