using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CejkaJ_OutdoorVybSklad
{
    internal class SqlRepos
    {
        public string ConnectionString { get; set; }
        public SqlRepos(string connectionString)
        {
            //ConnectionString = skola
            //ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cejka.jan\source\repos\CejkaJ_OutdoorVybSklad\CejkaJ_OutdoorVybSklad\ShopSys.mdf;Integrated Security=True";
            //ConnectionString = doma
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Cejka\source\repos\HonzaCejka\CejkaJ_OutdoorVybSklad\CejkaJ_OutdoorVybSklad\DB.mdf;Integrated Security=True";
        }

        public List<Prodejka> GetProdejka()
        {
            List<Prodejka> Prod= new List<Prodejka>();
            SqlConnection conn = new SqlConnection(ConnectionString);
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM dbo.Prodejka JOIN dbo.Zak ON dbo.Prodejka.IdZak = dbo.Zak.IdZak;";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            Prodejka prodejka = new Prodejka
                            (
                                 (int)reader["IdProd"],
                                 (string)reader["Jmeno"],
                                 (string)reader["Prijmeni"],
                                 (string)reader["Adresa"],
                                 (int)reader["Cena"],
                                 (DateTime)reader["DatumVytv"]

                            );
                                                   
                            Prod.Add(prodejka);
                        }
                    }
                    conn.Close();
                    return Prod;
                }            

        }
    }
}
