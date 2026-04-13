using System.Data;
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
        private DataView munkalapokView;

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
                munkalapokView = dbHelper.GetMunkalapok().DefaultView;
                dgMunkalapok.ItemsSource = munkalapokView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adatbázis hiba:\n\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SzuroEsKeresoAlkalmazasa()
        {
            if (munkalapokView == null) return;

            string keresesText = txtKereses.Text.Trim().Replace("'", "''");
            string szuresAllapot = (cmbSzures.SelectedItem as ComboBoxItem)?.Content.ToString();

            string filter = "";

            if (!string.IsNullOrEmpty(keresesText))
            {
                filter += $"Rendszam LIKE '%{keresesText}%'";
            }

            if (szuresAllapot != "Minden" && !string.IsNullOrEmpty(szuresAllapot))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += $"Allapot = '{szuresAllapot}'";
            }

            munkalapokView.RowFilter = filter;
        }

        private void txtKereses_TextChanged(object sender, TextChangedEventArgs e)
        {
            SzuroEsKeresoAlkalmazasa();
        }

        private void cmbSzures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SzuroEsKeresoAlkalmazasa();
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgMunkalapok.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgMunkalapok.SelectedItem;
                int id = Convert.ToInt32(row["MunkalapID"]);
                string rendszam = row["Rendszam"].ToString();

                MessageBoxResult result = MessageBox.Show($"Biztosan törölni szeretnéd a(z) {rendszam} rendszámú munkalapot?",
                                                          "Törlés megerősítése",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        dbHelper.DeleteMunkalap(id);
                        AdatokBetoltese();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba a törlés során:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Válassz ki egy sort a törléshez!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}