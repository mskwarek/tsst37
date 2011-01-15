using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AtmSim.Components
{
    public class PortIn : IPortIn
    {
        private int portID;
        public int PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private Socket thisSocket;
        private Socket remoteSocket;
        private byte[] buffer = new byte[1024];
        private int _tcpPort;
        int tcpPort
        {
            get { return _tcpPort; }
        }
        private bool open = false;
        bool Open { get { return open; } }
        private bool connected = false;
        bool Connected { get { return connected; } }
        IFrameReceiver receiver;
        
        PortIn()
        {
            thisSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, 0); // tworzenie end-pointu na dowolnym wolnym porcie
            thisSocket.Bind(ipLocal); /* TODO: petla, jesli bedzie potrzebna */
            _tcpPort = ipLocal.Port;
            thisSocket.Listen(1);
            thisSocket.BeginAccept(OnClientConnect, thisSocket);
            open = true;
        }

        public void OnClientConnect(IAsyncResult asyn)
        {
            if (connected)
            {
                remoteSocket.Close(); // przychodzace polaczenie spowoduje zamkniecie istniejacego
            }
            remoteSocket = thisSocket.EndAccept(asyn);
            connected = true;
            remoteSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, remoteSocket);
            thisSocket.BeginAccept(OnClientConnect, thisSocket);
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            int recv = remoteSocket.EndReceive(asyn);
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

        public void Receive(string pu)
        {
            
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
