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
        C config;
        Socket managerSocket;
        byte[] buffer = new byte[4086];

        public SwitchAgent(Switch n, int port)
        {
            node = n;
            config = new C(n.Name);
            C psIn = new C("PortsIn");
            for (int i = 0; i < node.PortsIn.Length; i++)
            {
                C pIn = new C(i.ToString());
                pIn.Add("Open");
                pIn.Add("Connected");
                pIn.Add("_port");
                psIn.Add(pIn);
            }
            C psOut = new C("PortsOut");
            for (int i = 0; i < node.PortsOut.Length; i++)
            {
                C pOut = new C(i.ToString());
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
            
            int recv;
            try { recv = managerSocket.EndReceive(asyn); }
            catch (SocketException) { managerSocket.Close(); return; }
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
                if (command.Length == 3 && command[1] == "log")
                {
                    int n;
                    try { n = Int32.Parse(command[2]); }
                    catch (FormatException) { n = 0; }
                    if (n == 0)
                        return Serial.SerializeObject(node.Log);
                    else
                        return Serial.SerializeObject(new Log(node.Log, n));
                }
                if (command.Length != 2)
                    return "getresp";
                if (command[1] == "config")
                    return Serial.SerializeObject(config);
                if (command[1] == "routing")
                    return Serial.SerializeObject(new Routing(node.Matrix.RoutingTable));
                response += "getresp " + command[1];
                string[] param = command[1].Split('.');
                if (param[0] == "type")
                    response += " Switch";
                else if (param[0] == "ID")
                    response += " " + node.Id;
                else if (param[0] == "Name")
                    response += " " + node.Name;
                else if (param[0] == "PortsIn")
                {
                    if (param.Length != 3 && param.Length != 5)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (FormatException) { return response; }
                    if (n >= node.PortsIn.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsIn[n].Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortsIn[n].Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortsIn[n].TcpPort;
                    else if (param[2] == "Available")
                    {
                        try
                        {
                            int vpi = Int32.Parse(param[3]);
                            int vci;
                            if (param[4] == "-")
                                vci = -1;
                            else
                                vci = Int32.Parse(param[4]);
                            response += " " + CheckPortIn(n, vpi, vci);
                        }
                        catch (FormatException) { return response; }
                    }
                }
                else if (param[0] == "PortsOut")
                {
                    if (param.Length != 3 && param.Length != 5)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (FormatException) { return response; }
                    if (n >= node.PortsOut.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsOut[n].Open;
                    else if (param[2] == "Connected")
                        response += " " + node.PortsOut[n].Connected;
                    else if (param[2] == "_port")
                        response += " " + node.PortsOut[n].TcpPort;
                    else if (param[2] == "Available")
                    {
                        try
                        {
                            int vpi = Int32.Parse(param[3]);
                            int vci;
                            if (param[4] == "-")
                                vci = -1;
                            else
                                vci = Int32.Parse(param[4]);
                            response += " " + CheckPortOut(n, vpi, vci);
                        }
                        catch (FormatException) { return response; }
                    }
                }
            }
            else if (command[0] == "set")
            {
                if (command.Length != 3)
                    return "setresp";
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
                    catch (FormatException) { return response; }
                    if (n >= node.PortsIn.Length)
                        return response;
                    if (param[2] == "Open")
                        response += " " + node.PortsIn[n].Open; // póki co niezmienne
                    else if (param[2] == "Connected")
                        response += " " + node.PortsIn[n].Connected; // niezmienne
                    else if (param[2] == "_port")
                        response += " " + node.PortsIn[n].TcpPort; // niezmienne
                }
                else if (param[0] == "PortsOut")
                {
                    if (param.Length != 3)
                        return response;
                    int n;
                    try { n = Int32.Parse(param[1]); }
                    catch (FormatException) { return response; }
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
                        catch (FormatException) { return response; }
                        response += " " + node.PortsOut[n].TcpPort;
                    }
                }
                node.Log.LogMsg(response);
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
                        RoutingEntry outcoming = new RoutingEntry(command[2]);
                        if (node.PortsIn.Length > incoming.Port && node.PortsOut.Length > outcoming.Port)
                        {
                            node.Matrix.RoutingTable.Add(incoming, outcoming);
                            response += " ok";
                        }
                        else
                            response += " fail";
                    }
                    catch (FormatException) { response += " fail"; }
                    catch (ArgumentException) { response += " fail"; }
                }
                else if (command.Length == 4)
                {
                    response += command[1] + " " + command[2] + " " + command[3];
                    try
                    {
                        int id = Int32.Parse(command[3]);
                        RoutingEntry incoming = new RoutingEntry(command[1]);
                        RoutingEntry outcoming = new RoutingEntry(command[2]);
                        if (node.PortsIn.Length > incoming.Port && node.PortsOut.Length > outcoming.Port)
                        {
                            node.Matrix.RoutingTable.Add(incoming, outcoming, id);
                            response += " ok";
                        }
                        else
                            response += " fail";
                    }
                    catch (FormatException) { response += " fail"; }
                    catch (ArgumentException) { response += " fail"; }
                }
                node.Log.LogMsg(response);
            }
            else if (command[0] == "rtdel")
            {
                response += "rtdelresp ";
                if (command.Length != 2)
                    return response;
                response += command[1];
                try
                {
                    int c = Int32.Parse(command[1]);
                    if (node.Matrix.RoutingTable.Remove(c))
                        response += " ok";
                    else
                        response += " fail";
                }
                catch (FormatException)
                {
                    try
                    {
                        if (node.Matrix.RoutingTable.Remove(new RoutingEntry(command[1])))
                            response += " ok";
                        else
                            response += " fail";
                    }
                    catch (FormatException) { response += " fail"; }
                }
                node.Log.LogMsg(response);
            }
            else
                response = command[0] + "resp";
            return response;
        }

        private bool CheckPortIn(int port, int vpi, int vci)
        {
            if (vci == -1)
                return !node.Matrix.RoutingTable.ContainsKey(new RoutingEntry(port, vpi));
            else
                return !node.Matrix.RoutingTable.ContainsKey(new RoutingEntry(port, vpi, vci));
        }

        private bool CheckPortOut(int port, int vpi, int vci)
        {
            if (vci == -1)
                return !node.Matrix.RoutingTable.ContainsValue(new RoutingEntry(port, vpi));
            else
                return !node.Matrix.RoutingTable.ContainsValue(new RoutingEntry(port, vpi, vci));
        }
    }
}
