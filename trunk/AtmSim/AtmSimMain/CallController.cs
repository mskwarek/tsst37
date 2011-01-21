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
        private Dictionary<string, DirectoryEntry> Directory = new Dictionary<string,DirectoryEntry>();
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
            this.rc = new RoutingController(manager);
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
            if (login.Length == 3 && login[0] == "login")
            {
                client.Name = login[1];
                client.Id = Int32.Parse(login[2]);
                if (Directory.ContainsKey(client.Name))
                {
                    client.Socket.Send(Encoding.ASCII.GetBytes("rejected"));
                    client.Socket.Close();
                }
                else
                {
                    Directory.Add(login[1], client);
                    client.Socket.Send(Encoding.ASCII.GetBytes("ok"));
                    client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, OnDataReceived, client);
                }
            }
            else
            {
                client.Socket.Send(Encoding.ASCII.GetBytes("rejected"));
                client.Socket.Close();
            }
            this.socket.BeginAccept(OnClientConnect, this.socket);
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            DirectoryEntry client = (DirectoryEntry)asyn.AsyncState;
            int r = 0;
            try { r = client.Socket.EndReceive(asyn); }
            catch (SocketException)
            {
                client.Socket.Close();
                Directory.Remove(client.Name);
                return;
            }
            if (r == 0)
            {
                client.Socket.Close();
                Directory.Remove(client.Name);
                return;
            }
            string recv = Encoding.ASCII.GetString(client.Buffer, 0, r);
            ProcessQuery(recv, client);
            client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, OnDataReceived, client);
        }

        private void ProcessQuery(string recv, DirectoryEntry client)
        {
            string[] query = recv.Split(' ');
            if (query[0] == "call_request")
                // format wiadomości: call_request {calling_name} {called_name} {capacity}
            {
                if (query[1] == client.Name)
                {
                    if (Directory.ContainsKey(query[2]))
                    {
                        var called = Directory[query[2]];
                        int callingId = client.Id;
                        int calledId = called.Id;
                        NetworkConnection connection = rc.setupConnection(callingId, calledId, manager.GetConnectionId(), 5);
                        if (connection == null)
                            client.Socket.Send(Encoding.ASCII.GetBytes(
                                String.Format("call_rejected no_resources {0}", query[2])));
                        else
                        {
                            manager.AddConnection(connection);
                            called.Socket.Send(Encoding.ASCII.GetBytes(
                                String.Format("call_pending {0} {1}", connection.Id, client.Name)));
                        }
                    }
                    else
                    {
                        client.Socket.Send(Encoding.ASCII.GetBytes(
                            String.Format("call_rejected no_target {0}", query[2])));
                    }
                }
            }
            else if (query[0] == "call_accepted")
                // format wiadomości: call_accepted {call_id} {calling_name}
            {
                int id = Int32.Parse(query[1]);
                manager.Connect(id);
                var caller = Directory[query[2]];
                caller.Socket.Send(Encoding.ASCII.GetBytes(
                    String.Format("call_finished {0} {1}", id, client.Name)));
                client.Socket.Send(Encoding.ASCII.GetBytes(
                    String.Format("call_finished {0} {1}", id, caller.Name)));
            }
            else if (query[0] == "call_teardown")
                // format wiadomości: call_teardown {call_id}
            {
                int connectionId = Int32.Parse(query[1]);
                if (client.Id == manager.Connections[connectionId].Source     // połączenie może rozłączyć jedynie
                    || client.Id == manager.Connections[connectionId].Target) // węzeł korzystający z niego
                    manager.Disconnect(connectionId);
            }
        }
    }
}
