using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AtmSim.Components
{
    public class SwitchAgent
    {
        // Referencja switcha zarzadzanego przez agenta
        Switch node;
        // Zawartosc konfiguracji wezla
        Configuration config;
        Socket managerSocket;
        byte[] buffer = new byte[4086];

        public SwitchAgent(Switch n, int port)
        {
            node = n;
            config = new Configuration(n.Name);
            Configuration psIn = new Configuration("PortsIn");
            for (int i = 0; i < node.PortsIn.Length; i++)
            {
                Configuration pIn = new Configuration(i.ToString());
                pIn.Add("Open");
                pIn.Add("Connected");
                pIn.Add("_port");
                psIn.Add(pIn);
            }
            Configuration psOut = new Configuration("PortsOut");
            for (int i = 0; i < node.PortsOut.Length; i++)
            {
                Configuration pOut = new Configuration(i.ToString());
                pOut.Add("Open");
                pOut.Add("Connected");
                pOut.Add("_port");
                psOut.Add(pOut);
            }
            config.Add("ID");
            config.Add("Name");
            config.Add(psIn);
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
                    return Serial.SerializeObject(new Routing(node.Matrix.RoutingTable));
                response += "getresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "type")
                    response += " Switch";
                if (param[0] == "ID")
                    response += " " + node.Id;
                else if (param[0] == "Name")
                    response += " " + node.Name;
                else if (param[0] == "PortsIn")
                {
                    if (param.Length != 3)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return response; }
                    if (n >= node.PortsIn.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsIn[n].Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortsIn[n].Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortsIn[n].TcpPort;
                    else return response;
                }
                else if (param[0] == "PortsOut")
                {
                    if (param.Length != 3)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return response; }
                    if (n >= node.PortsOut.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsOut[n].Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortsOut[n].Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortsOut[n].TcpPort;
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
                else if (param[0] == "PortsIn")
                {
                    if (param.Length != 3)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return response; }
                    if (n >= node.PortsIn.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsIn[n].Open; // póki co niezmienne
                    else if (param[2] == "Connected")
                        response += " " + node.PortsIn[n].Connected; // niezmienne
                    else if (param[2] == "_port")
                        response += " " + node.PortsIn[n].TcpPort; // niezmienne
                    else return response;
                }
                else if (param[0] == "PortsOut")
                {
                    if (param.Length != 3)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (ArgumentNullException) { return response; }
                    if (n >= node.PortsOut.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsOut[n].Open; // póki co niezmienne
                    else if (param[2] == "Connected")
                    {
                        if (command[2] == "True")
                            node.PortsOut[n].Connect();
                        response += " " + node.PortsOut[n].Connected;
                    }
                    else if (param[2] == "_port")
                    {
                        try
                        {
                            node.PortsOut[n].TcpPort = Int32.Parse(command[2]);
                        }
                        catch (ArgumentNullException) { return ""; }
                        response += " " + node.PortsOut[n].TcpPort;
                    }
                    else return response;
                }
            }
            else if (command[0] == "rtadd")
            {
                if (command.Length == 3)
                {
                    response += "rtaddresp " + command[1] + " " + command[2];
                    try
                    {
                        node.Matrix.RoutingTable.Add(new RoutingEntry(command[1]), new RoutingEntry(command[2]));
                    }
                    catch (ArgumentException)
                    {
                        response += " fail";
                        return response;
                    }
                    response += " ok";
                }
                else if (command.Length == 4)
                {
                    response += "rtaddresp " + command[1] + " " + command[2] + " " + command[3];
                    try
                    {
                        int id = Int32.Parse(command[3]);
                        node.Matrix.RoutingTable.Add(new RoutingEntry(command[1]), new RoutingEntry(command[2]), id);
                    }
                    catch (ArgumentException)
                    {
                        response += " fail";
                        return response;
                    }
                    response += " ok";
                }
                else return response;
            }
            else if (command[0] == "rtdel")
            {
                if (command.Length != 2)
                    return response;
                response += "rtdelresp " + command[1];
                if (node.Matrix.RoutingTable.Remove(new RoutingEntry(command[1])))
                    response += " ok";
                else
                    response += " fail";
            }
            return response;
        }
    }
}
