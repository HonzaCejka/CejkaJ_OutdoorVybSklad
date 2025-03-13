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
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cejka.jan\source\repos\CejkaJ_OutdoorVybSklad\CejkaJ_OutdoorVybSklad\ShopSys.mdf;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlRepos sqlRepos = new SqlRepos(cs);
            LVProd.ItemsSource = sqlRepos.GetProdejka();
        }
    }
}