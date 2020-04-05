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


		// The IP Of the Flight Server.
		string FlightServerIP { get; set; }

		// The Port of the Flight Server
		string FlightInfoPort { get; set; }            

		// Dashboard.

		double Indicated_heading_deg { set; get; }
		double Gps_indicated_vertical_speed { set; get; }
		double Gps_indicated_ground_speed_kt { set; get; }
		double Airspeed_indicator_indicated_speed_kt  { set; get; }
		double Gps_indicated_altitude_ft { set; get; }
		double Attitude_indicator_internal_roll_deg { set; get; }
		double Attitude_indicator_internal_pitch_deg { set; get; }
		double Altimeter_indicated_altitude_ft { set; get; }


		// Map. 
		double Longitude { get; }
		double Latitude { get; }
		string Coordinates { get; }


		// Set controls. 

		double Aileron { set; }
		double Elevator { set; }
		double Rudder { set; }
		double Throttle { set; }

		// Error. 
		string Error { set; get; }
	}

}

