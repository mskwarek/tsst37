using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    /**
     * Interfejs dla pola komutacyjnego badz innego elementu, ktory moze odebrac ramke dochodzaca do portu wejsciowego.
     **/
    public interface IFrameReceiver
    {
        void ReceiveFrame(ProtocolUnit pu, int port);
    }
}
