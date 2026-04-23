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
            cmbOrder.ItemsSource = new List<Mezo>
{
                new Mezo { Nev = "MunkalapID", Cimke = "Azonosító" },
                new Mezo { Nev = "Rendszam", Cimke = "Rendszám" },
                new Mezo { Nev = "Hiba_leirasa", Cimke = "Leírás" },
                new Mezo { Nev = "Allapot", Cimke = "Állapot" },
                new Mezo { Nev = "Rogzites_datuma", Cimke = "Rögzítés dátuma" },
                new Mezo { Nev = "UgyfelID", Cimke = "Ügyfél azonosító" }
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
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

        private void SearchAndFilterAndOrder()
        {
            if (munkalapokView == null) return;

            string searchText = txtSearch.Text.Trim().Replace("'", "''");
            string filterStatus = (cmbFilter.SelectedItem as ComboBoxItem).Content.ToString();

            string filter = "";

            if (!string.IsNullOrEmpty(searchText))
            {
                filter += $"Rendszam LIKE '%{searchText}%'";
            }

            if (filterStatus != "Minden" && !string.IsNullOrEmpty(filterStatus))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += $"Allapot = '{filterStatus}'";
            }

            munkalapokView.Sort = cmbOrder.SelectedValue.ToString();
            munkalapokView.RowFilter = filter;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchAndFilterAndOrder();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndFilterAndOrder();
        }

        private void cmbOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAndFilterAndOrder();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
                        LoadData();
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
            UjMunkalapWindow ujAblak = new UjMunkalapWindow();
            ujAblak.Owner = this;

            if (ujAblak.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void btnSzerkeszt_Click(object sender, RoutedEventArgs e)
        {
            if (dgMunkalapok.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgMunkalapok.SelectedItem;
                SzerkesztoWindow szerkeszto = new SzerkesztoWindow(row);
                szerkeszto.Owner = this;

                if (szerkeszto.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Válassz ki egy sort a szerkesztéshez!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnUjUgyfel_Click(object sender, RoutedEventArgs e)
        {
            UjUgyfelWindow ugyfelAblak = new UjUgyfelWindow();
            ugyfelAblak.Owner = this;

            ugyfelAblak.ShowDialog();
        }
    }

    public class Mezo
    {
        public string Nev { get; set; }
        public string Cimke { get; set; }
    }
}