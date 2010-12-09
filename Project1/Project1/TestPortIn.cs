using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    public class TestPortIn : IPortIn
    {
        private int _portID;
        public int portID
        {
            get { return _portID; }
            set { _portID = value; }
        }
        private IFrameReceiver receiver;
        public TestPortIn(int id)
        {
            portID = id;
        }
        public void Receive(string pu) { }
        public void Receive(ProtocolUnit pu)
        {
            receiver.ReceiveFrame(pu, this.portID);
        }
        public void SetReceiver(IFrameReceiver receiver)
        {
            this.receiver = receiver;
        }
    }
}
