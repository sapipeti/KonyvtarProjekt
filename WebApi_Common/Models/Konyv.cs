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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
