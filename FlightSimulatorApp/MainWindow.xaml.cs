using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.Interface;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object wheelsViewModel;

        public MainWindow()
        {
            InitializeComponent();
           // ITelnetClient telnet = new ();
            MySimulatorModel model = new MySimulatorModel(telnet);

            // VM
           
            DashBoardViewModel DashVM = new DashBoardViewModel(model);
            MapViewModel mapViewModel = new MapViewModel(model);
            WheelsViewModel wheelsViewModel = new WheelsViewModel(model);

            wheelsViewModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                this.wheelsViewModel.setJoystickValues(Point joystickValues);
            }
            else if () // throttleSlider
            {
                this.wheelsViewModel.setThrottle(double value);
            }
            else // aileronSlider
            {
                this.wheelsViewModel.setAileron(double value);
            }
        }
        }*/
        
    }
}
