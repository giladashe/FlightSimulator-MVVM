using FlightSimulatorApp.Model.Interface;
using System;
using System.ComponentModel;
using System.Windows;

namespace FlightSimulatorApp.ViewModel
{
	public class MapViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel _simulatorModel;

        /*private double VM_lon;
        private double VM_lat;*/

        public MapViewModel (ISimulatorModel simulatorModel)
        {
            this._simulatorModel = simulatorModel;
            _simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public Point VM_Coordinate
        {
            get
            {
                return _simulatorModel.Coordinate;
            }
        }

        /*
        public double VM_Lon
        {
            get { return _simulatorModel.Lon; }
        }

        public double VM_Lat
        {
            get { return _simulatorModel.Lat; }
        }
        */
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
