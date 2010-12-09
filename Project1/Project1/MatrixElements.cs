using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    public class MatrixElements //: IEquatable<MatrixElements>
    {
        private int portnum;

        private int vpi;

        private int vci;



        public MatrixElements(int portnum, int vpi, int vci)
        {
            this.vpi = vpi;
            this.vci = vci;
            this.portnum = portnum;
        }

        public bool Equals(MatrixElements me)
        {
            if (me.portnum == portnum && me.vpi == vpi && me.vci == vci)
                return true;
            else
                return false;


        }
        public int GetPortNumber() { return portnum; }

        public int GetVPI() { return vpi; }

        public int GetVCI() { return vci; }

        public void SetPortNumber(int portnum) { this.portnum = portnum; }

        public void SetVPI(int vpi) { this.vpi = vpi; }

        public void SetVCI(int vci) { this.vci = vci; }

    }
}
