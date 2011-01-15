using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    // testowa implementacja portu wyjsciowego - dziala w ramach jednego procesu
    public class TestPortOut : IPortOut
    {
        private int portID;
        public int PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private IPortIn remotePort;
        public TestPortOut(int id)
        {
            PortID = id;
        }
        public void Send(string pu)
        {
            remotePort.Receive(pu);
        }
        public void Send(ProtocolUnit pu)
        {
            remotePort.Receive(pu);
        }
        public void Connect(IPortIn port)
        {
            remotePort = port;
        }
    }
}
