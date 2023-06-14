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
using Microsoft.Windows.Themes;
using System.Windows.Interop;
using Microsoft.VisualBasic;

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

        private void Search_Click(object sender, RoutedEventArgs e)
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

        private void RefreshData()
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

                    dataGrid3.ItemsSource = dtbl.DefaultView;
                }
            }
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string type = Type.Text;
            string brand = Brand.Text;
            string model = Model.Text;
            string classEq = ClassEq.Text;
            int TypeID = 0;

            Action<string> AddNumberToTypeID = (type) =>
            {
                switch (type)
                {
                    case "Narty":
                        TypeID += 1;
                        break;
                    case "Snowboard":
                        TypeID += 2;
                        break;
                    case "Kijki":
                        TypeID += 3;
                        break;
                    case "Kask":
                        TypeID += 4;
                        break;
                    case "Google":
                        TypeID += 5;
                        break;
                    case "Buty narciarskie":
                        TypeID += 6;
                        break;
                    case "Buty snowboardowe":
                        TypeID += 7;
                        break;
                    case "Ochraniacze":
                        TypeID += 8;
                        break;
                    default:
                        break;
                }
            };

            AddNumberToTypeID(type);

            //MessageBox.Show(TypeID.ToString());


            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";


            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                string query = "INSERT INTO tblSprzet (IDRodzaj, Marka, Model, Klasa) VALUES (@type, @brand, @model, @classEq)";

                SqlCommand insertCmd = new SqlCommand(query, con);
                insertCmd.Parameters.AddWithValue("@type", (int)TypeID);
                insertCmd.Parameters.AddWithValue("@brand", brand);
                insertCmd.Parameters.AddWithValue("@model", model);
                insertCmd.Parameters.AddWithValue("@classEq", classEq);

                try
                {
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Sprzęt został dodany.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Wystąpił błąd przy dodawaniu sprzętu:\n" + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                RefreshData();
            }
        }

        private void DeleteData(object sender, RoutedEventArgs e)
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                foreach (DataRowView rowView in dataGrid3.SelectedItems)
                {
                    DataRow row = rowView.Row;
                    int idSprzet = Convert.ToInt32(row["IDSprzet"]);

                    string query = "DELETE FROM tblSprzet WHERE IDSprzet = @IDSprzet";
                    SqlCommand deleteCmd = new SqlCommand(query, con);
                    deleteCmd.Parameters.AddWithValue("@IDSprzet", idSprzet);
                    deleteCmd.ExecuteNonQuery();
                }
            }

            RefreshData();
        }

        private void UpdateData(object sender, RoutedEventArgs e)
        {
            string connstring = "Data Source=DESKTOP-RVQS4VV;Initial Catalog=WypozyczalniaFull;Integrated Security=True";
            string updateQuery = "UPDATE tblSprzet SET IDRodzaj = @IDRodzaj ,Marka = @Marka, Model = @Model, Klasa = @Klasa WHERE IDSprzet = @IDSprzet";


            using (SqlConnection con = new SqlConnection(connstring))
            {
                con.Open();

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                {
                    foreach (DataRowView rowView in dataGrid3.ItemsSource)
                    {
                        DataRow row = rowView.Row;

                        updateCmd.Parameters.Clear();
                        updateCmd.Parameters.AddWithValue("@IDRodzaj", row["IDRodzaj"]);
                        updateCmd.Parameters.AddWithValue("@Marka", row["Marka"].ToString());
                        updateCmd.Parameters.AddWithValue("@Model", row["Model"].ToString());
                        updateCmd.Parameters.AddWithValue("@Klasa", row["Klasa"].ToString());
                        updateCmd.Parameters.AddWithValue("@IDSprzet", row["IDSprzet"]); 

                        updateCmd.ExecuteNonQuery();
                    }
                }
                RefreshData();
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DataGridRow row = (DataGridRow)dataGrid3.ItemContainerGenerator.ContainerFromIndex(dataGrid3.SelectedIndex);
                if (row != null)
                {
                    // Zapisz zmiany w wierszu
                    row.BindingGroup.CommitEdit();

                    // Zaktualizuj dane
                    UpdateData(sender, e);
                }
            }
        }

        private void Brand_TextChanged(object sender, RoutedEventArgs e)
        {
                // Czyszczenie zawartości TextBoxa po kliknięciu
                Brand.Text = string.Empty;
        }

        private void Model_TextChanged(object sender, RoutedEventArgs e)
        {
            // Czyszczenie zawartości TextBoxa po kliknięciu
            Model.Text = string.Empty;
        }

        private void ClassEq_TextChanged(object sender, RoutedEventArgs e)
        {
            // Czyszczenie zawartości TextBoxa po kliknięciu
            ClassEq.Text = string.Empty;
        }
    }
}