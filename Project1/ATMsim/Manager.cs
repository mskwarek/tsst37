using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* **** TODO ****
 * Pobieranie danych o elementach na żądanie od agentów, zamiast trzymania ich w tablicy.
 * Do zrealizowania przez zmianę getterów i setterów AtmSimData, możliwe, że klasa NetworkElement
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

        // j.w.
        public class Routing : Dictionary<string, string>
        {
            public Routing() : base() { }
            public Routing(Routing routing) : base((Dictionary<string, string>)routing) { }
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
            public Utils.Log Log;

            public NetworkElement(string name, Config config, Routing routing, Utils.Log log)
            {
                this.Name = name;
                this.Config = config;
                this.Routing = routing;
                this.Log = log;
            }
        }

        private static Dictionary<string, NetworkElement> network = new Dictionary<string,NetworkElement>();

        public static void AddElement(string name, Config config, Routing routing, Utils.Log log)
        {
            network.Add(name,new NetworkElement(name, config, routing, log));
        }

        public static void Reset()
        {
            network.Clear();
        }

        // pobranie listy dostepnych elementow
        public static List<string> GetElements()
        {
            List<string> elements = new List<string>();
            foreach (string key in network.Keys)
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
            if (network.ContainsKey(name))
                return network[name].Config;
            else
                return new Config();
        }

        public static void SetConfig(string name, string param, string value)
        {
            if (network.ContainsKey(name))
                network[name].Config[param] = value;
        }

        public static Routing GetRouting(string name)
        {
            if (network.ContainsKey(name))
                return network[name].Routing;
            else
                return new Routing();
        }

        public static void AddRouting(string name, string label, string value)
        {
            if (network.ContainsKey(name))
                network[name].Routing.Add(label, value);
        }

        public static void RemoveRouting(string name, string label)
        {
            if (network.ContainsKey(name))
                network[name].Routing.Remove(label);
        }

        public static void ModifyRouting(string name, string label, string newlabel, string value)
        {
            if (network.ContainsKey(name))
            {
                network[name].Routing.Remove(label);
                network[name].Routing.Add(newlabel, value);
            }
        }

        public static Utils.Log GetLog(string name)
        {
            if (network.ContainsKey(name))
                return network[name].Log;
            else
                return new Utils.Log("Brak loga dla elementu "+name+"!");
        }
    }
}
