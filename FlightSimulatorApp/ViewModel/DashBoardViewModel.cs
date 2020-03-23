using System;
using FlightSimulatorApp.Model.Interface;

namespace FlightSimulatorApp.ViewModel
{
	public class DashBoardViewModel : BaseNotify
	{
		private ISimulatorModel _simulatorModel;

		// dashboard
		private double VM_indicated_heading_deg;
		private double VM_gps_indicated_vertical_speed;
		private double VM_gps_indicated_ground_speed_kt;
		private double VM_airspeed_indicator_indicated_speed_kt;
		private double VM_gps_indicated_altitude_ft;
		private double VM_attitude_indicator_internal_roll_deg;
		private double VM_attitude_indicator_internal_pitch_deg;
		private double VM_altimeter_indicated_altitude_ft;

		public DashBoardViewModel(ISimulatorModel simulatorModel)
		{
			this._simulatorModel = simulatorModel;
			_simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		//Properties
		public double VM_indicated_heading_deg
		{
			get{ return _simulatorModel.indicated_heading_deg; }
		}
		public double VM_gps_indicated_vertical_speed
		{
			get { return _simulatorModel.gps_indicated_vertical_speed; }
		}
		public double VM_gps_indicated_ground_speed_kt
		{
			get { return _simulatorModel.gps_indicated_ground_speed_kt; }
		}
		public double VM_airspeed_indicator_indicated_speed_kt
		{
			get { return _simulatorModel.airspeed_indicator_indicated_speed_kt; }
		}
		public double VM_gps_indicated_altitude_ft
		{
			get { return _simulatorModel.gps_indicated_altitude_ft; }
		}
		public double VM_attitude_indicator_internal_roll_deg
		{
			get { return _simulatorModel.attitude_indicator_internal_roll_deg; }
		}
		public double VM_attitude_indicator_internal_pitch_deg
		{
			get { return _simulatorModel.attitude_indicator_internal_pitch_deg; }
		}
		public double VM_altimeter_indicated_altitude_ft
		{
			get { return _simulatorModel.altimeter_indicated_altitude_ft; }
		}

	}

}

