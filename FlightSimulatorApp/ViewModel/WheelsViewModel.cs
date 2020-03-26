﻿using FlightSimulatorApp.Model;
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
        public double VM_aileron
        {
            set { _simulatorModel.Aileron = value; }
        }
        public double VM_elevator
        {
            set { _simulatorModel.Elevator = value; }
        }
        public double VM_rudder
        {
            set { _simulatorModel.Rudder = value; }
        }
        public double VM_throttle
        {
            set { _simulatorModel.Throttle = value; }
        }
    }
}
