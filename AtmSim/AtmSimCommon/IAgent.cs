using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    /**
     * Interfejs agenta urzadzenia sieciowego - metody, ktore musza byc dostepne dla Managera dla prawidlowego dzialania.
     **/
    public interface IAgent
    {
        // Pobiera liste konfigurowalnych parametrow wezla
        string[] GetParamList();
        // Pozwala pobrac wartosc wybranego parametru
        string GetParam(string name);
        // Ustawia wybrany parametr na zadana wartosc
        void SetParam(string name, string value);
        // Pobiera tablice routingu urzadzenia (tablice polaczen dla hosta)
        Routing GetRoutingTable();
        // Pozwala na dodanie wpisu do tablicy kierowania
        void AddRoutingEntry(string label, string value);
        // Pozwala na usuniecie wpisu z tablicy kierowania
        void RemoveRoutingEntry(string entry);
        // Pobiera log urzadzenia
        string GetLog();
    }
}
