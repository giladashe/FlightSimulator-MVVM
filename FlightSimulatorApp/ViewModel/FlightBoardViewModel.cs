using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    public class FlightBoardViewModel : BaseNotify
    {

        public double Lon
        {
            get;
        }

        public double Lat
        {
            get;
        }
    }
}
