using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using QuickGraph;

/* **** TODO ****
 * Pobieranie danych o elementach na żądanie od agentów, zamiast trzymania ich w tablicy.
 * Do zrealizowania przez zmianę getterów i setterów managera, możliwe, że klasa NetworkElement
 * okaże się zbędna.
 * 
 * Uwzględnienie struktury grafowej sieci - przechowywanie informacji o łączach
 */

namespace AtmSim
{
    // Ta klasa jest przeznaczona do przechowywania danych o elementach sieci na potrzeby zarzadzania.
    public class Manager
    {
        private class Node
        {
            public int Id
            {
                get { return tnode.Id; }
                set { tnode.Id = value; }
            }
            public string Name
            {
                get { return tnode.Name; }
                set { tnode.Name = value; }
            }
            public string Type
            {
                get { return tnode.Type; }
                set { tnode.Type = value; }
            }
            public Socket Socket;
            public Topology.Node tnode;
        }

        public class NodeUnaccessibleException : Exception
        {
            public int Id = -1;
            public NodeUnaccessibleException()
            { }
            public NodeUnaccessibleException(int id)
            {
                Id = id;
            }
        }

        private Dictionary<int, Node> nodes = new Dictionary<int, Node>(); // mapa numerów id na metadane węzłów
        private Topology topology = new Topology(); // topologia sieci
        public Topology Topology
        { get { return topology; } }
        private Dictionary<int, VirtualPath> virtualPaths = new Dictionary<int, VirtualPath>();
        public Dictionary<int, VirtualPath> VirtualPaths
        { get { return virtualPaths; } }
        private Dictionary<int, NetworkConnection> connections = new Dictionary<int, NetworkConnection>();
        public Dictionary<int, NetworkConnection> Connections
        { get { return connections; } }
        public CallController CallController { get; set; }

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
        public int CCPort { get { return CallController.Port; } }

        public void Reset()
        {
            foreach (Node node in nodes.Values)
            {
                node.Socket.Close();
            }
            nodes.Clear();
            topology.Clear();
        }

        public void Init()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            this.socket.Bind(ip);
            this.socket.Listen(10);
            this.socket.BeginAccept(OnClientConnect, socket);
            this.CallController = new CallController(this);
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            Node node = new Node();
            node.tnode = new Topology.Node();
            node.Socket = this.socket.EndAccept(asyn);
            string idStr;
            try
            {
                idStr = Get(node.Socket, "ID");
                node.Name = Get(node.Socket, "Name");
                node.Type = Get(node.Socket, "Type");
            }
            catch (NodeUnaccessibleException)
            {
                this.socket.BeginAccept(OnClientConnect, socket);
                return;
            }
            node.Id = Int32.Parse(idStr);
            topology.AddVertex(node.tnode);
            nodes.Add(node.Id, node);
            this.socket.BeginAccept(OnClientConnect, socket);
        }

        private string Query(Socket sock, string query)
        {
            try
            {
                sock.Send(Encoding.ASCII.GetBytes(query));
                byte[] buffer = new byte[4096];
                int received = sock.Receive(buffer);
                return Encoding.ASCII.GetString(buffer, 0, received);
            }
            catch (SocketException) { throw new NodeUnaccessibleException(); }
        }

