using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
namespace AtmSim.Config
{ 
    /**
     * Konfiguracja sieci
     */
    //[XmlRootAttribute("Network", Namespace = "", IsNullable = false)]
    public class Network
    {
        private string name = "";
        //[XmlElementAttribute("Name")]
        public string Name { get { return name; } set { name = value; } }

        private List<Node> nodes = new List<Node>();
        //[XmlArrayItem("Nodes")]
        public List<Node> Nodes { get { return nodes; } set { nodes = value; } }

        private List<Link> links = new List<Link>();
        //[XmlArrayItem("Link")]
        public List<Link> Links { get { return links; } set { links = value; } }

      //ta wartosc bedziemy wysylac do procesu wezla podczas inicjalizacji
      //moge dorobic jescze informacje ktory port z ktorym ma sie zestawic, narazie trzeba by to robic recznie
        public String getConfigurationToNode(int count)
        {
            if (count < nodes.Capacity)
                return Serial.SerializeObject(nodes[count]);
            else return null;
        }

        //zapisanie konfigracji
        public void writeFile(string filename)
        {
            // create a writer and open the file
            TextWriter tw = new StreamWriter(filename);
          
            // write a line of text to the file
            tw.Write(Serial.SerializeObject(this));

            // close the stream
            tw.Close();
        }
         //wczytanie konfiguracji
        public void readFile(string filename)
        {
            TextReader tr = new StreamReader(filename);
            String str = "";
            String input = null;

            while ((input = tr.ReadLine()) != null)
            {
                str += input;
            }
            tr.Close();
            Network comp = ((Network)Serial.DeserializeObject(str, typeof(Network)));
            this.nodes = comp.nodes;
            this.links = comp.links;
            this.name = comp.name;
            //this.managerPort = comp.managerPort;
        }

    }

     // polaczenia miedzy wezlami - id wezlow i portow poczatkowych i koncowych
     //[XmlRootAttribute("Link", Namespace = "", IsNullable = false)]
     public class Link
     {
         //[XmlElementAttribute("StartNode")]
         public int StartNode { get; set; }
         //[XmlElementAttribute("StartPort")]
         public int StartPort { get; set; }
         //[XmlElementAttribute("EndNode")]
         public int EndNode { get; set; }
         //[XmlElementAttribute("EndPort")]
         public int EndPort { get; set; }
     }
}
