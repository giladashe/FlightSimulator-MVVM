using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model.Interface
{
    public interface ITelnetClient
    {
        void connect(string ip, int port);
        void disconnect();
        void start();


    }
}
