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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True");
            //sda = new SqlDataAdapter(@"SELECT * FROM tblPracownicy",con);
            //dt = new DataTable();
            //sda.Fill(dt);
            //scb = new SqlCommandBuilder(sda);
            //sda.Update(dt);

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
                        //updateCmd.Parameters.Clear();
                        //updateCmd.Parameters.AddWithValue("@Imie", row["Imie"]);
                        //updateCmd.Parameters.AddWithValue("@Nazwisko", row["Nazwisko"]);
                        //updateCmd.Parameters.AddWithValue("@IDPracownik", row["IDPracownik"]);
                        updateCmd.Parameters.Clear();
                        updateCmd.Parameters.AddWithValue("@Imie", row["Imie"].ToString());
                        updateCmd.Parameters.AddWithValue("@Nazwisko", row["Nazwisko"].ToString());
                        updateCmd.Parameters.AddWithValue("@Data_ur", Convert.ToDateTime(row["Data_ur"]));
                        updateCmd.Parameters.AddWithValue("@PESEL", row["PESEL"].ToString());
                        updateCmd.Parameters.AddWithValue("@Numer_tel", row["Numer_tel"].ToString());
                        updateCmd.Parameters.AddWithValue("@Login_prac", row["Login_prac"].ToString());
                        updateCmd.Parameters.AddWithValue("@Password_prac", row["Password_prac"].ToString());
                        updateCmd.Parameters.AddWithValue("@IDPracownik", Convert.ToInt32(row["IDPracownik"]));



                        //updateCmd.Parameters.AddWithValue("@Pesel", row["Pesel"]);
                        //updateCmd.Parameters.AddWithValue("@Numer_tel", row["Numer telefonu"]);
                        //updateCmd.Parameters.AddWithValue("@Login", row["Login"]);
                        //updateCmd.Parameters.AddWithValue("@Password", row["Hasło"]);



                        updateCmd.ExecuteNonQuery();
                    }
                }
            }


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
