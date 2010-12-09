using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Common
{
    public interface IAgent
    {
        string[] GetParamList();
        string GetParam(string name);
        void SetParam(string name, string value);
        RoutingTable GetRoutingTable();
        void AddRoutingEntry(string label, string value);
        void RemoveRoutingEntry(string entry);
        string GetLog();
    }
}
