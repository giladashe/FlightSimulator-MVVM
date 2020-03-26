using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.Interface;
using System;
using System.ComponentModel;
using System.Windows;

namespace FlightSimulatorApp.ViewModel
{
	public class WheelsViewModel
	{
		private ISimulatorModel _simulatorModel;

		public WheelsViewModel (ISimulatorModel simulatorModel)
		{
			this._simulatorModel = simulatorModel;
		}

		public void setAileron(double value)
		{
			_simulatorModel.setAileron(value);
		}
		public void setThrottle(double value)
		{
			_simulatorModel.setThrottle(value);
		}
		public void setJoystickValues (Point joystickValues)
		{
			_simulatorModel.setJoystickValues(joystickValues);
		}
	}
}

