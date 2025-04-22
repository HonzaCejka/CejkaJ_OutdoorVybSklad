using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using System.Windows.Controls.Primitives;

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
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cejka.jan\Source\Repos\HonzaCejka\CejkaJ_OutdoorVybSklad\CejkaJ_OutdoorVybSklad\Database1.mdf;Integrated Security=True";
        }

        public List<Prodejka> GetProdejka(string orderBy,string orderDir)
        {
            List<Prodejka> Prod= new List<Prodejka>();
            SqlConnection conn = new SqlConnection(ConnectionString);
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = $"SELECT * FROM dbo.Prodejka JOIN dbo.Zak ON dbo.Prodejka.IdZak = dbo.Zak.IdZak ORDER BY {orderBy} {orderDir}";                                

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
        public List<Zbozi> GetZbo(string orderBy, string orderDir)
        {
            List<Zbozi> ZboList = new List<Zbozi>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"SELECT * FROM ZBOZI ORDER BY {orderBy} {orderDir}";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zbozi zbo = new Zbozi
                        (
                             (int)reader["IdZbo"],
                             (string)reader["Nazev"],
                             (string)reader["Popis"],
                             (int)reader["PocetKsSklad"],
                             (int)reader["CenaKs"]
                        );

                        ZboList.Add(zbo);
                    }
                }
                conn.Close();
                return ZboList;
            }
        }
        public List<Zakaznik> GetZak(string orderBy, string orderDir)
        {
            List<Zakaznik> ZakLIst = new List<Zakaznik>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"SELECT * FROM Zak ORDER BY {orderBy} {orderDir}";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zakaznik zak = new Zakaznik
                        (
                             (int)reader["IdZak"],
                             (string)reader["Jmeno"],
                             (string)reader["Prijmeni"],
                             (string)reader["Adresa"]                                                                                       
                        );

                        ZakLIst.Add(zak);
                    }
                }
                conn.Close();
                return ZakLIst;
            }
        }
        public List<Zbozi> GetZboZProd(int IdProd)
        {
            List<Zbozi> ZboList = new List<Zbozi>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"SELECT Zbozi.Nazev, Zbozi.Popis,Zbozi.CenaKs,ZboNM.pocet from Zbozi JOIN ZboNM on (ZboNM.IdZbo = Zbozi.IdZbo) where ZboNM.IdProd = {IdProd};";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zbozi zbo = new Zbozi
                        (                             
                             (string)reader["Nazev"],
                             (string)reader["Popis"],                             
                             (int)reader["CenaKs"],
                             (int)reader["pocet"]
                        );

                        ZboList.Add(zbo);
                    }
                }
                conn.Close();
                return ZboList;
            }
        }
        public void addProd(int idZak,List<int> idZbo,int cena)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT INTO dbo.Prodejka (IdZak,Cena,DatumVytv) Values (@IdZak,@Cena,GETDATE()); SELECT SCOPE_IDENTITY() AS LastInsertedID;";
                cmd.Parameters.AddWithValue("@IdZak", idZak);
                cmd.Parameters.AddWithValue("@Cena", cena);
                int insertedId = Convert.ToInt32(cmd.ExecuteScalar());                
                foreach (int id in idZbo)
                {                    
                    using (SqlCommand cmdInsert = conn.CreateCommand())
                    { 
                        cmdInsert.CommandText = "INSERT INTO dbo.ZboNM (IdZbo,IdProd,pocet) Values (@IdZbo,@IdProd,1);";
                        cmdInsert.Parameters.AddWithValue("@IdZbo",id);
                        cmdInsert.Parameters.AddWithValue("@IdProd", insertedId);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }
        public void RemoveProd(int id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"DELETE FROM ZboNM WHERE IdProd = @IdProd;DELETE FROM Prodejka WHERE IdProd = @IdProd;";
                cmd.Parameters.AddWithValue("@IdProd", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void AddZak(string jmeno, string prijmeni, string adresa)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT INTO dbo.Zak (Jmeno,Prijmeni,Adresa) Values (@Jmeno,@Prijmeni,@Adresa)";
                cmd.Parameters.AddWithValue("@Jmeno", jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", prijmeni);
                cmd.Parameters.AddWithValue("@Adresa", adresa);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void EditZak(int id,string jmeno, string prijmeni, string adresa)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"Update Zak set Jmeno = @Jmeno,Prijmeni = @Prijmeni,Adresa = @Adresa where IdZak = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Jmeno", jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", prijmeni);
                cmd.Parameters.AddWithValue("@Adresa", adresa);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void RemoveZak(int id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "Delete from Zak where IdZak = @Id";
                cmd.Parameters.AddWithValue ("@Id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void AddZbo(string nazev, string popis, int pocetSklad,int cena)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"INSERT INTO dbo.Zbozi (Nazev,Popis,PocetKsSklad,CenaKs) Values (@Nazev,@Popis,@PocetKsSklad,@Cena)";
                cmd.Parameters.AddWithValue("@Nazev", nazev);
                cmd.Parameters.AddWithValue("@Popis", popis);
                cmd.Parameters.AddWithValue("@PocetKsSklad", pocetSklad);
                cmd.Parameters.AddWithValue("@Cena", cena);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void EditZbo(int id, string nazev, string popis, int pocetSklad, int cena)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();                
                cmd.CommandText = $"Update Zbozi set Nazev = @Nazev,Popis = @Popis,PocetKsSklad = @PocetKsSklad,CenaKs = @Cena where IdZbo = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nazev", nazev);
                cmd.Parameters.AddWithValue("@Popis", popis);
                cmd.Parameters.AddWithValue("@PocetKsSklad", pocetSklad);
                cmd.Parameters.AddWithValue("@Cena", cena);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
