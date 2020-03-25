using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.Interface;
using System;
using System.ComponentModel;

namespace FlightSimulatorApp.ViewModel
{
	public class WheelsViewModel : INotifyPropertyChanged
	{
		private ISimulatorModel _simulatorModel;

		/*private double VM_aileron;
		private double VM_elevator;
		private double VM_rudder;
		private double VM_throttle;*/

		public WheelsViewModel (ISimulatorModel simulatorModel)
		{
			this._simulatorModel = simulatorModel;
			_simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public double VM_aileron
		{
			get { return _simulatorModel.Aileron; }
		}
		public double VM_elevator
		{
			get { return _simulatorModel.Elevator; }
		}
		public double VM_rudder
		{
			get { return _simulatorModel.Rudder; }
		}
		public double VM_throttle
		{
			get { return _simulatorModel.Throttle; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}


	}

}

