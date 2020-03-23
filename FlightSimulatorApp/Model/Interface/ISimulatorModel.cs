using System;

namespace FlightSimulatorApp.Model.Interface
{
	public interface ISimulatorModel : BaseNotify
	{
		void connect(string ip, int port);
		void disconnect();
		void start();

		// properties - wheels

		double Aileron { set; get; }
		double Elevator { set; get; }
		double Rudder { set; get; }
		double Throttle { set; get; }

		// dashboard

		double Indicated_heading_deg { set; get; }
		double Gps_indicated_vertical_speed { set; get; }
		double Gps_indicated_ground_speed_kt { set; get; }
		double Airspeed_indicator_indicated_speed_kt  { set; get; }
		double Gps_indicated_altitude_ft { set; get; }
		double Attitude_indicator_internal_roll_deg { set; get; }
		double Attitude_indicator_internal_pitch_deg { set; get; }
		double Altimeter_indicated_altitude_ft { set; get; }

		// map 

		double Lon { set; get; }
		double Lat { set; get; }



	}

}

