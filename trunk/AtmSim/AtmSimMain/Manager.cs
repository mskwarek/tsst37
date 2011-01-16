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
        }

        private Dictionary<string, IAgent> nodes1 = new Dictionary<string, IAgent>(); // potrzebne póki nie mamy podziału na procesy
        private Dictionary<int, Node> nodes = new Dictionary<int, Node>();
        private List<Edge<string>> links1 = new List<Edge<string>>();
        private List<Edge<int>> links = new List<Edge<int>>();
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

        public void AddNode(string name, IAgent node)
        {
            nodes1.Add(name, node);
        }

        public void AddLink(string sourceName, string destinationName)
        {
            links1.Add(new Edge<string>(sourceName, destinationName));
        }

        public void Reset()
        {
            nodes1.Clear();
            links1.Clear();
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
            string idStr = Get(node.socket, "ID");
            node.name = Get(node.socket, "Name");
            node.id = Int32.Parse(idStr);
            nodes.Add(node.id, node);
            this.socket.BeginAccept(OnClientConnect, socket);
        }

        private string Query(Socket sock, string query)
        {
            sock.Send(Encoding.ASCII.GetBytes(query));
            byte[] buffer = new byte[4096];
            sock.Receive(buffer);
            return Encoding.ASCII.GetString(buffer);
        }

        public string Get(Socket sock, string param)
        {
            string response = Query(sock, "get " + param);
            if (param == "config" || param == "routing" || param == "log")
                return response;
            string[] tokens = response.Split(' ');
            if (tokens.Length == 3 && tokens[0] == "getresp" && tokens[1] == param)
                return tokens[2];
            return "";
        }

        public string Get(int id, string param)
        {
            if (nodes.ContainsKey(id))
                return Get(nodes[id].socket, param);
            return "";
        }

        public string Set(int id, string param, string value)
        {
            if (nodes.ContainsKey(id))
            {
                string response = Query(nodes[id].socket, "set " + param + " " + value);
                string[] tokens = response.Split(' ');
                if (tokens.Length == 3 && tokens[0] == "setresp" && tokens[1] == param)
                    return tokens[2];
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

        public Log GetLog(int id)
        {
            if (nodes.ContainsKey(id))
            {
                string response = Query(nodes[id].socket, "get log");
                return (Log)Serial.DeserializeObject(response, typeof(Log));
            }
            return null;
        }

        public Configuration GetConfig(int id)
        {
            if (nodes.ContainsKey(id))
            {
                string response = Query(nodes[id].socket, "get config");
                return (Configuration)Serial.DeserializeObject(response, typeof(Configuration));
            }
            return null;
        }

        public Routing GetRouting(int id)
        {
            if (nodes.ContainsKey(id))
            {
                string response = Query(nodes[id].socket, "get routing");
                return (Routing)Serial.DeserializeObject(response, typeof(Routing));
            }
            return null;
        }

        public bool AddRouting(int id, string label, string value)
        {
            if (nodes.ContainsKey(id))
            {
                string response = Query(nodes[id].socket, "rtadd " + label + " " + value);
                string[] tokens = response.Split(' ');
                if (tokens.Length == 4 && tokens[3] == "ok")
                    return true;
            }
            return false;
        }

        public bool RemoveRouting(int id, string label)
        {
            if (nodes.ContainsKey(id))
            {
                string response = Query(nodes[id].socket, "rtdel " + label);
                string[] tokens = response.Split(' ');
                if (tokens.Length == 3 && tokens[2] == "ok")
                    return true;
            }
            return false;
        }

        public List<Edge<string>> GetLinks()
        {
            return links1;
        }
    }
}
