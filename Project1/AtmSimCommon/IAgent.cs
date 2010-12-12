using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public interface IAgent
    {
        string[] GetParamList();
        string GetParam(string name);
        void SetParam(string name, string value);
        Routing GetRoutingTable();
        void AddRoutingEntry(string label, string value);
        void RemoveRoutingEntry(string entry);
        string GetLog();
    }
}
