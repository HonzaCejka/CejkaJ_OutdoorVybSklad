using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CejkaJ_OutdoorVybSklad
{
    internal class Zbozi
    {        
        public int IdZbo { get; set; }
        public string Nazev { get; set; }
        public string Popis { get; set; }
        public int PocetKsSklad { get; set; }
        public int CenaKs { get; set; }
        public int PocetVProd { get; set; }

        public Zbozi(int idZbo, string nazev, string popis, int pocetKsSklad, int cenaKs)
        {
            IdZbo = idZbo;
            Nazev = nazev;
            Popis = popis;
            PocetKsSklad = pocetKsSklad;
            CenaKs = cenaKs;
        }

        public Zbozi(string nazev, string popis, int cenaKs, int pocetVProd)
        {
            Nazev = nazev;
            Popis = popis;            
            CenaKs = cenaKs;
            PocetVProd = pocetVProd;
        }
    }
}
