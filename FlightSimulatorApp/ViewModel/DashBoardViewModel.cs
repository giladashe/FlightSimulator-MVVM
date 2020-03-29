using System;
using System.ComponentModel;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.Interface;

namespace FlightSimulatorApp.ViewModel
{
	public class DashBoardViewModel : INotifyPropertyChanged
	{
		private ISimulatorModel _simulatorModel;

		// no need - maybe for binding 
		/*private double VM_indicated_heading_deg;
		private double VM_gps_indicated_vertical_speed;
		private double VM_gps_indicated_ground_speed_kt;
		private double VM_airspeed_indicator_indicated_speed_kt;
		private double VM_gps_indicated_altitude_ft;
		private double VM_attitude_indicator_internal_roll_deg;
		private double VM_attitude_indicator_internal_pitch_deg;
		private double VM_altimeter_indicated_altitude_ft;*/

		public DashBoardViewModel(ISimulatorModel simulatorModel)
		{
			this._simulatorModel = simulatorModel;
			_simulatorModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		//Properties
		/*public double[] VM_DashBoardValues
		{
			get { return _simulatorModel.DashBoardValues; }
		}*/

		
		public double VM_Indicated_heading_deg
		{
			get{ return _simulatorModel.Indicated_heading_deg; }
		}
		public double VM_Gps_indicated_vertical_speed
		{
			get { return _simulatorModel.Gps_indicated_vertical_speed; }
		}
		public double VM_Gps_indicated_ground_speed_kt
		{
			get { return _simulatorModel.Gps_indicated_ground_speed_kt; }
		}
		public double VM_Airspeed_indicator_indicated_speed_kt
		{
			get { return _simulatorModel.Airspeed_indicator_indicated_speed_kt; }
		}
		public double VM_Gps_indicated_altitude_ft
		{
			get { return _simulatorModel.Gps_indicated_altitude_ft; }
		}
		public double VM_Attitude_indicator_internal_roll_deg
		{
			get { return _simulatorModel.Attitude_indicator_internal_roll_deg; }
		}
		public double VM_Attitude_indicator_internal_pitch_deg
		{
			get { return _simulatorModel.Attitude_indicator_internal_pitch_deg; }
		}
		public double VM_Altimeter_indicated_altitude_ft
		{
			get { return _simulatorModel.Altimeter_indicated_altitude_ft; }
		}
		

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

	}

}

