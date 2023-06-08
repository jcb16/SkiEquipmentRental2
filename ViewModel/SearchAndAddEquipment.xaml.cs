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
    /// <summary>
    /// Interaction logic for SearchAndAddEquipment.xaml
    /// </summary>
    public partial class SearchAndAddEquipment : UserControl
    {
        public SearchAndAddEquipment()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";

            string query = "SELECT IDSprzet,r.IDRodzaj,r.Rodzaj,Marka,Model,Klasa FROM tblSprzet w JOIN tblRodzaj r on w.IDRodzaj = r.IDRodzaj";



            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);

                    dataGrid3.AutoGenerateColumns = false;
                    //dataGrid.DataContext = dtbl;
                    dataGrid3.ItemsSource = dtbl.DefaultView;
                }
            }
        }
    }
}
