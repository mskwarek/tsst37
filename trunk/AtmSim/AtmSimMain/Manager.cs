﻿using System;
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
        private List<TaggedEdge<int, string>> links = new List<TaggedEdge<int, string>>();
        private Topology topology = new Topology(); // topologia sieci
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
                node.Socket.Close();
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
            node.tnode = new Topology.Node();
            node.Socket = this.socket.EndAccept(asyn);
            string idStr;
            try
            {
                idStr = Get(node.Socket, "ID");
                node.Name = Get(node.Socket, "Name");
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

        private string Query(int id, string query)
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
            List<string> elements = new List<string>();
            int[] keys = new int[nodes.Count];
            nodes.Keys.CopyTo(keys, 0);
            Array.Sort(keys);
            foreach (int key in keys)
            {
                if (Ping(key))
                {
                    string element = "[" + key + "] " + nodes[key].Name;
                    elements.Add(element);
                }
            }
            return elements;
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

        public Log GetLog(int id)
        {
            string response = Query(id, "get log");
            if (response != "")
                return (Log)Serial.DeserializeObject(response, typeof(Log));
            else
                return null;
        }

        public Configuration GetConfig(int id)
        {
            string response = Query(id, "get config");
            if (response != "")
                return (Configuration)Serial.DeserializeObject(response, typeof(Configuration));
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

        public bool RemoveRouting(int id, string label)
        {
            string response = Query(id, "rtdel " + label);
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
                links.Add(new TaggedEdge<int, string>(link.StartNode, link.EndNode, link.StartPort + ":" + link.EndPort));
                topology.AddEdge(new Topology.Link(link.StartPort + ":" + link.EndPort,
                   nodes[link.StartNode].tnode, nodes[link.EndNode].tnode));
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
                g.AddVerticesAndEdge(new Edge<int>(edge.Source, edge.Target));
            return g;
        }

        public List<TaggedEdge<int, string>> GetLinks()
        {
            return links;
        }
    }
}
