using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace SkiEquipmentRental2.ViewModel
{
    /// <summary>
    /// Interaction logic for Rent.xaml
    /// </summary>
    public partial class Rent : UserControl
    {
        public Rent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";
            string query = "select w.IDWypozyczenia,p.IDPracownik,r.Rodzaj,s.Marka,s.Model,k.Imie,k.Nazwisko,Data_wyp,Liczba_godz_wyp,Liczba_godz_wyp * Cena_godz as doZaplaty FROM tblWypozyczenia w JOIN tblKlienci k on w.IDKlient = k.IDKlient JOIN tblPracownicy p on w.IDPracownik = p.IDPracownik JOIN tblSprzet s on w.IDSprzet = s.IDSprzet JOIN tblRodzaj r on s.IDRodzaj = r.IDRodzaj JOIN tblCennik c on s.Klasa = c.Klasa";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);

                    dataGrid4.AutoGenerateColumns = false;
                    //dataGrid.DataContext = dtbl;
                    dataGrid4.ItemsSource = dtbl.DefaultView;
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true; // Zatrzymaj propagację zdarzenia, aby uniemożliwić wprowadzanie niepoprawnych znaków
            }
        }

        private void ID_TextChanged(object sender, RoutedEventArgs e)
        {
            // Czyszczenie zawartości TextBoxa po kliknięciu
            IDPracownik.Text = string.Empty;
        }

        private void BrandFocus(object sender, RoutedEventArgs e)
        {
            // Czyszczenie zawartości TextBoxa po kliknięciu
            idStuff.Text = string.Empty;
        }

        private void ModelFocus(object sender, RoutedEventArgs e)
        {
            // Czyszczenie zawartości TextBoxa po kliknięciu
            idCliennt.Text = string.Empty;
        }


        private void HoursFocus(object sender, RoutedEventArgs e)
        {
            // Czyszczenie zawartości TextBoxa po kliknięciu
            Hourss.Text = string.Empty;
        }


        private void RefreshData()
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";
            string query = "select w.IDWypozyczenia,p.IDPracownik,r.Rodzaj,s.Marka,s.Model,k.Imie,k.Nazwisko,Data_wyp,Liczba_godz_wyp,Liczba_godz_wyp * Cena_godz as doZaplaty FROM tblWypozyczenia w JOIN tblKlienci k on w.IDKlient = k.IDKlient JOIN tblPracownicy p on w.IDPracownik = p.IDPracownik JOIN tblSprzet s on w.IDSprzet = s.IDSprzet JOIN tblRodzaj r on s.IDRodzaj = r.IDRodzaj JOIN tblCennik c on s.Klasa = c.Klasa";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);

                    dataGrid4.ItemsSource = dtbl.DefaultView;
                }
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string id = IDPracownik.Text;
            string idStufff = idStuff.Text;
            string idClient = idCliennt.Text;
            string hours = Hourss.Text;
            DateTime? selectedDate = Date.SelectedDate;


            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";


            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                string query = "INSERT INTO tblWypozyczenia (IDPracownik, IDSprzet, IDKlient,Data_Wyp,Liczba_godz_wyp) VALUES (@id, @idStufff, @idClient, @selectedDate,@Hours)";

                //SqlCommand insertCmd = new SqlCommand(query, con);
                //insertCmd.Parameters.AddWithValue("@IDPracownik", int.Parse(id));
                //insertCmd.Parameters.AddWithValue("@IDSprzet", idStufff);
                //insertCmd.Parameters.AddWithValue("@IDKlient", idClient);
                //insertCmd.Parameters.AddWithValue("@Data_Wyp", selectedDate);
                //insertCmd.Parameters.AddWithValue("@Liczba_godz_wyp", hours);
               

                SqlCommand insertCmd = new SqlCommand(query, con);
                insertCmd.Parameters.AddWithValue("@id", id);
                insertCmd.Parameters.AddWithValue("@idStufff", idStufff);
                insertCmd.Parameters.AddWithValue("@idClient", idClient);
                insertCmd.Parameters.AddWithValue("@selectedDate", selectedDate);
                insertCmd.Parameters.AddWithValue("@hours", int.Parse(hours));


                try
                {
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Wypożyczenie dodane!.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Wystąpił błąd przy dodawaniu wypożyczenia:\n" + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               RefreshData();
            }


        }
    }
}
