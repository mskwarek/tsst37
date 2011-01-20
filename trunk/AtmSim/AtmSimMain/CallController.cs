using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim
{
    public class CallController
    {
        private class DirectoryEntry
        {
            public int Id;
            public string Name;
            public Socket Socket;
            public byte[] Buffer = new byte[4086];
        }
        private Dictionary<string, DirectoryEntry> Directory;
        private Manager manager;
        private RoutingController rc;
        private Socket socket;

        public int Port
        {
            get
            {
                if (this.socket != null)
                    return ((IPEndPoint)this.socket.LocalEndPoint).Port;
                else
                    return 0;
            }
        }

        public CallController(Manager manager)
        {
            this.manager = manager;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            this.socket.Bind(ip);
            this.socket.Listen(10);
            this.socket.BeginAccept(OnClientConnect, this.socket);
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            DirectoryEntry client = new DirectoryEntry();
            client.Socket = this.socket.EndAccept(asyn);
            int received = client.Socket.Receive(client.Buffer);
            string[] login = (Encoding.ASCII.GetString(client.Buffer, 0, received)).Split(' ');
            client.Name = login[0];
            client.Id = Int32.Parse(login[1]);
            Directory.Add(login[0], client);
            client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, OnDataReceived, client);
            this.socket.BeginAccept(OnClientConnect, this.socket);
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            DirectoryEntry client = (DirectoryEntry)asyn;
            int recv = client.Socket.EndReceive(asyn);
            string request = Encoding.ASCII.GetString(client.Buffer, 0, recv);
            ProcessQuery(request, client);
            client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, OnDataReceived, client);
        }

        private void ProcessQuery(string request, DirectoryEntry client)
        {
            string[] query = request.Split(' ');
            if (query[0] == "call_request")
                // format wiadomości: call_request {calling_name} {called_name}
            {
                if (query[1] == client.Name)
                {
                    var called = Directory[query[2]];
                    int callingId = client.Id;
                    int calledId = called.Id;
                    rc.setupConnection(callingId, calledId, 1);
                    // called.Socket.BeginSend("call_pending");
                }
            }
            else if (query[0] == "call_accept")
                // format wiadomości: call_accept {call_id}
            {

            }
            else if (query[0] == "call_teardown")
                // format wiadomości: call_teardown {call_id}
            {

            }
        }
    }
}
