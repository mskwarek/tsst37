using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public interface IPortIn
    {
        int portID
        {
            get;
            set;
        }

        void Receive(string pu);
        void Receive(ProtocolUnit pu);
    }
}
