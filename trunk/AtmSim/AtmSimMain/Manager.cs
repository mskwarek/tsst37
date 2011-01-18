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
            public int id;
            public string name;
            public Socket socket;
            public Topology.Node node;
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

//        private Dictionary<string, IAgent> nodes1 = new Dictionary<string, IAgent>(); // potrzebne póki nie mamy podziału na procesy
        private Dictionary<int, Node> nodes = new Dictionary<int, Node>();
//        private List<Edge<string>> links1 = new List<Edge<string>>();
        private List<TaggedEdge<int, string>> links = new List<TaggedEdge<int, string>>();
        private Topology topology = new Topology();
        public Topology Topology
        { get { return topology; } }
        
        
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

        //public void AddNode(string name, IAgent node)
        //{
        //    nodes1.Add(name, node);
        //}

        //public void AddLink(string sourceName, string destinationName)
        //{
        //    links1.Add(new Edge<string>(sourceName, destinationName));
        //}

        public void Reset()
        {
            foreach (Node node in nodes.Values)
            {
                node.socket.Close();
            }
            nodes.Clear();
            links.Clear();
            topology.Clear();
        }

        public void Init()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            this.socket.Bind(ip);
            this.socket.Listen(10);
            this.socket.BeginAccept(OnClientConnect, socket);
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            Node node = new Node();
            node.socket = this.socket.EndAccept(asyn);
            string idStr;
            try
            {
                idStr = Get(node.socket, "ID");
                node.name = Get(node.socket, "Name");
            }
            catch (NodeUnaccessibleException)
            {
                this.socket.BeginAccept(OnClientConnect, socket);
                return;
            }
            node.id = Int32.Parse(idStr);
            Topology.Node tnode = new Topology.Node(node.id, node.name);
            node.node = tnode;
            topology.AddVertex(tnode);
            nodes.Add(node.id, node);
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

        public string Get(Socket sock, string param)
        {
            string response = Query(sock, "get " + param);
            //if (param == "config" || param == "routing" || param == "log")
            //    return response;
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[0] == "getresp" && tokens[1] == param)
                return tokens[2];
            return "";
        }

        public string Get(int id, string param)
        {
            if (nodes.ContainsKey(id))
            {
                try { return Get(nodes[id].socket, param); }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return "";
        }

        public string Set(int id, string param, string value)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    string response = Query(nodes[id].socket, "set " + param + " " + value);
                    string[] tokens = response.Split(' ');
                    if (tokens.Length == 3 && tokens[0] == "setresp" && tokens[1] == param)
                        return tokens[2];
                }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return "";
        }

        // pobranie listy dostepnych elementow
        public List<string> GetElements()
        {
            List<string> elements = new List<string>();
            foreach (int key in nodes.Keys)
            {
                string element = "[" + key + "] " + nodes[key].name;
                elements.Add(element);
            }
            return elements;
        }

        public int ConnectedNodes
        {
            get { return nodes.Count; }
        }

        public Log GetLog(int id)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    string response = Query(nodes[id].socket, "get log");
                    return (Log)Serial.DeserializeObject(response, typeof(Log));
                }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return null;
        }

        public Configuration GetConfig(int id)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    string response = Query(nodes[id].socket, "get config");
                    return (Configuration)Serial.DeserializeObject(response, typeof(Configuration));
                }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return null;
        }

        public Routing GetRouting(int id)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    string response = Query(nodes[id].socket, "get routing");
                    return (Routing)Serial.DeserializeObject(response, typeof(Routing));
                }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return null;
        }

        public bool AddRouting(int id, string label, string value)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    string response = Query(nodes[id].socket, "rtadd " + label + " " + value);
                    string[] tokens = response.Split(' ');
                    if (tokens.Length == 4 && tokens[3] == "ok")
                        return true;
                }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return false;
        }

        public bool RemoveRouting(int id, string label)
        {
            if (nodes.ContainsKey(id))
            {
                try
                {
                    string response = Query(nodes[id].socket, "rtdel " + label);
                    string[] tokens = response.Split(' ');
                    if (tokens.Length == 3 && tokens[2] == "ok")
                        return true;
                }
                catch (NodeUnaccessibleException) { throw new NodeUnaccessibleException(id); }
            }
            return false;
        }

        public void AddLink(Config.Link link)
        {
            if (nodes.ContainsKey(link.StartNode) && nodes.ContainsKey(link.EndNode))
            {
                string port = Get(link.EndNode, "PortsIn." + link.EndPort + "._port");
                Set(link.StartNode, "PortsOut." + link.StartPort + "._port", port);
                Set(link.StartNode, "PortsOut." + link.StartPort + ".Connected", "True");
                links.Add(new TaggedEdge<int, string>(link.StartNode, link.EndNode, link.StartPort+":"+link.EndPort));
                topology.AddEdge(new Topology.Link(link.StartPort + ":" + link.EndPort, 
                   nodes[link.StartNode].node, nodes[link.EndNode].node));
            }
        }

        // przestarzałe
        public BidirectionalGraph<int, Edge<int>> GetTopology()
        {
            BidirectionalGraph<int, Edge<int>> g =
                new BidirectionalGraph<int, Edge<int>>();
            //foreach (Node node in nodes.Values)
            //    g.AddVertex(node.id);
            foreach (TaggedEdge<int, string> edge in links)
                g.AddVerticesAndEdge(new Edge<int> (edge.Source, edge.Target));
            return g;
        }

        public List<TaggedEdge<int,string>> GetLinks()
        {
            return links;
        }
    }
}
