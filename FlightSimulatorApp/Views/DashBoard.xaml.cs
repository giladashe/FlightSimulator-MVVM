using FlightSimulatorApp.ViewModel;
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
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : UserControl
    {
        private double _airSpeed;
        private double _altitude;
        private double _roll;
        private double _pitch;
        private double _altimeter;
        private double _headingDegrees;
        private double _groundSpeed;
        private double _verticalSpeed;
        private DashBoardViewModel _vmDash;
        public DashBoard(DashBoardViewModel vmDash)
        {
            InitializeComponent();
            this._vmDash = vmDash;
        }

        public double AirSpeed { get => this._vmDash.VM_airspeed_indicator_indicated_speed_kt; set => _airSpeed = value; }
        //what here???????????????????????????????????????????????????????? - altitude

        public double Altitude { get => _vmDash.VM_airspeed_indicator_indicated_speed_kt; set => _altitude = value; }
        public double Roll { get => _vmDash.VM_attitude_indicator_internal_roll_deg; set => _roll = value; }
        public double Pitch { get => _vmDash.VM_attitude_indicator_internal_pitch_deg; set => _pitch = value; }
        public double Altimeter { get => _vmDash.VM_altimeter_indicated_altitude_ft; set => _altimeter = value; }
        public double HeadingDegrees { get => _vmDash.VM_indicated_heading_deg; set => _headingDegrees = value; }
        public double GroundSpeed { get => _vmDash.VM_gps_indicated_ground_speed_kt; set => _groundSpeed = value; }
        public double VerticalSpeed { get => _vmDash.VM_gps_indicated_vertical_speed; set => _verticalSpeed = value; }
    }
}
