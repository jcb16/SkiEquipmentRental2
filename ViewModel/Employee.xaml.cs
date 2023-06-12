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

namespace SkiEquipmentRental2.ViewModel
{

    public partial class Employee
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;

        private void selectEmployee_Click(object sender, RoutedEventArgs e)
        {
            //string connstring = "Data Source = DESKTOP-RVQS4VV; Initial Catalog = WypozyczalniaFull; Integrated Security = true";
            //SqlConnection con = new SqlConnection(connstring);
            //con.Open();

            ////string query = $"Select * from tblPracownicy";

            ////SqlCommand cmd = new SqlCommand(query, con);

            //SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from tblPracownicy", con);

            //DataTable dtbl = new DataTable();
            //sqlDa.Fill(dtbl);

            //dataGrid.DataSource = dtbl;


            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";
            string query = "SELECT * FROM tblPracownicy";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);

                    dataGrid.AutoGenerateColumns = false;
                    //dataGrid.DataContext = dtbl;
                    dataGrid.ItemsSource = dtbl.DefaultView;
                }
            }
        }

        private void UpdateData(object sender, RoutedEventArgs e)
        {

            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";
            string updateQuery = "UPDATE tblPracownicy SET Imie = @Imie, Nazwisko = @Nazwisko, Data_ur = @Data_ur, PESEL = @PESEL, Numer_tel = @Numer_tel,Login_prac = @Login_prac,  Password_prac=@Password_prac WHERE IDPracownik = @IDPracownik";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                {
                    foreach (DataRowView rowView in dataGrid.ItemsSource)
                    {
                        DataRow row = rowView.Row;
                        
                        updateCmd.Parameters.Clear();
                        updateCmd.Parameters.AddWithValue("@Imie", row["Imie"].ToString());
                        updateCmd.Parameters.AddWithValue("@Nazwisko", row["Nazwisko"].ToString());
                        updateCmd.Parameters.AddWithValue("@Data_ur", Convert.ToDateTime(row["Data_ur"]));
                        updateCmd.Parameters.AddWithValue("@PESEL", row["PESEL"].ToString());
                        updateCmd.Parameters.AddWithValue("@Numer_tel", row["Numer_tel"].ToString());
                        updateCmd.Parameters.AddWithValue("@Login_prac", row["Login_prac"].ToString());
                        updateCmd.Parameters.AddWithValue("@Password_prac", row["Password_prac"].ToString());
                        updateCmd.Parameters.AddWithValue("@IDPracownik", Convert.ToInt32(row["IDPracownik"]));


                        updateCmd.ExecuteNonQuery();
                    }
                }
            }


        }
        private int GetMaxID(SqlConnection con)
        {
            string query = "SELECT MAX(IDPracownik) FROM tblPracownicy";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        private void RefreshData()
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";
            string query = "SELECT * FROM tblPracownicy";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);

                    dataGrid.ItemsSource = dtbl.DefaultView;
                }
            }
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                int maxID = GetMaxID(con); // Pobranie maksymalnej wartości IDPracownik

                foreach (DataRowView rowView in dataGrid.ItemsSource)
                {
                    DataRow row = rowView.Row;

                    // Pomiń rekord, który jest już dodany do bazy danych
                    if (row.RowState != DataRowState.Added)
                        continue;

                    // Pobierz wartość hasła z wiersza
                    string password = row["Password_prac"].ToString();

                    // Sprawdź warunek dla wymagań hasła
                    if (password.Length <= 5)
                    {
                        // Hasło nie spełnia wymagań - komunikat
                        MessageBox.Show("Hasło musi mieć co najmniej 6 znaków.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        RefreshData();
                        return; // Przerwij proces dodawania pracownika
                    }

                    string query = "INSERT INTO tblPracownicy (IDPracownik, Imie, Nazwisko, Data_ur, PESEL, Numer_tel, Login_prac, Password_prac) " +
                                   "VALUES (@IDPracownik, @Imie, @Nazwisko, @Data_ur, @PESEL, @Numer_tel, @Login_prac, @Password_prac)";

                    SqlCommand insertCmd = new SqlCommand(query, con);
                    insertCmd.Parameters.AddWithValue("@IDPracownik", maxID + 1); // Zwiększ wartość IDPracownik o 1
                    insertCmd.Parameters.AddWithValue("@Imie", row["Imie"].ToString());
                    insertCmd.Parameters.AddWithValue("@Nazwisko", row["Nazwisko"].ToString());
                    insertCmd.Parameters.AddWithValue("@Data_ur", Convert.ToDateTime(row["Data_ur"]));
                    insertCmd.Parameters.AddWithValue("@PESEL", row["PESEL"].ToString());
                    insertCmd.Parameters.AddWithValue("@Numer_tel", row["Numer_tel"].ToString());
                    insertCmd.Parameters.AddWithValue("@Login_prac", row["Login_prac"].ToString());
                    insertCmd.Parameters.AddWithValue("@Password_prac", row["Password_prac"].ToString());

                    //insertCmd.ExecuteNonQuery();

                    //maxID++; // Zwiększenie wartości maxID o 1
                    //RefreshData();
                    try
                    {
                        insertCmd.ExecuteNonQuery();
                        maxID++; // Zwiększenie wartości maxID o 1
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Wystąpił błąd przy dodawaniu pracownika:\n" + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        RefreshData();
                    }

                }
            }
        }

        private void DeleteData(object sender, RoutedEventArgs e)
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                foreach (DataRowView rowView in dataGrid.SelectedItems)
                {
                    DataRow row = rowView.Row;
                    int idPracownik = Convert.ToInt32(row["IDPracownik"]);

                    string query = "DELETE FROM tblPracownicy WHERE IDPracownik = @IDPracownik";
                    SqlCommand deleteCmd = new SqlCommand(query, con);
                    deleteCmd.Parameters.AddWithValue("@IDPracownik", idPracownik);
                    deleteCmd.ExecuteNonQuery();
                }

            }
            RefreshData();
        }
    }


    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();
        }


       
    }
}
