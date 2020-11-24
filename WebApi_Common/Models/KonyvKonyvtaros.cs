using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi_Common.Models
{
    public class KonyvKonyvtaros
    {
        public long Id { get; set; }
        public string Cím { get; set; }
        public long ISBN { get; set; }
        public string Kiadó { get; set; }
        public int Kiadás_Év { get; set; }
        public string Műfajok { get; set; }
        public string Szerző { get; set; }
        public int Darabszám { get; set; }

        public string NeptunKod { get; set; }
        public string VisszaHozas { get; set; }
        public string KolcsonzottDB { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

        public KonyvKonyvtaros(long id, string cím, long iSBN, string kiadó, int kiadás_Év, string műfajok, string szerző, int darabszám, string neptunKod, string visszaHozas, string kolcsonzottDB)
        {
            Id = id;
            Cím = cím;
            ISBN = iSBN;
            Kiadó = kiadó;
            Kiadás_Év = kiadás_Év;
            Műfajok = műfajok;
            Szerző = szerző;
            Darabszám = darabszám;
            NeptunKod = neptunKod;
            VisszaHozas = visszaHozas;
            KolcsonzottDB = kolcsonzottDB;
        }
    }
}