        public string Query(int id, string query)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    nodes[id].Socket.Send(Encoding.ASCII.GetBytes(query));
                    byte[] buffer = new byte[4096];
                    int received = nodes[id].Socket.Receive(buffer);
                    return Encoding.ASCII.GetString(buffer, 0, received);
                }
                catch (SocketException)
                {
                    Topology.RemoveVertex(nodes[id].tnode);
                    nodes.Remove(id);
                    List<int> disconnected = new List<int>();
                    foreach(int con in connections.Keys)
                    {
                        if (connections[con].Nodes.Contains(id))
                            disconnected.Add(con);
                    }
                    foreach (int con in disconnected)
                        CallController.CallTeardown(connections[con], "system");
                }
            }
            return "";
        }

        public string Get(Socket sock, string param)
        {
            string response = Query(sock, "get " + param);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[0] == "getresp" && tokens[1] == param)
                return tokens[2];
            return "";
        }

        public string Get(int id, string param)
        {
            string response = Query(id, "get " + param);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[0] == "getresp" && tokens[1] == param)
                return tokens[2];
            return "";
        }

        public string Set(int id, string param, string value)
        {
            string response = Query(id, "set " + param + " " + value);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[0] == "setresp" && tokens[1] == param)
                return tokens[2];
            return "";
        }

        // pobranie listy dostepnych elementow
        public List<string> GetElements()
        {
            List<string> ret = new List<string>();
            int[] keys = new int[nodes.Count];
            nodes.Keys.CopyTo(keys, 0);
            Array.Sort(keys);
            foreach (int key in keys)
            {
                if (Ping(key))
                {
                    string item = "[" + key + "] " + nodes[key].Name;
                    ret.Add(item);
                }
            }
            return ret;
        }

        public List<string> GetLinks()
        {
            List<string> ret = new List<string>();
            foreach (var link in topology.Edges)
            {
                string item = "{[" + link.Source.Id + "]" + link.Source.Name + "/" + link.SourcePort +
                    "} -> {[" + link.Target.Id + "]" + link.Target.Name + "/" + link.TargetPort + "} -- " + link.Capacity;
                ret.Add(item);
            }
            return ret;
        }


        public List<string> GetConnections()
        {
            List<string> ret = new List<string>();
            int[] keys = new int[connections.Count];
            connections.Keys.CopyTo(keys, 0);
            Array.Sort(keys);
            foreach (int key in keys)
            {
                string item = "[" + key + "] " + nodes[connections[key].Source].Name +  " ---> " + nodes[connections[key].Target].Name;
                ret.Add(item);
            }
            return ret;
        }

        public List<string> GetVPaths()
        {
            List<string> ret = new List<string>();
            int[] keys = new int[virtualPaths.Count];
            virtualPaths.Keys.CopyTo(keys, 0);
            Array.Sort(keys);
            foreach (int key in keys)
            {
                string item = "[" + key + "] " + virtualPaths[key].Source.Name + " --> " + virtualPaths[key].Target.Name;
                ret.Add(item);
            }
            return ret;
        }

        public bool Ping(int id)
        {
            string pong = Query(id, "ping");
            if (pong == "pong")
                return true;
            else 
                return false;
        }

        public int ConnectedNodes
        {
            get { return nodes.Count; }
        }

        public Log GetLog(int id, int index)
        {
            string response = Query(id, "get log " + index);
            if (response != "")
                return (Log)Serial.DeserializeObject(response, typeof(Log));
            else
                return null;
        }

        public C GetConfig(int id)
        {
            string response = Query(id, "get config");
            if (response != "")
                return (C)Serial.DeserializeObject(response, typeof(C));
            else
                return null;
        }

        public Routing GetRouting(int id)
        {
            string response = Query(id, "get routing");
            if (response != "")
                return (Routing)Serial.DeserializeObject(response, typeof(Routing));
            else
                return null;
        }

        public bool AddRouting(int id, string label, string value)
        {
            string response = Query(id, "rtadd " + label + " " + value);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 4 && tokens[3] == "ok")
                return true;
            else
                return false;
        }

        public bool AddRouting(int nodeId, string label, string value, int connectionId)
        {
            string response = Query(nodeId, "rtadd " + label + " " + value + " " + connectionId);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 5 && tokens[4] == "ok")
                return true;
            else
                return false;
        }

        public bool RemoveRouting(int id, string label)
        {
            string response = Query(id, "rtdel " + label);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[2] == "ok")
                return true;
            return false;
        }

        public bool RemoveRouting(int nodeId, int connectionId)
        {
            string response = Query(nodeId, "rtdel " + connectionId);
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[2] == "ok")
                return true;
            return false;
        }

        public void AddLink(Config.Link link)
        {
            if (nodes.ContainsKey(link.StartNode) && nodes.ContainsKey(link.EndNode))
            {
                string port = Get(link.EndNode, "PortsIn." + link.EndPort + "._port");
                Set(link.StartNode, "PortsOut." + link.StartPort + "._port", port);
                Set(link.StartNode, "PortsOut." + link.StartPort + ".Connected", "True");
                topology.AddEdge(new Topology.Link(
                   nodes[link.StartNode].tnode, nodes[link.EndNode].tnode,
                   link.StartPort, link.EndPort, link.Capacity));
            }
        }

        public int GetFreeId()
        {
            int i = 1;
            while (connections.ContainsKey(i) || virtualPaths.ContainsKey(i))
                i++;
            return i;
        }

        public bool AddPath(VirtualPath vpath)
        {
            string label = "";
            string value = "";
            foreach (var link in vpath.Path)
            {
                if (label != "")
                {
                    value = link.SourceRouting;
                    AddRouting(link.SourceId, label, value, vpath.Id);
                }
                label = link.TargetRouting;
            }
            virtualPaths.Add(vpath.Id, vpath);
            return true;
        }

        public void AddConnection(NetworkConnection connection)
        {
            connections.Add(connection.Id, connection);
        }

        public bool Connect(int connectionId)
        {
            if (!connections.ContainsKey(connectionId))
                return false;
            NetworkConnection connection = connections[connectionId];
            if (!connections[connectionId].Active)
            {
                string label = connection.Id.ToString(); // pierwszy wpis: Id -> re
                string value = "";
                foreach (var link in connection.Path)
                {
                    value = link.SourceRouting;
                    if (!AddRouting(link.SourceId, label, value, connection.Id))
                    {   // to nie powinno się zdarzyć...
                        Disconnect(connectionId);
                        return false;
                    }
                    link.Link.Capacity -= connection.Capacity;
                    label = link.TargetRouting;
                }
                value = connection.Id.ToString();
                if (!AddRouting(connection.Target, label, value, connection.Id))
                {   // ...to także
                    Disconnect(connectionId);
                    return false;
                }
                connection.Active = true;
            }
            return connection.Active;
        }

        public void Disconnect(int connectionId)
        {
            NetworkConnection connection = connections[connectionId];
            foreach (var link in connection.Path)
            {
                if (RemoveRouting(link.SourceId, connection.Id))
                    link.Link.Capacity += connection.Capacity;
            }
            RemoveRouting(connection.Target, connection.Id);
            connections.Remove(connectionId);
        }
    }
}
