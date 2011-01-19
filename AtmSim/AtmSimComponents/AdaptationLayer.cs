using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    //czyli AAL . Mapujemy strumien uzytkowy data do dataunitow
    class AdaptationLayer
    {
        private Data data;

        public void SetDataToMap(Data d) { data = d; }  //wstawienie data do mapowania

        public Data GetDataToMap() { return data; }  //zwracanie data ktore mamy mapowac

        //mapowanie data. Zwraca nam tablice DataUnitow gdzie kazy ma po 48B ze strumienia uzytkowego Data
        public DataUnit[] MapData()
        {
            int len = data.GetId().Length; //najpierw pobieramy wielkos strumienia do mapowania
            DataUnit[] dataunittab = new DataUnit[len]; //tu okreslamy ile bedzie DataUnitow po dlugosci strumienia

            while (len > 0)  // to wpisuje do poszczegolnych DataUnitow czesci strumienia uzytkowego
            {
                DataUnit du = new DataUnit();
                du.Id = data.GetElement(len);
                dataunittab[len - 1] = du;
                len--;
            }

            return dataunittab;  // i zwraca ostatecznie tablice otzrymanych DataUnitow
        }
    }
}
