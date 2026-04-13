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

namespace GVODQ6_SzervizApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseHelper dbHelper;

        public MainWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AdatokBetoltese();
        }

        private void AdatokBetoltese()
        {
            try
            {
                dgMunkalapok.ItemsSource = dbHelper.GetMunkalapok().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adatbázis hiba:\n\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtKereses_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbSzures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}