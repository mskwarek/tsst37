using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim.Components 
{
    public class NodeAgent : IAgent
    {
        // Referencja wezla zarzadzanego przez agenta
        Node node;
        // Zawartosc konfiguracji wezla
        Configuration config;
        Socket managerSocket;
        byte[] buffer = new byte[4086];

        public NodeAgent(Node n, int port)
        {
            node = n;
            config = new Configuration(n.Name);
            Configuration pIn = new Configuration("PortsIn");
            for (int i = 0; i<node.PortsIn.Length; i++ ) {
                pIn.Add("Open");
                pIn.Add("Connected");
                pIn.Add("_port");
            }
            Configuration pOut = new Configuration("PortsIn");
            for (int i = 0; i < node.PortsIn.Length; i++)
            {
                pOut.Add("Open");
                pOut.Add("Connected");
                pOut.Add("_port");
            }
            config.Add("ID");
            config.Add("Name");
            config.Add(pIn);
            config.Add(pOut);

            managerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint server = new IPEndPoint(IPAddress.Loopback, port);
            managerSocket.Connect(server);
            managerSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, managerSocket);
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            int recv = managerSocket.EndReceive(asyn);
            if (recv == 0)
            {
                managerSocket.Close();
                return;
            }
            string query = Encoding.ASCII.GetString(buffer, 0, recv);
            string response = ProcessQuery(query);
            if (response != "")
                managerSocket.Send(System.Text.Encoding.ASCII.GetBytes(response));
            managerSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, managerSocket);
        }

        private string ProcessQuery(string query)
        {
            string[] command = query.Split(' ');
            string response = "";
            if (command[0] == "get")
            {
                if (command.Length != 2)
                    return "";
                if (command[1] == "config")
                    return Serial.SerializeObject(config);
                response += "getresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "ID")
                    response += " " + node.Id;
                else if (param[0] == "Name")
                    response += " " + node.Name;
                else if (param[0] == "PortsIn")
                {
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return ""; }
                    if (n >= node.PortsIn.Length)
                        return "";
                    if (param[2] == "Open")
                        response += " " + node.PortsIn[n].Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortsIn[n].Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortsIn[n].TcpPort;
                    else return "";
                }
                else if (param[0] == "PortsOut")
                {
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return ""; }
                    if (n >= node.PortsOut.Length)
                        return "";
                    if (param[2] == "Open")
                        response += " " + node.PortsOut[n].Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortsOut[n].Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortsOut[n].TcpPort;
                    else return "";
                }
            }
            else if (command[0] == "set")
            {
                if (command.Length != 3)
                    return "";
                response += "setresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "ID")
                    response += " X"; // parametr niezmienny
                else if (param[0] == "Name")
                {   
                    node.Name = command[2];
                    response += " " + node.Name;
                }
                else if (param[0] == "PortsIn")
                {
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return ""; }
                    if (n >= node.PortsIn.Length)
                        return "";
                    if (param[2] == "Open")
                        response += " " + node.PortsIn[n].Open; // póki co niezmienne
                    else if (param[2] == "Connected")                        
                        response += " " + node.PortsIn[n].Connected; // niezmienne
                    else if (param[2] == "_port")
                        response += " " + node.PortsIn[n].TcpPort; // niezmienne
                    else return "";
                }
                else if (param[0] == "PortsOut")
                {
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return ""; }
                    if (n >= node.PortsOut.Length)
                        return "";
                    if (param[2] == "Open")
                        response += " " + node.PortsOut[n].Open; // póki co niezmienne
                    else if (param[2] == "Connected")
                    {
                        if (command[2] == "1")
                            node.PortsOut[n].Connect();
                        response += " " + node.PortsOut[n].Connected;
                    }
                    else if (param[2] == "_port")
                    {
                        try {
                            node.PortsOut[n].TcpPort = Int32.Parse(command[2]);
                        }
                        catch (ArgumentNullException) { return ""; }
                        response += " " + node.PortsOut[n].TcpPort;
                    }
                    else return "";
                }
            }
            return response;
        }

        public string[] GetParamList()
        {
            string[] param = { "name", "portsIn", "portsOut" };
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return node.Name;
            else if (name == "portsIn")
                return "0-" + (node.PortsIn.Length - 1);
            else if (name == "portsOut")
                return "0-" + (node.PortsOut.Length - 1);
            else return "";
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                node.Name = value;
        }

        public Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.Matrix.RoutingTable)
            {
                table.Add(element.Key.ToString(),element.Value.ToString());
            }
            return table;
        }

        public void AddRoutingEntry(string label, string value)
        {
            node.Matrix.AddToMatrix(new RoutingEntry(label), new RoutingEntry(value));
        }

        public void RemoveRoutingEntry(string entry)
        {
            node.Matrix.DeleteFromMatrix(new RoutingEntry(entry));
        }

        public string GetLog()
        {
            return node.Log.GetString();
        }
    }
}
