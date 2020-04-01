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
        /*private bool isConnected;*/
        private ConnectWindowViewModel _connectViewModel;
        public MainWindow()
        {
            InitializeComponent();
           /* isConnected = false;*/
            _model = new MySimulatorModel();
            /* model.connect("127.0.0.1", 5402);
             model.start();*/
            // VM

            DashBoardViewModel DashVM = new DashBoardViewModel(_model);
            MapViewModel mapViewModel = new MapViewModel(_model);
            WheelsViewModel wheelsViewModel = new WheelsViewModel(_model);
            _connectViewModel = new ConnectWindowViewModel(_model);
            myDashBoard.DataContext = DashVM;
            MyControls.DataContext = wheelsViewModel;
            myMap.DataContext = mapViewModel;
            disconnectButton.IsEnabled = false;
            MyControls.IsEnabled = false;
            myMap.IsEnabled = false;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            /*   if (!isConnected)
               {
                   ConnectWindow cW = new ConnectWindow(_model);
                   cW.Show();
                   if (cW.IsConnected)
                   {
                       isConnected = true;
                       cW.Close();
                   }
               }
               else
               {
                   Warning.Content = "Disconnect first!!";
               }*/

            ConnectWindow cW = new ConnectWindow(_model);
            cW.DataContext = _connectViewModel;
            cW.Show();
            connectButton.IsEnabled = false;
            disconnectButton.IsEnabled = true;
            MyControls.IsEnabled = true;
            myMap.IsEnabled = true;
            /*Warning.Content = "Disconnect first!!";*/

        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (!connectButton.IsEnabled)
            {*/
            _model.disconnect();
            disconnectButton.IsEnabled = false;
            connectButton.IsEnabled = true;
            MyControls.IsEnabled = false;
            myMap.IsEnabled = false;
            //Warning.Content = "";
            // isConnected = false;
            //}
            /*  else
              {
                  Warning.Content = "Connect first!!";
              }*/
        }
    }
}
