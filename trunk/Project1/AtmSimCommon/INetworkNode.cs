using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public interface INetworkNode
    {
        IAgent Agent { get; }
    }
}
