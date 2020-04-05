using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.Interface;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       /* private ISimulatorModel _model;
        private ConnectWindowViewModel _connectViewModel;*/
        public MainWindow()
        {
            InitializeComponent();
            //_model = new MySimulatorModel();

            // VM.
            Warning.DataContext = (Application.Current as App).ErrorViewModel;
            myDashBoard.DataContext = (Application.Current as App).DashVM;
            MyControls.DataContext = (Application.Current as App).ControlsViewModel;
            MapViewModel mapVm = (Application.Current as App).MapViewModel;
            myMap.DataContext = mapVm;
            longitudeText.DataContext = mapVm;
            latitudeText.DataContext = mapVm;
            placeText.DataContext = mapVm;
            disconnectButton.IsEnabled = false;
            MyControls.IsEnabled = false;
            myMap.IsEnabled = false;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindow cW = new ConnectWindow()
            {
                DataContext = (Application.Current as App).ConnectViewModel
            };
            cW.Show();
            connectButton.IsEnabled = false;
            disconnectButton.IsEnabled = true;
            MyControls.IsEnabled = true;
            myMap.IsEnabled = true;
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).Model.Disconnect();
            disconnectButton.IsEnabled = false;
            connectButton.IsEnabled = true;
            MyControls.IsEnabled = false;
            myMap.IsEnabled = false;
        }
    }
}
