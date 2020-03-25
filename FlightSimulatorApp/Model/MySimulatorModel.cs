using FlightSimulatorApp.Model.Interface;
using System;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp.Model
{
	public class MySimulatorModel : ISimulatorModel 
	{
		ITelnetClient telnetClient;
		volatile Boolean stop;

		// wheels
		private double aileron;
		private double elevator;
		private double rudder;
		private double throttle;

		// dashboard
		private double indicated_heading_deg;
		private double gps_indicated_vertical_speed;
		private double gps_indicated_ground_speed_kt;
		private double airspeed_indicator_indicated_speed_kt;
		private double gps_indicated_altitude_ft;
		private double attitude_indicator_internal_roll_deg;
		private double attitude_indicator_internal_pitch_deg;
		private double altimeter_indicated_altitude_ft;

		// map 
		private double lon;
		private double lat;


		public MySimulatorModel(ITelnetClient telnetClient)
		{
			this.telnetClient = telnetClient;
			stop = false;
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

		public double Aileron
		{
			set
			{
				this.aileron = value;
				NotifyPropertyChanged("aileron");
			}
			get
			{
				return this.aileron;
			}
		}
		public double Elevator
		{
			set
			{
				this.elevator = value;
				NotifyPropertyChanged("elevator");
			}
			get
			{
				return this.elevator;
			}
		}
		public double Rudder
		{
			set
			{
				this.rudder = value;
				NotifyPropertyChanged("rudder");
			}
			get
			{
				return this.rudder;
			}
		}
		public double Throttle
		{
			set
			{
				this.throttle = value;
				NotifyPropertyChanged("throttle");
			}
			get
			{
				return this.throttle;
			}
		}



		public double Indicated_heading_deg
		{
			set
			{
				this.indicated_heading_deg = value;
				NotifyPropertyChanged("indicated_heading_deg");
			}
			get
			{
				return this.indicated_heading_deg;
			}
		}
		public double Gps_indicated_vertical_speed
		{
			set
			{
				this.gps_indicated_vertical_speed = value;
				NotifyPropertyChanged("gps_indicated_vertical_speed");
			}
			get
			{
				return this.gps_indicated_vertical_speed;
			}
		}
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

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}


	}

}
