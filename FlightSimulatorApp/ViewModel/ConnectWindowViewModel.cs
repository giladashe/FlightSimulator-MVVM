using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class ConnectWindowViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel _model;

        public ConnectWindowViewModel(ISimulatorModel model)
        {
            _model = model;
        }


        public string VM_FlightServerIP
        {
            get { return _model.FlightServerIP; }
            set { _model.FlightServerIP = value; }
        }

        public string VM_FlightInfoPort
        {
            get { return _model.FlightInfoPort; }
            set { _model.FlightInfoPort = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
