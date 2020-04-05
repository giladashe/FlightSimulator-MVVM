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
        private ISimulatorModel _model;
        private ConnectWindowViewModel _connectViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _model = new MySimulatorModel();

            // VM.

            DashBoardViewModel DashVM = new DashBoardViewModel(_model);
            MapViewModel mapViewModel = new MapViewModel(_model);
            ControlsViewModel controlsViewModel = new ControlsViewModel(_model);
            _connectViewModel = new ConnectWindowViewModel(_model);
            ErrorViewModel errorViewModel = new ErrorViewModel(_model);
            Warning.DataContext = errorViewModel;
            myDashBoard.DataContext = DashVM;
            MyControls.DataContext = controlsViewModel;
            myMap.DataContext = mapViewModel;
            longitudeText.DataContext = mapViewModel;
            latitudeText.DataContext = mapViewModel;
            //coordinatesText.DataContext = mapViewModel;
            disconnectButton.IsEnabled = false;
            MyControls.IsEnabled = false;
            myMap.IsEnabled = false;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindow cW = new ConnectWindow(_model);
            cW.DataContext = _connectViewModel;
            cW.Show();
            connectButton.IsEnabled = false;
            disconnectButton.IsEnabled = true;
            MyControls.IsEnabled = true;
            myMap.IsEnabled = true;
        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            _model.disconnect();
            disconnectButton.IsEnabled = false;
            connectButton.IsEnabled = true;
            MyControls.IsEnabled = false;
            myMap.IsEnabled = false;

        }
    }
}
