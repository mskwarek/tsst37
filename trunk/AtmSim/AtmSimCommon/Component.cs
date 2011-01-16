using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
namespace AtmSim.Components
{ /**
   * Clasa komponent ktora czyta Manager
   * 
   */
     [XmlRootAttribute("Component", Namespace = "", IsNullable = false)]
   public class Component
    {
      
        
        [XmlElementAttribute("Name")]
        public string name { get; set; }
        [XmlElementAttribute("ManagerPort")]
        public int managerPort { get; set; }
        [XmlArrayItem("NodeComponent")]
        public List<NodeComponent> nodeComponent { get; set; }
        [XmlArrayItem("Link")]
        public List<Link> link { get; set; }

       public Component() { nodeComponent = new List<NodeComponent>();
                            link = new List<Link>();
                          }

      //ta wartosc bedziemy wysylac do procesu wezla podczas inicjalizacji
      //moge dorobic jescze informacje ktory port z ktorym ma sie zestawic, narazie trzeba by to robic recznie
        public String getConfigurationToNode(int count)
        {
            if (count < nodeComponent.Capacity)
                return Serial.SerializeObject(nodeComponent[count]);
            else return null;
        }

        //zapisanie konfigracji
        public void writeFile()
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter("Configuration.xml");
          
            // write a line of text to the file
            tw.Write(Serial.SerializeObject(this));

            // close the stream
            tw.Close();
        }
         //wczytanie konfiguracji
        public void readFile()
        {
            TextReader tr = new StreamReader("Configuration.xml");
            String str = "";
            String input = null;

            while ((input = tr.ReadLine()) != null)
            {
                str += input;
            }
            tr.Close();
            Component comp = ((Component)Serial.DeserializeObject(str, typeof(Component)));
            this.nodeComponent = comp.nodeComponent;
            this.link = comp.link;
            this.name = comp.name;
            this.managerPort = comp.managerPort;
        }

    }

    //ktory router ma sie zestawic z ktorym, informacja dla gui
     [XmlRootAttribute("Link", Namespace = "", IsNullable = false)]
     public class Link
     {
         [XmlElementAttribute("LinkStart")]
         public String linkStart { get; set; }
          [XmlElementAttribute("LinkEnd")]
         public String linkEnd { get; set; }


         public Link(){}
         public Link(String linkStart, String linkEnd) { this.linkStart = linkStart; this.linkEnd = linkEnd; }
     }
}
