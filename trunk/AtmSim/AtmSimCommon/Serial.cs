using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

/*Klasa służaca do serializacji obiektu Object ===> stringxml
 *                                      stringxml ====> Object
 */
namespace AtmSim
{
    public static class Serial
    {
        public static String SerializeObject(Object pObject)
        {
            try
            {
                String XmlizedString = null;
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StringWriter writer = new System.IO.StringWriter(sb);
                xs.Serialize(writer, pObject);
                XmlizedString = sb.ToString();
                return XmlizedString;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }
        /*
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);

        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;

        }
        */
        public static Object DeserializeObject(String pXmlizedString, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            StringReader reader = new StringReader(pXmlizedString);
            XmlTextReader xmlTextReader = new XmlTextReader(reader);
            return xs.Deserialize(xmlTextReader);
        } 
    }   

}
