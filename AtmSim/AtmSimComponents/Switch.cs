using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class Switch
    {
        private int id;
        public int Id { get { return id; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private SwitchAgent agent;
        public SwitchAgent Agent { get { return agent; } }

        private Log log;
        public Log Log { get { return log; } }

        private PortIn[] inportsgroup; //porty wejsciowe wezla
        public PortIn[] PortsIn { get { return inportsgroup; } }

        private PortOut[] outportsgroup; //porty wyjsciowe wezla
        public PortOut[] PortsOut { get { return outportsgroup; } }

        private Matrix tab; //= new Matrix();  //Matrix wezla czyli tablica routingu
        public Matrix Matrix { get { return tab; } }
 
        public Switch(Config.Node node, int managerPort)
        {
            this.id = node.Id;
            this.name = (string)node.Name.Clone();

            inportsgroup = new PortIn[node.PortsIn.Count];
            outportsgroup = new PortOut[node.PortsOut.Count];
            tab = new Matrix(this);

            foreach (Config.PortIn portIn in node.PortsIn)
            {
                PortIn port = new PortIn(portIn.Id);
                port.SetReceiver(this.tab);
                inportsgroup[portIn.Id] = port;
            }

            foreach (Config.PortOut portOut in node.PortsOut)
            {
                outportsgroup[portOut.Id] = new PortOut(portOut.Id);
            }

            this.log = new Log("Log urzadzenia " + name);
            this.agent = new SwitchAgent(this, managerPort);            
        }
    }
}