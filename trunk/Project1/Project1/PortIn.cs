using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AtmSim.Components
{

    public class PortIn // : IPortIn
    {               //reprezentacja portu wejściowego, realizuje otrzymywanie danych 
        private static int counting = 0;
        private int number=0;      //numer identyfikujacy port wejsciowy

        private ProtocolUnit protuni;

        private bool isreceived = false;    //wartosc logiczna okreslajaca czy wezel ma do obsluzenia(mapowanie w poku kom.) pakiet.Jezeli tak to "true" jezeli nie to "false".

        public PortIn() { number = counting; counting++; }


        public int GetNumber() { return number; }

        public void SetNumber(int i) { number = i; }


        private Int32 realPort;
        public PortIn(Int32 realPort)
        {
            this.realPort = realPort;
        
        }


        private void Receive(String xmlString)
        {        
         
         protuni = (ProtocolUnit)Serial.DeserializeObject(xmlString.Substring(1,xmlString.Length-1),typeof(ProtocolUnit));
          //Console.WriteLine(protuni.ToString());
          isreceived = true; 
        } //otrzymanie pakietu przez port wejsciowy i zamiana wartosci logicznej na "true".

        public bool GetIsReceived() { return isreceived; }  //tu poprostu pobieramy wartosc  logiczna isreceived.

        public void SetIsReceived(bool b) { isreceived = b; }// metoda dla wezla. Po obsluzeniu pakietu musi ustawic z powrotem wartosc logicza false.

        public ProtocolUnit GetProtocolUnit() { return protuni; } //zwaca protocol unit



        public void listener()
        {

            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, realPort);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("PortIn is waiting for a connection... \n");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected! \n");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("PortIn recieved: {0}\n", data);
                        lock (this)
                        {
                            this.Receive(data);
                        }
                        // Process the data sent by the client.
                       

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("Data was recived by PortIn...");

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Send confirmation...\n");
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();

        }
    }







}
