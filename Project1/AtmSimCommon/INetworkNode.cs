using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    /** 
     * Interfejs na potrzeby Managera, pozwala zarzadzac wezlami sieci niezaleznie od typu (router, source, czy sink)
     **/
    public interface INetworkNode
    {
        IAgent Agent { get; }
    }
}
