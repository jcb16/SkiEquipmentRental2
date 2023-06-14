using SkiEquipmentRental2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace SkiEquipmentRental2
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        public MainPanel()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            //ResizeMode = ResizeMode.CanResize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new DashboardView();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Employee();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Rent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new SearchAndAddEquipment();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Statistics();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
                // Zamykanie aplikacji
                Application.Current.Shutdown();
            
        }
    }


}
