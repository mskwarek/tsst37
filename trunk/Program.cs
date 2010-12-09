using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project1;
using System.Threading;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
           Node node = new Node();

           ProtocolUnit protocolUnit = new ProtocolUnit();
           protocolUnit.SetRandomVCI();
           protocolUnit.SetRandomVPI();
            List<PortIn> listPortIn = new List<PortIn>();
            listPortIn.Add(new PortIn(13002));
            listPortIn.Add(new PortIn(13003));

            List<PortOut> listPortOut = new List<PortOut>();
            listPortOut.Add(new PortOut(13000));
            listPortOut.Add(new PortOut(13001));

            Thread thread1 = new Thread(new ThreadStart(listPortIn[0].listener));
            Thread thread2 = new Thread(new ThreadStart(listPortIn[1].listener));
            Thread thread3 = new Thread(new ThreadStart(listPortOut[0].listener));
            Thread thread4 = new Thread(new ThreadStart(listPortOut[1].listener));
           thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
 
            listPortOut[0].protuni = protocolUnit;
            listPortOut[0].isOccupied = true;
             listPortOut[1].isOccupied = true;

            Console.ReadLine();


        }
    }
}
