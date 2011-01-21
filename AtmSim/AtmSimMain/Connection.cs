using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    class LinkConnection
    {
        public int SourceId;
        public int TargetId;
        public string SourceRouting;
        public string TargetRouting;
    }

    class NetworkConnection
    {
        public int Id { get; private set; }
        public List<LinkConnection> Path;
        public bool Active;
    }
}
