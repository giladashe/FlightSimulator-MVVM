using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    public class ErrorViewModel : INotifyPropertyChanged
    {
        private readonly ISimulatorModel _simulatorModel;

        public ErrorViewModel (ISimulatorModel simulatorModel)
        {
            this._simulatorModel = simulatorModel;
            _simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public string VM_Error
        {
            get
            {
                return _simulatorModel.Error;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
