using FlightSimulatorApp.Model.Interface;
using System;
using System.ComponentModel;
using System.Windows;

namespace FlightSimulatorApp.ViewModel
{
	public class MapViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel _simulatorModel;

        public MapViewModel (ISimulatorModel simulatorModel)
        {
            this._simulatorModel = simulatorModel;
            _simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public string VM_Coordinates
        {
            get
            {
                return _simulatorModel.Coordinates;
            }
        }

        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
