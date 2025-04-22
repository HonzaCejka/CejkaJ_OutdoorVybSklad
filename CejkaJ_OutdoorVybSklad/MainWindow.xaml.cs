using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CejkaJ_OutdoorVybSklad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentSortColumn = "Id";
        private bool isAscending = true;        
        static string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cejka.jan\Source\Repos\HonzaCejka\CejkaJ_OutdoorVybSklad\CejkaJ_OutdoorVybSklad\Database1.mdf;Integrated Security=True";
        SqlRepos sqlRepos = new SqlRepos(cs);
        public MainWindow()
        {
            InitializeComponent();            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            update(); 
        }
        public void update()
        {
            LVZbo.ItemsSource = sqlRepos.GetZbo("IdZbo", "asc");
            LVZbo2.ItemsSource = sqlRepos.GetZbo("IdZbo", "asc");
            LVProd.ItemsSource = sqlRepos.GetProdejka("idProd", "asc");
            LVZak.ItemsSource = sqlRepos.GetZak("IdZak", "asc");
            LVZak2.ItemsSource = sqlRepos.GetZak("IdZak", "asc");
        }
        private void LoadData()
        {
            SqlRepos sqlRepos = new SqlRepos(cs);
            LVProd.ItemsSource = sqlRepos.GetProdejka(currentSortColumn, isAscending ? "ASC" : "DESC");
            //LVZbo.ItemsSource = sqlRepos.GetZbo(currentSortColumn, isAscending ? "ASC" : "DESC");
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is GridViewColumnHeader header && header.Column != null)
            {
                if (header.Column.DisplayMemberBinding is Binding binding)
                {
                    string columnName = binding.Path.Path;
                    switch(columnName)
                    {
                        case "Id":
                            columnName = "dbo.Prodejka.IdProd";
                            break;
                        case"Jmeno": 
                            columnName = "Zak.Jmeno"; 
                            break;
                        case "Prijmeni":
                            columnName = "Zak.Prijmeni";
                            break;
                        case "Adresa":
                            columnName = "Zak.Adresa";
                            break;
                        case "Cena":
                            columnName = "dbo.Prodejka.Cena";
                            break;
                        case "DatumVytvoreni":
                            columnName = "dbo.Prodejka.DatumVytv";
                            break;
                    }
                    if (currentSortColumn == columnName)
                    {
                        isAscending = !isAscending;
                    }
                    else
                    {
                        currentSortColumn = columnName;
                        isAscending = true;
                    }

                    LoadData();
                }
            }
        }        

        private void LVProd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LVProd.SelectedItem != null)
            {                
                Prodejka prodejka = (Prodejka)LVProd.SelectedItem;
                List<Zbozi> listZbo = sqlRepos.GetZboZProd(prodejka.Id);
                string text = "";
                int pocet = 0;
                foreach (Zbozi zbozi in listZbo)
                {
                    pocet++;
                    text += $"Zboží: {pocet} Název: {zbozi.Nazev}, Popis: {zbozi.Popis}, Cena za ks: {zbozi.CenaKs} Kč,-, Pocet KS: {zbozi.PocetVProd} \n";
                }
                
                MessageBox.Show($"Vydejka obsahuje: \n{text}");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(LVZak2.SelectedItem != null && LVZbo2.SelectedItem != null)
            {
                int selectedZak = LVZak2.SelectedIndex + 1;
                List<int> selectedZbo = new List<int>();
                int cena = 0;
                foreach (Zbozi item in LVZbo2.SelectedItems)
                {
                    cena += item.CenaKs;
                    selectedZbo.Add(item.IdZbo);                    
                }                

                sqlRepos.addProd(selectedZak, selectedZbo, cena);
                LVProd.ItemsSource = sqlRepos.GetProdejka("IdProd","ASC");
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (LVProd.SelectedItem != null)
            {
                Prodejka prod = (Prodejka)LVProd.SelectedItem;
                sqlRepos.RemoveProd(prod.Id);
                LVProd.ItemsSource = sqlRepos.GetProdejka("IdProd", "ASC");
            }
        }

        private void ADDZAK(object sender, RoutedEventArgs e)
        {
            if (JmenoTXT.Text !="" && PrijmeniTXT.Text != " " && AdresaTXT.Text != " ")
            {
                sqlRepos.AddZak(JmenoTXT.Text,PrijmeniTXT.Text,AdresaTXT.Text);
                update();
            }
            
        }

        private void EDITZAK(object sender, RoutedEventArgs e)
        {
            if (LVZak.SelectedItem != null && JmenoTXTE.Text != "" && PrijmeniTXTE.Text != " " && AdresaTXTE.Text != " ")
            {
                Zakaznik zakaznik = (Zakaznik)LVZak.SelectedItem;
                sqlRepos.EditZak(zakaznik.IdZak,JmenoTXTE.Text, PrijmeniTXTE.Text, AdresaTXTE.Text);
                update();
            }
        }

        private void LVZak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LVZak.SelectedItem != null)
            {
                Zakaznik zakaznik = (Zakaznik)LVZak.SelectedItem;
                JmenoTXTE.Text = zakaznik.Jmeno;
                PrijmeniTXTE.Text = zakaznik.Prijmeni;
                AdresaTXTE.Text = zakaznik.Adresa;
            }
            
        }
        private void ADDZBO(object sender, RoutedEventArgs e)
        {
            if (NazevTXT.Text != "" && PopisTXT.Text != " " && PocetTXT.Text != " " && CenaTXT.Text != " ")
            {
                sqlRepos.AddZbo(NazevTXT.Text, PopisTXT.Text, int.Parse(PocetTXT.Text), int.Parse(CenaTXT.Text));
                update();
            }
        }

        private void EDITZBO(object sender, RoutedEventArgs e)
        {
            if (LVZbo.SelectedItem != null && NazevTXTE.Text != "" && PopisTXTE.Text != " " && PocetTXTE.Text != " " && CenaTXTE.Text != " ")
            {
                Zbozi zbo = (Zbozi)LVZbo.SelectedItem;
                sqlRepos.EditZbo(zbo.IdZbo,NazevTXTE.Text, PopisTXTE.Text, int.Parse(PocetTXTE.Text), int.Parse(CenaTXTE.Text));
                update();
            }
        }

        private void LVZbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVZbo.SelectedItem != null)
            {
                Zbozi zbo = (Zbozi)LVZbo.SelectedItem;
                NazevTXTE.Text = zbo.Nazev;
                PopisTXTE.Text = zbo.Popis;
                PocetTXTE.Text = zbo.PocetKsSklad.ToString();
                CenaTXTE.Text = zbo.CenaKs.ToString();                
            }
        }

        private void RemoveZAK(object sender, RoutedEventArgs e)
        {
            if(LVZak.SelectedItem != null)
            {
                Zakaznik zak = (Zakaznik)LVZak.SelectedItem;
                try
                {
                    sqlRepos.RemoveZak(zak.IdZak);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}