using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public class C
    {
        private List<C> nodes;
        public List<C> S { get { return nodes; } }

        private string name;
        public string N
        {
            get { return name; }
            set { name = value; }
        }

        public C()
        {
            nodes = new List<C>();
        }

        public C(string n)
            : this()
        {
            name = n;
        }

        public void Add(C c)
        {
            if (c != this)
                this.S.Add(c);
        }

        public void Add(string n)
        {
            this.S.Add(new C(n));
        }
    }
}
