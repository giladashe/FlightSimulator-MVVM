using FlightSimulatorApp.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;

namespace FlightSimulatorApp.Model.Interface
{
	public interface ISimulatorModel : INotifyPropertyChanged
	{
		void connect(string ip, int port);
		void disconnect();
		void start();


		// dashboard

		//double [] DashBoardValues { get; }

		
		double Indicated_heading_deg { set; get; }
		double Gps_indicated_vertical_speed { set; get; }
		double Gps_indicated_ground_speed_kt { set; get; }
		double Airspeed_indicator_indicated_speed_kt  { set; get; }
		double Gps_indicated_altitude_ft { set; get; }
		double Attitude_indicator_internal_roll_deg { set; get; }
		double Attitude_indicator_internal_pitch_deg { set; get; }
		double Altimeter_indicated_altitude_ft { set; get; }
		

		// map 
		Point Coordinate { get; }

		
		/*double Lon { set; get; }
		double Lat { set; get; }*/
		

		// set wheels 

		double Aileron { set; }
		double Elevator { set; }
		double Rudder { set; }
		double Throttle { set; }

	}

}

