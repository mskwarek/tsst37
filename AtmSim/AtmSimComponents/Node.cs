using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class Node : INetworkNode
    {    //pojedynczy węzeł sieci, nie generuje ruchu, tylko go kieruje
        private NodeAgent a;
        public IAgent Agent
        {
            get { return a; }
        }

        private Log log;
        public Log Log
        {
            get { return log; }
        }

        private string name = "unknown";

        private PortIn[] inportsgroup; //porty wejsciowe wezla
        public PortIn[] PortsIn { get { return inportsgroup; } }
        private PortOut[] outportsgroup; //porty wyjsciowe wezla
        public PortOut[] PortsOut { get { return outportsgroup; } }
        private Matrix tab; //= new Matrix();  //Matrix wezla czyli tablica routingu
        public Matrix Matrix { get { return tab; } }

        public string Name { get { return name; } set { name = value; } }   //zwraca imie wezla
 
 //       public IPortIn[] GetPortsIn() { return inportsgroup; } //metoda zwraca porty wejsciowe
 //       public IPortOut[] GetPortsOut() { return outportsgroup; }  //metoda zwraca porty wyjsciowe

 //       public void SetPortsIn(IPortIn[] pi) { inportsgroup = pi; } //metoda ustawia porty wejsciowe
 //       public void SetPortsOut(IPortOut[] po) { outportsgroup = po; } //metoda ustawia porty wyjsciowe

 //       public Matrix GetMatrix() { return tab; }  //zwraca Matrix-pole komutacyjne węzla
 //       public void SetMatrix(Matrix m) { tab = m; } //Ustawienie pola kom. dla węzla

        public Node(int numberofin, int numberofout, string name, int managerPort)
        {
            this.name = name;

            inportsgroup = new PortIn[numberofin];
            outportsgroup = new PortOut[numberofout];
            tab = new Matrix(this);

            for (int i = 0; i < inportsgroup.Length; i++)
            {
                PortIn port = new PortIn(i);
                port.SetReceiver(this.tab);
                inportsgroup[i] = port;
            }
            for (int j = 0; j < outportsgroup.Length; j++) 
            {
                outportsgroup[j] = new PortOut(j);
            }

            this.log = new Log("Log urzadzenia " + name);
            this.a = new NodeAgent(this, managerPort);
        }
    }
}