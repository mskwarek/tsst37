using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    // testowa implementacja portu wejsciowego - dziala w ramach jednego procesu
    public class TestPortIn : IPortIn
    {
        private int portID;
        public int PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private IFrameReceiver receiver;
        public TestPortIn(int id)
        {
            PortID = id;
        }
        public void Receive(string pu) { }
        public void Receive(ProtocolUnit pu)
        {
            receiver.ReceiveFrame(pu, this.PortID);
        }
        public void SetReceiver(IFrameReceiver receiver)
        {
            this.receiver = receiver;
        }
    }
}
