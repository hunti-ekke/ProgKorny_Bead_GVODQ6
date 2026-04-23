using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace GVODQ6_SzervizApp
{
    public partial class UjMunkalapWindow : Window
    {
        private DatabaseHelper dbHelper;

        public UjMunkalapWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbUgyfelek.ItemsSource = dbHelper.GetUgyfelek().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az ügyfelek betöltésekor:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            lblHibaUzenet.Content = "";

            if (cmbUgyfelek.SelectedValue == null)
            {
                lblHibaUzenet.Content = "Kérlek, válassz egy ügyfelet!";
                return;
            }

            string rendszam = txtRendszam.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(rendszam))
            {
                lblHibaUzenet.Content = "A rendszám megadása kötelező!";
                return;
            }

            if (!Regex.IsMatch(rendszam, @"^([A-Z]{3}-\d{3}|[A-Z]{4}-\d{3}|[A-Z]{2}\s[A-Z]{2}-\d{3})$"))
            {
                lblHibaUzenet.Content = "Érvénytelen rendszám formátum!";
                return;
            }

            string hiba = txtHiba.Text.Trim();
            if (string.IsNullOrEmpty(hiba))
            {
                lblHibaUzenet.Content = "Kérlek, írd le a hibát!";
                return;
            }

            string status = (cmbAllapot.SelectedItem as ComboBoxItem)?.Content.ToString();
            int ugyfelId = Convert.ToInt32(cmbUgyfelek.SelectedValue);

            try
            {
                dbHelper.HozzaadMunkalap(rendszam, hiba, status, ugyfelId);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adatbázis hiba a mentés során:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}