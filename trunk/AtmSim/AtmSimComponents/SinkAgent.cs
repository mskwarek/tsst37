using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim.Components
{
    public class SinkAgent
    {
        Sink node;
        Configuration config;
        Socket managerSocket;
        byte[] buffer = new byte[4086];

        public SinkAgent(Sink n, int port)
        {
            node = n;
            config = new Configuration(n.Name);
            Configuration pOut = new Configuration("0");
            pOut.Add("Open");
            pOut.Add("Connected");
            pOut.Add("_port");
            Configuration psOut = new Configuration("PortsIn");
            psOut.Add(pOut);
            config.Add("ID");
            config.Add("Name");
            config.Add(psOut);

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
                if (command[1] == "log" && command.Length == 3)
                {
                    int n;
                    try { n = Int32.Parse(command[2]); }
                    catch (ArgumentNullException) { n = 0; }
                    if (n == 0)
                        return Serial.SerializeObject(node.Log);
                    else
                        return Serial.SerializeObject(new Log(node.Log, n));
                }
                if (command.Length != 2)
                    return response;
                if (command[1] == "config")
                    return Serial.SerializeObject(config);
                if (command[1] == "routing")
                    return Serial.SerializeObject(GetRoutingTable());
                response += "getresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "type")
                    response += " Sink";
                if (param[0] == "ID")
                    response += " " + node.Id;
                else if (param[0] == "Name")
                    response += " " + node.Name;
                else if (param[0] == "PortsIn")
                {
                    if (param.Length != 3 && param.Length != 5)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortIn.Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortIn.Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortIn.TcpPort;
                    else if (param[2] == "Available")
                    {
                        try
                        {
                            int n = Int32.Parse(param[1]);
                            int vpi = Int32.Parse(param[3]);
                            int vci = Int32.Parse(param[4]);
                            response += " " + CheckPortIn(n, vpi, vci);
                        }
                        catch (ArgumentNullException) { return response; }
                    }
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
                else if (param[0] == "PortsIn")
                {
                    if (param.Length != 3)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortIn.Open; // póki co niezmienne
                    else if (param[2] == "Connected")
                        response += " " + node.PortIn.Connected; // niezmienne
                    else if (param[2] == "_port")
                        response += " " + node.PortIn.TcpPort; // niezmienne
                }
            }
            else if (command[0] == "rtadd")
            {
                response += "rtaddresp ";
                if (command.Length == 3)
                {
                    response += command[1] + " " + command[2];
                    try
                    {
                        RoutingEntry incoming = new RoutingEntry(command[1]);
                        if (1 > incoming.Port)
                        {
                            node.Receiver.Sources.Add(incoming, command[2]);
                            response += " ok";
                        }
                        else
                            response += " fail";
                    }
                    catch (ArgumentException)
                    {
                        response += " fail";
                    }
                }
            }
            else if (command[0] == "rtdel")
            {
                if (command.Length != 2)
                    return response;
                response += "rtdelresp " + command[1];
                if (node.Receiver.Sources.Remove(new RoutingEntry(command[1])))
                    response += " ok";
                else
                    response += " fail";
            }
            else
                response = command[0] + "resp";
            return response;
        }

        private Routing GetRoutingTable()
        {
            Routing table = new Routing();
            foreach (var element in node.Receiver.Sources)
            {
                table.Add(element.Key.ToString(), element.Value);
            }
            return table;
        }

        private bool CheckPortIn(int port, int vpi, int vci)
        {
            return !node.Receiver.Sources.ContainsKey(new RoutingEntry(port, vpi, vci));
        }
    }
}
