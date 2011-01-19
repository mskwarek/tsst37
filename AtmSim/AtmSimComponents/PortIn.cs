using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AtmSim.Components
{
    public class PortIn
    {
        private int portID;
        public int PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private Socket listenerSocket;
        private Socket remoteSocket;
        private byte[] buffer = new byte[1024];
        private int tcpPort;
        public int TcpPort
        {
            get { return tcpPort; }
        }
        private bool open = false;
        public bool Open { get { return open; } }
        private bool connected = false;
        public bool Connected { get { return connected; } }
        private IFrameReceiver receiver;

        public PortIn(int id) : this()
        {
            portID = id;
        }

        public PortIn()
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, 0); // tworzenie end-pointu na dowolnym wolnym porcie
            listenerSocket.Bind(ipLocal);
            tcpPort = ((IPEndPoint)listenerSocket.LocalEndPoint).Port;
            listenerSocket.Listen(1);
            listenerSocket.BeginAccept(OnClientConnect, listenerSocket);
            open = true;
        }

        public void OnClientConnect(IAsyncResult asyn)
        {
            if (connected)
            {
                remoteSocket.Close(); // przychodzace polaczenie spowoduje zamkniecie istniejacego
            }
            remoteSocket = listenerSocket.EndAccept(asyn);
            connected = true;
            remoteSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, remoteSocket);
            listenerSocket.BeginAccept(OnClientConnect, listenerSocket);
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            int recv = 0;
            try
            {
                recv = remoteSocket.EndReceive(asyn);
            }
            catch (SocketException)
            {
                remoteSocket.Close();
                connected = false;
                return;
            }
            if (recv == 0)
            {
                remoteSocket.Close();
                connected = false;
                return;
            }
            string receivedData = Encoding.ASCII.GetString (buffer, 0, recv);
            Receive( (ProtocolUnit) Serial.DeserializeObject( receivedData, typeof(ProtocolUnit)) );
            remoteSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, remoteSocket);
        }

        public void Receive(ProtocolUnit pu)
        {
            receiver.ReceiveFrame(pu, portID);
        }

        public void SetReceiver(IFrameReceiver fr)
        {
            receiver = fr;
        }
    }
}
