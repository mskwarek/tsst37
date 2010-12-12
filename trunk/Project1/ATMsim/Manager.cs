using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public static class Manager
    {
        // z tęsknoty za typedef z C++ :<
        public class Config : Dictionary<string, string>
        {
            public Config() : base() { }
            public Config(Config config) : base((Dictionary<string, string>)config) { }
        }

        // pojedynczy element sieci i jego dane
        private class NetworkElement
        {
            public string Name;     // nazwa elementu
            public Config Config;   // konfiguracja: pary parametr-wartosc
            /*
             * Tablica routingu bedzie miala postac zalezna od tego, czy element jest hostem, czy
             * routerem:
             * Dla routera beda to pary:
             * "portID;VPI;VCI" - wejsciowe
             * "portID;VPI;VCI" - wyjsciowe
             * Dla hosta beda to pary:
             * "HostID" - nazwa hosta docelowego
             * "VPI,VCI" - sciezka prowadzaca do danego hosta
             */
            public Routing Routing;
            public Log Log;

            public NetworkElement(string name, Config config, Routing routing, Log log)
            {
                this.Name = name;
                this.Config = config;
                this.Routing = routing;
                this.Log = log;
            }
        }

        private static Dictionary<string, IAgent> nodes = new Dictionary<string, IAgent>();
        private static List<Edge<string>> connections = new List<Edge<string>>();

        public static void AddNode(string name, IAgent node)
        {
            nodes.Add(name, node);
        }

        public static void AddConnection(string sourceName, string destinationName)
        {
            connections.Add(new Edge<string>(sourceName, destinationName));
        }

        public static void Reset()
        {
            nodes.Clear();
            connections.Clear();
        }

        // pobranie listy dostepnych elementow
        public static List<string> GetElements()
        {
            List<string> elements = new List<string>();
            foreach (string key in nodes.Keys)
            {
                elements.Add(key);
            }
            return elements;
        }

        /* **** TODO ****
         * Szalone miotanie wyjątkami, zamiast generowania pustaków. Niech żyją wyjątki!
         */
        public static Config GetConfig(string name)
        {
            if (nodes.ContainsKey(name))
            {
                Config config = new Config();
                string[] paramlist = nodes[name].GetParamList();
                foreach (string param in paramlist)
                {
                    config.Add(param, nodes[name].GetParam(param));
                }
                return config;
            }
            else return new Config();
        }

        public static void SetConfig(string name, string param, string value)
        {
            if (nodes.ContainsKey(name))
                nodes[name].SetParam(param, value);
        }

        public static Routing GetRouting(string name)
        {
            if (nodes.ContainsKey(name))
            {
                /*Common.RoutingTable table = nodes[name].GetRoutingTable();
                Routing routing = new Routing();
                foreach (var entry in table)
                {
                    routing.Add(entry.Key.ToString(), entry.Value.ToString());
                }*/
                return nodes[name].GetRoutingTable();
            }
            else
                return new Routing();
        }

        public static void AddRouting(string name, string label, string value)
        {
            if (nodes.ContainsKey(name))
            {
                
                nodes[name].AddRoutingEntry(label, value);
            }
        }

        public static void RemoveRouting(string name, string label)
        {
            if (nodes.ContainsKey(name))
                nodes[name].RemoveRoutingEntry(label);
        }

        public static void ModifyRouting(string name, string label, string newlabel, string value)
        {
            //if (nodes.ContainsKey(name))
            //{
            //    nodes[name].Agent.RemoveRoutingEntry(label);
            //    nodes[name].Agent.AddRoutingEntry(newlabel, value);
            //}
        }

        public static string GetLog(string name)
        {
            if (nodes.ContainsKey(name))
                return nodes[name].GetLog();
            else
                return "Brak logów dla elementu " + name;
        }

        public static void LogMsg(string name, string msg)
        {
            //if (nodes.ContainsKey(name))
            //    nodes[name].Log.LogMsg(msg);
        }

        public static void SubscribeLog(string name, ILogListener listener)
        {
            //if (nodes.ContainsKey(name))
            //    nodes[name].Log.Subscribe(listener);
        }

        public static void UnsubscribeLog(string name, ILogListener listener)
        {
            //if (nodes.ContainsKey(name))
            //    nodes[name].Log.Unsubscribe(listener);
        }

        public static List<Edge<string>> GetConnections()
        {
            return connections;
        }
    }
}
