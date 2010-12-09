using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
//klasa component sluzy to archiwizowania wlasciwowsci zawartych w obiekcie ppodstawowym Node
namespace Project1
{
    [XmlRootAttribute("Component", Namespace = "", IsNullable = false)]
    public class Component
    {
        [XmlElementAttribute("Id")]
        public int id { get; set; }
        [XmlArrayItem("PortType")]
        public List<PortType> list {get; set;}

        public Component() {list = new List<PortType>(); }

        //metoda przerabiajaca component na element Node
       public  Node getNode()
        {
       
            List<PortIn> portIn =new List<PortIn>();
            List<PortOut> portOut = new List<PortOut>();
            foreach(PortType p in list)
            {
                if (p.type == "Project1.PortIn")
                    portIn.Add(new PortIn(p.portNumber));
                if (p.type == "Project1.PortOut")
                    portOut.Add(new PortOut(p.portNumber));
            }
            Node node = new Node(portIn.Count, portOut.Count, "Node number" + id);
            node.SetPortsIn(portIn.ToArray());
            node.SetPortsOut(portOut.ToArray());
            return node;
           
        }
    }
    [Serializable]
    public class PortType 
    {   
       [XmlElementAttribute("Type")]
        public String type{ get; set; }
        [XmlElementAttribute("Link")]
       public int link { get; set; }
        [XmlElementAttribute("RealPortNumber")]
        public int portNumber{ get; set; }
    }
 
}
