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
        // z tęsknoty za typedef z C++ :<
        public class Config : Dictionary<string, string>
        {
            public Config() : base() { }
            public Config(Config config) : base((Dictionary<string, string>)config) { }
        }
        
        // pojedynczy element sieci i jego dane
        //private class NetworkElement
        //{
        //    public string Name;     // nazwa elementu
        //    public Config Config;   // konfiguracja: pary parametr-wartosc
        //    /*
        //     * Tablica routingu bedzie miala postac zalezna od tego, czy element jest hostem, czy
        //     * routerem:
        //     * Dla routera beda to pary:
        //     * "portID;VPI;VCI" - wejsciowe
        //     * "portID;VPI;VCI" - wyjsciowe
        //     * Dla hosta beda to pary:
        //     * "HostID" - nazwa hosta docelowego
        //     * "VPI,VCI" - sciezka prowadzaca do danego hosta
        //     */
        //    public Routing Routing;
        //    public Log Log;

        //    public NetworkElement(string name, Config config, Routing routing, Log log)
        //    {
        //        this.Name = name;
        //        this.Config = config;
        //        this.Routing = routing;
        //        this.Log = log;
        //    }
        //}

        private class ConnectionData
        {
            public Socket socket;
            public byte[] buffer = new byte[4096];
            public ConnectionData(Socket sock) { this.socket = sock; }
        }

        private class Node
        {
            public int id;
            public string name;
            public Socket socket;
        }

        private Dictionary<string, IAgent> nodes1 = new Dictionary<string, IAgent>();
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

        private void OnDataReceived(IAsyncResult asyn)
        {
            ConnectionData c = (ConnectionData)asyn;
            int recv = c.socket.EndReceive(asyn);
            if (recv == 0)
            {
                c.socket.Close();
                return;
            }
            //string receivedData = Encoding
        }

        private string Get(Socket sock, string param)
        {
            string query = "get " + param;
            sock.Send(Encoding.ASCII.GetBytes(query));
            byte[] buffer = new byte[4096];
            sock.Receive(buffer);
            string response = Encoding.ASCII.GetString(buffer);
            if (param == "config")
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
            else
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

        public Configuration GetConfig(int id)
        {
            string conf = Get(id, "config");
            return (Configuration)Serial.DeserializeObject(conf, typeof(Configuration));
        }

        /* **** TODO ****
         * Szalone miotanie wyjątkami, zamiast generowania pustaków. Niech żyją wyjątki!
         */
        public Config GetConfig(string name)
        {
            if (nodes1.ContainsKey(name))
            {
                Config config = new Config();
                string[] paramlist = nodes1[name].GetParamList();
                foreach (string param in paramlist)
                {
                    config.Add(param, nodes1[name].GetParam(param));
                }
                return config;
            }
            else return new Config();
        }

        public void SetConfig(string name, string param, string value)
        {
            if (nodes1.ContainsKey(name))
                nodes1[name].SetParam(param, value);
        }

        public Routing GetRouting(string name)
        {
            if (nodes1.ContainsKey(name))
            {
                /*Common.RoutingTable table = nodes[name].GetRoutingTable();
                Routing routing = new Routing();
                foreach (var entry in table)
                {
                    routing.Add(entry.Key.ToString(), entry.Value.ToString());
                }*/
                return nodes1[name].GetRoutingTable();
            }
            else
                return new Routing();
        }

        public void AddRouting(string name, string label, string value)
        {
            if (nodes1.ContainsKey(name))
            {
                
                nodes1[name].AddRoutingEntry(label, value);
            }
        }

        public void RemoveRouting(string name, string label)
        {
            if (nodes1.ContainsKey(name))
                nodes1[name].RemoveRoutingEntry(label);
        }

        public void ModifyRouting(string name, string label, string newlabel, string value)
        {
            //if (nodes.ContainsKey(name))
            //{
            //    nodes[name].Agent.RemoveRoutingEntry(label);
            //    nodes[name].Agent.AddRoutingEntry(newlabel, value);
            //}
        }

        public string GetLog(string name)
        {
            if (nodes1.ContainsKey(name))
                return nodes1[name].GetLog();
            else
                return "Brak logów dla elementu " + name;
        }

        public void LogMsg(string name, string msg)
        {
            //if (nodes.ContainsKey(name))
            //    nodes[name].Log.LogMsg(msg);
        }

        public void SubscribeLog(string name, ILogListener listener)
        {
            //if (nodes.ContainsKey(name))
            //    nodes[name].Log.Subscribe(listener);
        }

        public void UnsubscribeLog(string name, ILogListener listener)
        {
            //if (nodes.ContainsKey(name))
            //    nodes[name].Log.Unsubscribe(listener);
        }

        public List<Edge<string>> GetLinks()
        {
            return links1;
        }
    }
}
