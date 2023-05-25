using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace SkiEquipmentRental2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //funkcja jest wywoływana również po naciśnięciu enter
        //private void Button_Click_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        Button_Click(sender, e);
        //    }
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = usernameBox.Text;
            string password = passwordBox.Password;

            string connstring = "Data Source = DESKTOP-RVQS4VV; Initial Catalog = WypozyczalniaFull; Integrated Security = true";

            SqlConnection con = new SqlConnection(connstring);
            con.Open();

            //string query = $"Select * from tblPracownicy where login_prac = ${login} and password_prac = ${password}";
            string query = $"Select * from tblPracownicy where login_prac = @login and password_prac = @password";

            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Dane logowania są poprawne
                    // Możesz umożliwić dostęp do aplikacji lub wykonać inne operacje

                    MessageBox.Show("Poprawne dane logowania. Dostęp przyznany.");
                    MainPanel objSearchEquipment = new MainPanel();
                    this.Visibility = Visibility.Hidden;
                    objSearchEquipment.Show();
                }
                else
                {
                    // Dane logowania są nieprawidłowe
                    // Wyświetl odpowiedni komunikat dla użytkownika

                    MessageBox.Show("Nieprawidłowe dane logowania. Spróbuj ponownie.");
                }
            }
            
        }

       
    }
}
