using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi_Common.Models
{
    //Táblázatos megjelenítéshez kell a felhasználó oldalra
    public class KonyvKliens
    {
        public long Id { get; set; }
        public string Cím { get; set; }
        public long ISBN { get; set; }
        public string Kiadó { get; set; }
        public int Kiadás_Év { get; set; }
        public string Műfajok { get; set; }
        public string Szerző { get; set; }

        public DateTime VisszaHozas { get; set; }
        public int KolcsonzottDB { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

        public KonyvKliens(long id, string cím, long iSBN, string kiadó, int kiadás_Év, string műfajok, string szerző, DateTime visszaHozas, int kolcsonzottDB)
        {
            Id = id;
            Cím = cím;
            ISBN = iSBN;
            Kiadó = kiadó;
            Kiadás_Év = kiadás_Év;
            Műfajok = műfajok;
            Szerző = szerző;
            VisszaHozas = visszaHozas;
            KolcsonzottDB = kolcsonzottDB;
        }
    }
}
