using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace GVODQ6_SzervizApp
{
    public partial class SzerkesztoWindow : Window
    {
        private DatabaseHelper dbHelper;
        private int munkalapId;

        public SzerkesztoWindow(DataRowView row)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();

            try
            {
                cmbUgyfelek.ItemsSource = dbHelper.GetUgyfelek().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            munkalapId = Convert.ToInt32(row["MunkalapID"]);
            txtRendszam.Text = row["Rendszam"].ToString();
            txtHiba.Text = row["Hiba_leirasa"].ToString();
            cmbUgyfelek.SelectedValue = Convert.ToInt32(row["UgyfelID"]);

            string allapot = row["Allapot"].ToString();
            foreach (ComboBoxItem item in cmbAllapot.Items)
            {
                if (item.Content.ToString() == allapot)
                {
                    item.IsSelected = true;
                    break;
                }
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

            string allapot = (cmbAllapot.SelectedItem as ComboBoxItem)?.Content.ToString();
            int ugyfelId = Convert.ToInt32(cmbUgyfelek.SelectedValue);

            try
            {
                dbHelper.FrissitMunkalap(munkalapId, rendszam, hiba, allapot, ugyfelId);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adatbázis hiba:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}