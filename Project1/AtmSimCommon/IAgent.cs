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
        void AddRoutingEntry(RoutingEntry label, RoutingEntry value);
        void RemoveRoutingEntry(RoutingEntry entry);
        string GetLog();
    }
}
