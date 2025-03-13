using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CejkaJ_OutdoorVybSklad
{
    internal class Prodejka
    {       

        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Adresa { get; set; }
        public int Cena { get; set; }
        public DateTime DatumVytvoreni { get; set; }

        public Prodejka(int id, string jmeno, string prijmeni, string adresa, int cena, DateTime datumVytvoreni)
        {
            Id = id;
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Adresa = adresa;
            Cena = cena;
            DatumVytvoreni = datumVytvoreni;
        }
    }
}
