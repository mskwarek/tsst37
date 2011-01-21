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
        int portID;
        public int PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private Socket clientSocket;
        private int tcpPort;
        public int TcpPort
        {
            get { return tcpPort; }
            set { tcpPort = value; }
        }
        private bool open = true;
        public bool Open { get { return open; } }
        private bool connected = false;
        public bool Connected { get { return connected; } }

        public PortOut(int id)
        {
            portID = id;
        }

        public void Send(ProtocolUnit pu)
        {
            string str = Serial.SerializeObject(pu);
            byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            if (clientSocket != null && connected)
            {
                try { clientSocket.Send(data); }
                catch (SocketException) { connected = false; }
            }
        }

        public void Connect()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint server = new IPEndPoint(IPAddress.Loopback, tcpPort);
            clientSocket.Connect(server);
            if (clientSocket.Connected)
                connected = true;
        }
    }   
}

