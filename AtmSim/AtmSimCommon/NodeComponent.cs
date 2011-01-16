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

        [XmlElementAttribute("NumberOfPortIn")]
        public int numberOfPortIn {get; set;} //ilosc portow wejsciowych wezla

        [XmlElementAttribute("NumberOfPortOut")]
        public int numberOfPortOut { get; set; } //ilos portow wyjsciowych routera
        public NodeComponent() { }


   
        }
    }
  
 

