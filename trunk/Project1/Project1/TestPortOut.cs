using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class TestPortOut : IPortOut
    {
        private int _portID;
        public int portID
        {
            get { return _portID; }
            set { _portID = value; }
        }
        private IPortIn remotePort;
        public TestPortOut(int id)
        {
            portID = id;
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
