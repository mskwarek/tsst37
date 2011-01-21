using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim.Components
{
    public class Caller
    {
        private string name;
        public string Name { get { return name; } }
        private int ccport;
        private Socket socket;
        public bool Connected { get; private set; }
        byte[] buffer = new byte[4096];
        public Dictionary<int, string> Connections { get; private set; }
        public string Message { get; private set; }

        public Caller(string name, int port)
        {
            this.name = name;
            this.ccport = port;
            this.Connected = false;
            this.Connections = new Dictionary<int, string>();
            this.Message = "Brak połączenia z centralą.";
        }

        public void Init(int id)
        {
            this.Message = "Łączenie z centralą...";
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint server = new IPEndPoint(IPAddress.Loopback, ccport);
            socket.Connect(server);
            this.Message = "Autoryzacja...";
            socket.Send(Encoding.ASCII.GetBytes(
                String.Format("login {0} {1}",name, id)));
            int r = socket.Receive(buffer);
            if (Encoding.ASCII.GetString(buffer, 0, r) == "ok")
                Connected = true;
            this.Message = "Połączono.";
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, socket);
        }

        public void BeginCall(string name)
        {
            socket.Send(Encoding.ASCII.GetBytes(
                String.Format("call_request {0} {1}", this.name, name)));
            this.Message = "Łączenie z " + name + "...";
        }

        public void EndCall(int id)
        {
            socket.Send(Encoding.ASCII.GetBytes(
                String.Format("call_teardown {0}", id)));
            Connections.Remove(id);
            this.Message = "Połączenie " + id + " zakończone.";
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            int r = socket.EndReceive(asyn);
            string recv = Encoding.ASCII.GetString(buffer, 0, r);
            Process(recv);
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, socket);
        }

        private void Process(string recv)
        {
            string[] query = recv.Split(' ');
            if (query[0] == "call_accepted")
            {
                int id = Int32.Parse(query[1]);
                Connections.Add(id, query[2]);
                this.Message = "Połączono z " + query[2] + ".";
            }
            else if (query[0] == "call_rejected")
            {
                this.Message = "Nie ma takiego numeru!";
            }
        }
    }
}
