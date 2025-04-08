using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CejkaJ_OutdoorVybSklad
{
    internal class Zakaznik
    {        
        public int IdZak { get; set; }
        public string Jmeno { get; set;}
        public string Prijmeni { get; set; }
        public string Adresa { get; set; }

        public Zakaznik(int idZak, string jmeno, string prijmeni, string adresa)
        {
            IdZak = idZak;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Adresa = adresa;
        }
    }
}
