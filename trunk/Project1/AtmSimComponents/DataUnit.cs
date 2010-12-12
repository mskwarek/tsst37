using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class DataUnit
    {   // reprezentuje 48B kawalek Data

        private string id;  // czyli litera duza symbolizujaca ta wiadomosc 48B
        public string Id { get { return id; } set { id = value; } }

    }
}
