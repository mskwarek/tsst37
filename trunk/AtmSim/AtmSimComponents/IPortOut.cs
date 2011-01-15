using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    // Interfejs portu wyjsciowego
    public interface IPortOut
    {
        int PortID
        {
            get;
            set;
        }
        void Send(string pu);
        void Send(ProtocolUnit pu);
    }
}
