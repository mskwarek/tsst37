using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AtmSim
{
    public class Routing : Dictionary<string, string>, IXmlSerializable
    {
        //private Dictionary<string, string> routing;
        public Routing() : base() { }
        public Routing(Routing routing) : base((Dictionary<string, string>)routing) { }

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
 
        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(string));
 
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
 
            if (wasEmpty)
                return;
 
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
 
                reader.ReadStartElement("key");
                string key = (string)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
 
                reader.ReadStartElement("value");
                string value = (string)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
 
                this.Add(key, value);
 
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }
 
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(string));
 
            foreach (string key in this.Keys)
            {
                writer.WriteStartElement("item");
 
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
 
                writer.WriteStartElement("value");
                string value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
 
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
