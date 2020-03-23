using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model.EventArgs
{
    public class VirtualJoystickEventArgs
    {
        public double Rudder { get; set; }
        public double Elevator { get; set; }
    }
}
