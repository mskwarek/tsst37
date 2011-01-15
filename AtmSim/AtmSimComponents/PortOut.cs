using System;
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
        int _portID;
        public int portID
        {
            get { return _portID; }
            set { _portID = value; }
        }
        private Socket remoteSocket;
        //private byte[] buffer = new byte[1024];
        private int _tcpPort;
        public int tcpPort
        {
            get { return _tcpPort; }
            set { _tcpPort = value; }
        }
        private bool _open = false;
        public bool isOpen { get { return _open; } }
        private bool _connected = false;
        public bool isConnected { get { return _connected; } }

        public void Send(string pu)
        {
            throw new NotImplementedException();
        }

        public void Send(ProtocolUnit pu)
        {
            throw new NotImplementedException();
        }
    }   
}

