using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim.Components
{
    class SinkAgent : IAgent
    {
        Sink node;
        Configuration config;
        Socket managerSocket;
        byte[] buffer = new byte[4086];

        public SinkAgent(Sink n, int port)
        {
            node = n;
            config = new Configuration(n.Name);
            Configuration pOut = new Configuration("PortIn");
            pOut.Add("Open");
            pOut.Add("Connected");
            pOut.Add("_port");
            config.Add("ID");
            config.Add("Name");
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
            if (command[0] == "ping")
            {
                response += "pong";
            }
            else if (command[0] == "get")
            {
                if (command.Length != 2)
                    return response;
                if (command[1] == "config")
                    return Serial.SerializeObject(config);
                //if (command[1] == "routing")
                //    return Serial.SerializeObject(GetRoutingTable());
                if (command[1] == "log")
                    return Serial.SerializeObject(node.Log);
                response += "getresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "type")
                    response += " Sink";
                if (param[0] == "ID")
                    response += " " + node.Id;
                else if (param[0] == "Name")
                    response += " " + node.Name;
                else if (param[0] == "PortIn")
                {
                    if (param.Length != 2)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortIn.Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortIn.Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortIn.TcpPort;
                    else return response;
                }
            }
            else if (command[0] == "set")
            {
                if (command.Length != 3)
                    return response;
                response += "setresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "ID")
                    response += " " + node.Id; // parametr niezmienny
                else if (param[0] == "Name")
                {
                    node.Name = command[2];
                    response += " " + node.Name;
                }
                else if (param[0] == "PortIn")
                {
                    if (param.Length != 2)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortIn.Open; // póki co niezmienne
                    else if (param[2] == "Connected")
                        response += " " + node.PortIn.Connected; // niezmienne
                    else if (param[2] == "_port")
                        response += " " + node.PortIn.TcpPort; // niezmienne
                    else return response;
                }
            }
            return response;
        }

        public string[] GetParamList()
        {
            string[] param = { "name" } ;
            return param;
        }

        public string GetParam(string name)
        {
            if (name == "name")
                return node.Name;
            else
                return "";
        }

        public void SetParam(string name, string value)
        {
            if (name == "name")
                node.Name = value;
        }

        public Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.Receiver.Sources)
            {
                table.Add(element.Key.ToString(), element.Value);
            }
            return table;
        }

        public void AddRoutingEntry(string label, string value)
        {
            if (!node.Receiver.Sources.ContainsKey(new RoutingEntry(label)))
                node.Receiver.Sources.Add(new RoutingEntry(label), value);
        }

        public void RemoveRoutingEntry(string entry)
        {
            if (node.Receiver.Sources.ContainsKey(new RoutingEntry(entry)))
                node.Receiver.Sources.Remove(new RoutingEntry(entry));
        }
  
        public string GetLog()
        {
            return node.Log.ToString();
        }
    }
}
