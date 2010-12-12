using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Project1
{
    class ReadWrite
    {
        private FileStream fs;
        private StreamReader sr;
        private StreamWriter sw;
        private String text;
     
       public ReadWrite(){
     
                            }
       public String ReadText(String path) {
           fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
           sr = new StreamReader(fs);
           String buffor = null;
            while((buffor=sr.ReadLine())!= null)
                 { text += buffor; }
        //sw.Close();
                 sr.Close();
                 fs.Close();
             return text;

       }
   


      public void WriteText(String path, String message)
      {
          fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
          sw = new StreamWriter(fs);
          sw.Write(message);
         // writer.Close();
          //file.Close();
          sw.Close();
         // sr.Close();
          fs.Close();
      }


        public void changeString(String text)
        {
            this.text = text;
        }

        public String getString()
        {
            return this.text;
        }
      

    }
}
