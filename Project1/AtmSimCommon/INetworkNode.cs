using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Common
{
    public interface INetworkNode
    {
        IAgent Agent { get; }
    }
}
