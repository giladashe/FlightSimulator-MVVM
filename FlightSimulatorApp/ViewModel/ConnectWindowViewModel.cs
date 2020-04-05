using FlightSimulatorApp.Model.Interface;
using System.ComponentModel;


namespace FlightSimulatorApp.ViewModel
{
    public class ConnectWindowViewModel : INotifyPropertyChanged
    {
        private readonly ISimulatorModel _model;

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
