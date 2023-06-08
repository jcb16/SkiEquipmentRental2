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

                    dataGrid2.AutoGenerateColumns = false;
                    //dataGrid.DataContext = dtbl;
                    dataGrid2.ItemsSource = dtbl.DefaultView;
                }
            }
        }
    }
}
