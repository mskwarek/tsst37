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
        [XmlElementAttribute("NodeType")]
        public string nodeType { get; set; }

        [XmlArrayItem("PortType")]
        public List<PortType> list {get; set;}

        public Component() {list = new List<PortType>(); }

        //metoda przerabiajaca component na element Node
       public  Object getNode()
        {

            if (nodeType.ToLower() == "node")
            {
                List<PortIn> portIn = new List<PortIn>();
                List<PortOut> portOut = new List<PortOut>();
                foreach (PortType p in list)
                {
                    if (p.type == "Project1.PortIn")
                        portIn.Add(new PortIn(p.portNumber));
                    if (p.type == "Project1.PortOut")
                        portOut.Add(new PortOut(p.portNumber));
                }
                Node node = new Node(portIn.Count, portOut.Count, "Node number" + id);
                node.SetPortsIn(portIn.ToArray());
                node.SetPortsOut(portOut.ToArray());
                return (Node)node;
            }

            switch (nodeType.ToLower())
            {
                case "node":
                    List<PortIn> portIn = new List<PortIn>();
                    List<PortOut> portOut = new List<PortOut>();
                    foreach (PortType p in list)
                    {
                        if (p.type == "Project1.PortIn")
                            portIn.Add(new PortIn(p.portNumber));
                        if (p.type == "Project1.PortOut")
                            portOut.Add(new PortOut(p.portNumber));
                    }
                    Node node = new Node(portIn.Count, portOut.Count, "Node number" + id);
                    node.SetPortsIn(portIn.ToArray());
                    node.SetPortsOut(portOut.ToArray());
                    return (Node)node;
                   
                case "source":
                    Sorce source = new Sorce();
                    if (list.Count == 1 && list[0].type == "Project1.PortOut")
                    {
                        source.SetPortOut(new PortOut(list[0].portNumber));
                        return (Sorce)source;
                    }
                    else return null;
                 case "sink":
                    Sink sink = new Sink();
                   if (list.Count == 1 && list[0].type == "Project1.PortIn")
                    {
                    //    source.SetPortOut(new PortOut(list[0].portNumber));
                       return (Sink)sink;
                    }
                   else return null;
                   
                default:
                    return null;
                    
            }
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
