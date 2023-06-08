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
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Data_ur { get; set; }
        //public string Pesel { get; set; }
        //public string Numer_tel { get;set; }
        //public string Login { get; set; }
        //public string Password { get; set; }

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
