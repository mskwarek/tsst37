using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{

    /*Urządzenie odbierające ze swojego portu ruch sieciowy 
 * 
 * 
 */
    public class Sink// : AtmSim.Common.INetworkNode
    {
        class SinkReceiver : IFrameReceiver
        {
            public void ReceiveFrame(ProtocolUnit pu, int port)
            {

            }
        }

        

        private SinkReceiver receiver;
        private TestPortIn lastport;

        public Sink(TestPortIn port)
        {
            receiver = new SinkReceiver();
            lastport = port;
            lastport.SetReceiver(receiver);
        }
    }
}
