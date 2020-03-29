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
        /*private ISimulatorModel _model;*/
        /*private bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
        }*/

        public ConnectWindow(ISimulatorModel model)
        {
            InitializeComponent();
           /* _isConnected = false;*/
            /*_model = model;
            IpText.Text = ConfigurationManager.AppSettings["ip"];
            PortText.Text = ConfigurationManager.AppSettings["port"];*/
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {

            /*   _model.connect(IpText.Text, Convert.ToInt32(PortText.Text));
               _model.start();*/
            /*            _isConnected = true;
                        cW.Close();
            */
            IpText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            PortText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.Close();

        }

      /*  private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}
