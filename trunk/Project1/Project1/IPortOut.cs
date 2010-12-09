using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public interface IPortOut
    {
        int portID
        {
            get;
            set;
        }
        void Send(string pu);
        void Send(ProtocolUnit pu);
    }
}
