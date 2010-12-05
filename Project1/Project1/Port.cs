using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Project1
{
   public class Port : IPort
    {
        public static string messagez = null;
        public static  bool isOcuppiedz = false;
        public static Component component;
        public static Node node;
        public Port()
        {
         
        }
        public void Recieve(int idPortu, string message)
        {
           
                messagez = message;
                isOcuppiedz = true;


            }
                
        

      
    }
}
