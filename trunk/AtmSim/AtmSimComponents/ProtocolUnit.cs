using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
namespace AtmSim.Components
{ 
    //czyli juz dataunit z numerami vci vpi
    public class ProtocolUnit
    {  
        private int vpi;
        private int vci;
        private DataUnit datau;

        public int Vpi{
        set{ vpi= value;}
        get { return vpi; }
        }

        public int Vci
        {
            set { vci = value; }
            get { return vci; }
        }

        public DataUnit DataUnit 
        {
            set {datau=value;}
            get { return datau; }
        }
    }
}
