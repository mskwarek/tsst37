using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class ProtocolUnit
    {   //czyli juz dataunit z numerami vci vpi

        private int vpi;

        private int vci;

        private DataUnit datau;

        private Random rand = new Random();  //do generowania vci vpi randomowych..pewnie bedzie nie potrzebne ale napisalem



        public int GetVCI()
        { return vci; }

        public void SetVCI(int vci)
        { this.vci = vci; }

        public int SetRandomVCI()
        { return this.vci = (int)rand.Next((int)Math.Pow(2, 16)); }

        public int GetVPI()
        { return vpi; }

        public void SetVPI(int vpi)
        { this.vpi = vpi; }

        public int SetRandomVPI()
        { return this.vpi = (int)rand.Next((int)Math.Pow(2, 12)); }

        public DataUnit GetDataUnit() { return datau; }

        public void SetDataUnit(DataUnit d) { datau = d; }

    }
}
