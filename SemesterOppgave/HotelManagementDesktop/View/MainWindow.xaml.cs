using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HotelManagementDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        DesktopVM viewModel = new DesktopVM();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void findRoomClick(object sender, RoutedEventArgs e)
        {
            Reservations.IsEnabled = false;
            Reservations.Visibility = Visibility.Hidden;

            RoomPicker.IsEnabled = true;
            RoomPicker.Visibility = Visibility.Visible;
        }

        private void selectRoomClick(object sender, RoutedEventArgs e)
        {
            Reservations.IsEnabled = true;
            Reservations.Visibility = Visibility.Visible;

            RoomPicker.IsEnabled = false;
            RoomPicker.Visibility = Visibility.Hidden;
        }
    }
}
