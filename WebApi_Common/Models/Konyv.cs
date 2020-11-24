using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi_Common.Models
{
    public class Konyv
    {
        public long Id  { get; set; }
        public string Cím { get; set; }
        public long ISBN { get; set; }
        public string Kiadó { get; set; }
        public int Kiadás_Év { get; set; }
        public List<string> Műfajok { get; set; }
        public List<string> Szerző { get; set; }
        public int Darabszám { get; set; }

        public List<string> NeptunKod { get; set; }
        public List<DateTime> VisszaHozas { get; set; }
        public List<int> KolcsonzottDB { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

        public Konyv(long id, string cím, long iSBN, string kiadó, int kiadás_Év, List<string> műfajok, List<string> szerző, int darabszám, List<string> neptunKod, List<DateTime> visszaHozas, List<int> kolcsonzottDB)
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

        public Konyv()
        {
        }
    }
}
