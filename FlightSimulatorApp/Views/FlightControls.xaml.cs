using System;
using System.Collections.Generic;
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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for FlightControls.xaml
    /// </summary>
    public partial class FlightControls : UserControl
    {
        public FlightControls()
        {
            InitializeComponent();
            RudderValueText.DataContext = MyJoystick;
            ElevatorValueText.DataContext = MyJoystick;

        }

        /*   public static readonly DependencyProperty RudderProperty =
               DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);
           public double Rudder
           {
               get { return MyJoystick.Rudder; }
               set { MyJoystick.Rudder = value; }
           }*/


    }
}
