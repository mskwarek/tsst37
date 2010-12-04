using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class AdaptacionLayer
    {   //czyli AAL . Mapujemy strumien uzytkowy data do dataunitow

        private Data dat;                   //nasze data

        public void SetDataToMap(Data d) { dat = d; }  //wstawienie data do mapowania

        public Data GetDataToMap() { return dat; }  //zwracanie data ktore mamy mapowac

        public DataUnit[] MapData()
        {    //mapowanie data. Zwraca nam tablice DataUnitow gdzie kazy ma po 48B ze strumienia uzytkowego Data

            int len = dat.GetId().Length; //najpierw pobieramy wielkos strumienia do mapowania
            DataUnit[] dataunittab = new DataUnit[len]; //tu okreslamy ile bedzie DataUnitow po dlugosci strumienia

            while (len > 0)  // to wpisuje do poszczegolnych DataUnitow czesci strumienia uzytkowego
            {
                DataUnit du = new DataUnit();
                du.SetId(dat.GetElement(len));
                dataunittab[len - 1] = du;
                len--;
            }

            return dataunittab;  // i zwraca ostatecznie tablice otzrymanych DataUnitow
        }




    }
}
