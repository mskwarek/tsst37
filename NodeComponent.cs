using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
//klasa component sluzy to archiwizowania wlasciwowsci zawartych w obiekcie ppodstawowym Node
namespace AtmSim
{
    [XmlRootAttribute("NodeComponent", Namespace = "", IsNullable = false)]
    public class NodeComponent
    {

        [XmlElementAttribute("Type")]
        public string type { get; set; } //typ wezla, moze byc source, sink lub node, mozna przerobic na enum, ale wteyd bedzie zapisywac do pliku numer a nie stringa      
        [XmlElementAttribute("Name")]
        public string name { get; set; } //nazwa routera
        [XmlElementAttribute("ID")]
        public int id { get; set; }  //identyfikator routera

        [XmlArrayItem("PortInComponent")]
        public List<PortInComponent>  portInComponet {get; set;} //ilosc portow wejsciowych wezla

        [XmlArrayItem("PortOutComponent")]
        public List<PortOutComponent> portOutComponet { get; set; } //ilos portow wyjsciowych routera
        public NodeComponent() { portInComponet = new List<PortInComponent>();
                                  portOutComponet = new List<PortOutComponent>();
                                  name = "";
                                  type = "";
        }

        }


    [XmlRootAttribute("In", Namespace = "", IsNullable = false)]
    public class PortInComponent
    {
        [XmlElementAttribute("ID")]
        public int id { get; set; }
    }

    [XmlRootAttribute("Out", Namespace = "", IsNullable = false)]
    public class PortOutComponent
    {
        [XmlElementAttribute("ID")]
        public int id { get; set; }

    }
    }
  
 

