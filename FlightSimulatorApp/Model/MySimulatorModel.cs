using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
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


		private double indicated_heading_deg;
		private double gps_indicated_vertical_speed;
		private double gps_indicated_ground_speed_kt;
		private double airspeed_indicator_indicated_speed_kt;
		private double gps_indicated_altitude_ft;
		private double attitude_indicator_internal_roll_deg;
		private double attitude_indicator_internal_pitch_deg;
		private double altimeter_indicated_altitude_ft;
		private double lon;
		private double lat;


		// map 
		private Point coordinate;

		/*
		x - lon;
		y - lat;
		*/

		public MySimulatorModel()
		{
			this.telnetClient = null;
			stop = false;
			coordinate = new Point(0, 0);
		}

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

				/*	// TODO to all of the properties 
					telnetClient.write("get x");
					x = Double.Parse(telnetClient.read());

					Thread.Sleep(250);*/
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

		private Queue<string> updateVariables = new Queue<string>();



		public double Rudder
		{
			set
			{
				if (value > 1)
				{
					value = 1;
				}
				else if (value < -1)
				{
					value = -1;
				}

				updateVariables.Enqueue("set address " + value);
			}
		}
		public double Throttle
		{
			set
			{
				if (value > 1)
				{
					value = 1;
				}
				else if (value < 0)
				{
					value = 0;
				}

				updateVariables.Enqueue("set address " + value);
			}
		}
		public double Elevator
		{
			set
			{
				if (value > 1)
				{
					value = 1;
				}
				else if (value < -1)
				{
					value = -1;
				}

				updateVariables.Enqueue("set address " + value);
			}
		}
		public double Aileron
		{
			set
			{
				if (value > 1)
				{
					value = 1;
				}
				else if (value < -1)
				{
					value = -1;
				}

				updateVariables.Enqueue("set address " + value);
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


		public void Check()
		{
			for(int i = 1; i < 100; i++)
			{
				Gps_indicated_ground_speed_kt = i;
				Airspeed_indicator_indicated_speed_kt = i;
				Gps_indicated_altitude_ft = i;
				Attitude_indicator_internal_roll_deg = i;
				Attitude_indicator_internal_pitch_deg = i;
				Altimeter_indicated_altitude_ft = i;
				Indicated_heading_deg = i;
				Gps_indicated_vertical_speed = i;
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

	}
}
