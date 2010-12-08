using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

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
