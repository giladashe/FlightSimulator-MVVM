using FlightSimulatorApp.Model.Interface;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace FlightSimulatorApp.Model
{
	public class MySimulatorModel : ISimulatorModel 
	{
		ITelnetClient telnetClient;  
		volatile Boolean stop;

		// dashboard
		private double[] dashBoardValues = { 0, 0, 0, 0, 0, 0, 0, 0 };

		/*
		dashBoardValues[0] - indicated_heading_deg;
		dashBoardValues[1] - gps_indicated_vertical_speed;
		dashBoardValues[2] - gps_indicated_ground_speed_kt;
		dashBoardValues[3] - airspeed_indicator_indicated_speed_kt;
		dashBoardValues[4] - gps_indicated_altitude_ft;
		dashBoardValues[5] - attitude_indicator_internal_roll_deg;
		dashBoardValues[6] - attitude_indicator_internal_pitch_deg;
		dashBoardValues[7] - altimeter_indicated_altitude_ft;
		*/

		// map 
		private Point coordinate;

		/*
		x - lon;
		y - lat;
		*/

		public MySimulatorModel(ITelnetClient telnetClient)
		{
			this.telnetClient = telnetClient;
			stop = false;
			coordinate = new Point(0, 0);
		}

		public void connect(string ip, int port)
		{
			telnetClient.connect(ip, port);
		}
		public void disconnect()
		{
			stop = true;
			telnetClient.disconnect();
		}
		public void start()
		{
			new Thread(delegate () {
				while (!stop)
				{

					// TODO to all of the properties 
					telnetClient.write("get x");
					x = Double.Parse(telnetClient.read());

					Thread.Sleep(250);
				}
			}).Start();
		}


		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		// notify and set in the server communication

		
		public double[] DashBoardValues
		{
			get
			{
				return this.dashBoardValues;
			}
		}

		public Point Coordinate
		{
			get
			{
				return this.coordinate;
			}
		}

		public void setAileron(double value)
		{

		}
		public void setThrottle(double value)
		{

		}
		public void setJoystickValues(Point joystickValues)
		{

		}

		/*
		public double Gps_indicated_ground_speed_kt
		{
			set
			{
				this.gps_indicated_ground_speed_kt = value;
				NotifyPropertyChanged("gps_indicated_ground_speed_kt");
			}
			get
			{
				return this.gps_indicated_ground_speed_kt;
			}
		}
		public double Airspeed_indicator_indicated_speed_kt
		{
			set
			{
				this.airspeed_indicator_indicated_speed_kt = value;
				NotifyPropertyChanged("airspeed_indicator_indicated_speed_kt");
			}
			get
			{
				return this.airspeed_indicator_indicated_speed_kt;
			}
		}
		public double Gps_indicated_altitude_ft
		{
			set
			{
				this.gps_indicated_altitude_ft = value;
				NotifyPropertyChanged("gps_indicated_altitude_ft");
			}
			get
			{
				return this.gps_indicated_altitude_ft;
			}
		}
		public double Attitude_indicator_internal_roll_deg
		{
			set
			{
				this.attitude_indicator_internal_roll_deg = value;
				NotifyPropertyChanged("attitude_indicator_internal_roll_deg");
			}
			get
			{
				return this.attitude_indicator_internal_roll_deg;
			}
		}
		public double Attitude_indicator_internal_pitch_deg
		{
			set
			{
				this.attitude_indicator_internal_pitch_deg = value;
				NotifyPropertyChanged("attitude_indicator_internal_pitch_deg");
			}
			get
			{
				return this.attitude_indicator_internal_pitch_deg;
			}
		}
		public double Altimeter_indicated_altitude_ft
		{
			set
			{
				this.altimeter_indicated_altitude_ft = value;
				NotifyPropertyChanged("altimeter_indicated_altitude_ft");
			}
			get
			{
				return this.altimeter_indicated_altitude_ft;
			}
		}
		public double Lon
		{
			set
			{
				this.lon = value;
				NotifyPropertyChanged("lon");
			}
			get
			{
				return this.lon;
			}
		}
		public double Lat
		{
			set
			{
				this.lat = value;
				NotifyPropertyChanged("lat");
			}
			get
			{
				return this.lat;
			}
		}
		*/


	}

}
