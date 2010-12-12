using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public class Routing : Dictionary<string, string>
    {
        public Routing() : base() { }
        public Routing(Routing routing) : base((Dictionary<string, string>)routing) { }
    }
}
