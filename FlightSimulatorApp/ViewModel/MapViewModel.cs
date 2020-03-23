using System;

namespace FlightSimulatorApp.ViewModel
{
	public class MapViewModel : BaseNotify
	{
        private ISimulatorModel _simulatorModel;

        private double VM_lon;
        private double VM_lat;

        public MapViewModel (ISimulatorModel simulatorModel)
        {
            this._simulatorModel = simulatorModel;
            _simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public double VM_Lon
        {
            get { return _simulatorModel.lon; }
        }

        public double VM_Lat
        {
            get { return _simulatorModel.lat; }
        }

    }
}
