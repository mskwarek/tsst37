using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public interface IFrameReceiver
    {
        void ReceiveFrame(ProtocolUnit pu, int port);
    }
}
