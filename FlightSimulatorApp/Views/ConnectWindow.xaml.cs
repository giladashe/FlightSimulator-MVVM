using FlightSimulatorApp.Model.Interface;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
       

        public ConnectWindow () 
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            IpText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            PortText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.Close();
        }
    }
}
