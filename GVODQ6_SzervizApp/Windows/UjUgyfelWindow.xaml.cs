using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace GVODQ6_SzervizApp
{
    public partial class UjUgyfelWindow : Window
    {
        private DatabaseHelper dbHelper;

        public UjUgyfelWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            lblHiba.Content = "";
            string name = txtName.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                lblHiba.Content = "Minden mezőt ki kell tölteni!";
                return;
            }

            if (!Regex.IsMatch(phone, @"^(?:\+36|06)(?:[\s\-\(\)]*\d){8,9}$"))
            {
                lblHiba.Content = "Érvénytelen telefonszám formátum!";
                return;
            }

            try
            {
                dbHelper.HozzaadUgyfel(name, phone);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}