using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* **** TODO ****
 * Pobieranie danych o elementach na żądanie od agentów, zamiast trzymania ich w tablicy.
 * Do zrealizowania przez zmianę getterów i setterów AtmSimData, możliwe, że klasa NetworkElement
 * okaże się zbędna.
 */

namespace ATMsim
{
    // Ta klasa jest przeznaczona do przechowywania danych o elementach sieci na potrzeby zarzadzania.
    class AtmSimData
    {
        // pojedynczy element sieci i jego dane
        private class NetworkElement
        {
            public string name;                         // nazwa elementu
            public Dictionary<string, string> config;   // konfiguracja: pary parametr-wartosc
            /* Tablica routingu bedzie miala postac zalezna od tego, czy element jest hostem, czy
             * routerem:
             * Dla routera beda to pary:
             * "portID;VPI;VCI" - wejsciowe
             * "portID;VPI;VCI" - wyjsciowe
             * Dla hosta beda to pary:
             * "HostID" - nazwa hosta docelowego
             * "VPI,VCI" - sciezka prowadzaca do danego hosta
             */
            public Dictionary<string, string> routing;
            public Utils.Log log;

            public NetworkElement(string name, Dictionary<string, string> config, Dictionary<string, string> routing, Utils.Log log)
            {
                this.name = name;
                this.config = config;
                this.routing = routing;
                this.log = log;
            }
        }

        private Dictionary<string, NetworkElement> network;

        public AtmSimData()
        {
            network = new Dictionary<string,NetworkElement>();
        }

        public void addNetworkElement(string name, Dictionary<string, string> config, Dictionary<string, string> routing, Utils.Log log)
        {
            network.Add(name,new NetworkElement(name, config, routing, log));
        }

        // pobranie listy dostepnych elementow
        public List<string> getElements()
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
        public Dictionary<string, string> getConfig(string name)
        {
            if (network.ContainsKey(name))
                return network[name].config;
            else
                return new Dictionary<string,string>();
        }

        public Dictionary<string, string> getRouting(string name)
        {
            if (network.ContainsKey(name))
                return network[name].routing;
            else
                return new Dictionary<string, string>();
        }

        public Utils.Log getLog(string name)
        {
            if (network.ContainsKey(name))
                return network[name].log;
            else
                return new Utils.Log("Brak loga dla elementu "+name+"!");
            }
    }
}
