using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi_Common.Models
{
    public class FelhasznaloAdatok
    {
        public string neptunKod { get; set; }
        public string jelszo { get; set; }

        public FelhasznaloAdatok(string neptunKod, string jelszo)
        {
            this.neptunKod = neptunKod;
            this.jelszo = jelszo;
        }

        public FelhasznaloAdatok()
        {
        }

        public override string ToString()
        {
            return neptunKod;
        }
    }
}
