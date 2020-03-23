using System;

namespace FlightSimulatorApp.ViewModel
{
	public class WheelsViewModel : BaseNotify
	{
		private ISimulatorModel _simulatorModel;

		private double VM_aileron;
		private double VM_elevator;
		private double VM_rudder;
		private double VM_throttle;

		public MapViewModel(ISimulatorModel simulatorModel)
		{
			this._simulatorModel = simulatorModel;
			_simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public double VM_aileron
		{
			get { return _simulatorModel.aileron; }
		}
		public double VM_elevator
		{
			get { return _simulatorModel.elevator; }
		}
		public double VM_rudder
		{
			get { return _simulatorModel.rudder; }
		}
		public double VM_throttle
		{
			get { return _simulatorModel.throttle; }
		}


	}

}

