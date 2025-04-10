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
            LVZbo.ItemsSource = sqlRepos.GetZbo("IdZbo", "asc");
            LVZbo2.ItemsSource = sqlRepos.GetZbo("IdZbo", "asc");
            LVProd.ItemsSource = sqlRepos.GetProdejka("idProd","asc");
            LVZak.ItemsSource = sqlRepos.GetZak("IdZak","asc");
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
                List<Zbozi> listZbo = sqlRepos.GetZboZProd(LVProd.SelectedIndex+3);
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

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}