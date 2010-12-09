using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AtmSim.Components
{
   public class PortOut
   {
       private static int counting = 0;

       public PortOut() { number = counting; counting++; }

       public int GetNumber() { return number; }

       public void SetNumber(int i) { number = i; }

       public int GetCounting() { return counting; }
       // reprezancacja portu wyjściowego, realizuje przekazanie danych do portu po drugiej stronie łącza 
       public bool isOccupied;

       public ProtocolUnit protuni{get; set;}

        private int number=0; //numer identyfikujacy port wyjsciowy.

        public void Send(ProtocolUnit pu) { }  // metoda ktora bedzie wysylala ProtocolUnit ktory jej podamy na wejscie.
        
       private Int32 realPort;

       public PortOut(Int32 realPort)
       {
        this.realPort = realPort;
        isOccupied = false;
       }
       
       private  void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.

                TcpClient client = new TcpClient(server, realPort);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("PortOut send: {0}\n", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}\n", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

           // Console.WriteLine("\n Press Enter to continue...");
           // Console.Read();
        }


       public void listener()
       {

           while (true)
           {
               if (this.isOccupied == true)
               {
                   this.Connect("127.0.0.1", (string)Serial.SerializeObject(this.protuni));
                   this.isOccupied = false;

               }
           }
       
       
       }


    }

    }

