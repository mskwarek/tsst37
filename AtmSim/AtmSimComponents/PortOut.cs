﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AtmSim.Components
{
    public class PortOut : IPortOut
    {
        int portID;
        public int PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private Socket clientSocket;
        //private byte[] buffer = new byte[1024];
        private int tcpPort;
        public int TcpPort
        {
            get { return tcpPort; }
            set { tcpPort = value; }
        }
        private bool open = false;
        public bool Open { get { return open; } }
        private bool connected = false;
        public bool Connected { get { return connected; } }

        public void Send(string pu)
        {
            throw new NotImplementedException();
        }

        public void Send(ProtocolUnit pu)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(Serial.SerializeObject(pu));
            if (clientSocket != null && connected)
                clientSocket.Send(data);
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

