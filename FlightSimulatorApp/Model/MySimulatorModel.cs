using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace FlightSimulatorApp.Model
{
    public class MySimulatorModel : ISimulatorModel
    {
        private TcpClient tcpClient;
        volatile Boolean stop;
        private readonly Queue<string> updateVariablesQueue = new Queue<string>();
        private String message;
        private NetworkStream stream;

        // Dashboard.

        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft;
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;

        private string place;

        private string port;
        private string ip;

        public string FlightServerIP
        {
            get { return ip; }
            set { ip = value; }
        }

        public string FlightInfoPort
        {
            get { return port; }
            set
            {
                port = value;
                Connect(ip, Convert.ToInt32(port));
                Start();
            }
        }



        // Map.

        private double longitude;
        private double latitude;
        private Point coordinatePoint;
        private string coordinates;

        //Error string.
        private string error;

        public MySimulatorModel()
        {
            this.tcpClient = null;
            this.stop = false;
            this.ip = ConfigurationManager.AppSettings["ip"];
            this.port = ConfigurationManager.AppSettings["port"];
        }

        public void Connect(string ip, int port)
        {
            try
            {
                this.Error = "";
                stop = false;
                this.tcpClient = new TcpClient
                {
                    ReceiveTimeout = 10000
                };
                tcpClient.Connect(ip, port);
                this.stream = this.tcpClient.GetStream();
            }
            catch
            {
                this.Error = "Connection Error";
            }
        }
        public void Disconnect()
        {
            this.Error = "";
            stop = true;
            if (this.stream != null)
            {
                this.stream.Close();
                this.stream = null;            
            }
            if (this.tcpClient != null)
            {
                this.tcpClient.Dispose();
                this.tcpClient.Close();
            }
        }
        public void WriteToServer(String message)
        {
            if (this.stream == null)
            {
                this.Error = "First Server!!";
            }
            if (message != null && this.stream != null)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                this.stream.Write(data, 0, data.Length);
            }
        }

        public string ReadFromServer()
        {
            bool end = false;
            if (this.stream == null)
            {
                this.Error = "First Server!!";
                return null;
            }
            Byte[] data = new Byte[1024];
            StringBuilder response = new StringBuilder();
            while (!end)
            {
                stream.Read(data, 0, data.Length);
                response.Append(Encoding.ASCII.GetString(data, 0, data.Length));
                for (int i = 0; i < 1024; i++)
                {
                    if (data[i] == '\n')
                    {
                        end = true;
                        break;
                    }
                }
            }
            return response.ToString();
        }

        public void Start()
        {
            // Reading.

            new Thread(delegate ()
            {
                while (!stop)
                {
                    try
                    {
                        string accepted = "";

                        //  Get dashboard values.
                        WriteToServer("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "indicated_heading_deg");
                        WriteToServer("get /instrumentation/gps/indicated-vertical-speed\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "gps_indicated_vertical_speed");
                        WriteToServer("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "gps_indicated_ground_speed_kt");
                        WriteToServer("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "airspeed_indicator_indicated_speed_kt");
                        WriteToServer("get /instrumentation/gps/indicated-altitude-ft\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "gps_indicated_altitude_ft");
                        WriteToServer("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "attitude_indicator_internal_roll_deg");
                        WriteToServer("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "attitude_indicator_internal_pitch_deg");
                        WriteToServer("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "altimeter_indicated_altitude_ft");

                        //  Get map values
                        WriteToServer("get /position/longitude-deg\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "longitude");
                        WriteToServer("get /position/latitude-deg\n");
                        accepted = ReadFromServer();
                        HandleMessage(accepted, "latitude");

                        while (this.GetQueueVariables().Count > 0)
                        {
                            message = this.GetQueueVariables().Dequeue();
                            WriteToServer(message);
                            message = ReadFromServer();
                            message = "";
                        }
                        Thread.Sleep(250);
                    }
                    catch (IOException e)
                    {
                        if (e.Message.Contains("time"))
                        {
                            this.Error = "Timeout passed,\n disconnect please.";
                        }
                        else if (e.Message.Contains("forcibly closed"))
                        {
                            this.Error = "Server is down,\n disconnect please.";
                        }
                        else
                        {   
                            if (this.stream != null)
                            {
                                this.Error = "Read/Write Err.";
                            }
                        }
                        if (this.stream != null)
                        {
                            this.stream.Flush();
                        }
                    }
                }
            }).Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        private void HandleMessage(string accepted, string property)
        {
            if (accepted == null)
            {
                stop = true;
                return;
            }
            if (Double.TryParse(accepted, out double acceptedValue))
            {
                if (acceptedValue > Double.MaxValue || acceptedValue < Double.MinValue)
                {
                    this.Error = "Invalid value";
                }
                else
                {
                    if (property == "indicated_heading_deg")
                    {
                        Indicated_heading_deg = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "gps_indicated_vertical_speed")
                    {
                        Gps_indicated_vertical_speed = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "gps_indicated_ground_speed_kt")
                    {
                        Gps_indicated_ground_speed_kt = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "airspeed_indicator_indicated_speed_kt")
                    {
                        Airspeed_indicator_indicated_speed_kt = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "gps_indicated_altitude_ft")
                    {
                        Gps_indicated_altitude_ft = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "attitude_indicator_internal_roll_deg")
                    {
                        Attitude_indicator_internal_roll_deg = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "attitude_indicator_internal_pitch_deg")
                    {
                        Attitude_indicator_internal_pitch_deg = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "altimeter_indicated_altitude_ft")
                    {
                        Altimeter_indicated_altitude_ft = Math.Round(Double.Parse(accepted), 3);
                    }
                    else if (property == "longitude")
                    {
                        Longitude = Double.Parse(accepted);
                    }
                    else if (property == "latitude")
                    {
                        Latitude = Double.Parse(accepted);

                        Coordinates = Convert.ToString(latitude + "," + longitude);
                    }
                }
            }
            else if (accepted == "ERR")
            {
                if (property == "longitude" || property == "latitude")
                {
                    this.Error = "ERR in MAP";
                }
                else
                {
                    this.Error = "ERR in DashBoard";
                }
            }
            else
            {
                this.Error = " Value that isn't\n a double was sent";
            }
        }

        // Notify and set in the server communication.

        public string Error
        {
            set
            {
                this.error = value;
                NotifyPropertyChanged("Error");
            }
            get
            {
                return this.error;
            }
        }

        public string Place
        {
            set
            {
                if (this.place != value)
                {
                    this.place = value;
                    NotifyPropertyChanged("place");
                }
            }
            get
            {
                return this.place;
            }
        }


        public string Coordinates
        {
            set
            {
                this.coordinates = value;
                NotifyPropertyChanged("Coordinates");
            }
            get
            {
                return this.coordinates;
            }
        }


        public Queue<string> GetQueueVariables()
        {
            return this.updateVariablesQueue;
        }

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

                this.updateVariablesQueue.Enqueue("set /controls/flight/rudder " + value + "\n");
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

                this.updateVariablesQueue.Enqueue("set /controls/engines/current-engine/throttle " + value + "\n");
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

                this.updateVariablesQueue.Enqueue("set /controls/flight/elevator " + value + "\n");
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

                this.updateVariablesQueue.Enqueue("set /controls/flight/aileron " + value + "\n");
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
        public double Longitude
        {
            set
            {
                while (value > 180)
                {
                    value -= 360;
                }
                while (value < -180)
                {
                    value += 360;
                }
                this.longitude = value;
                NotifyPropertyChanged("longitude");
            }
            get
            {
                return this.longitude;
            }
        }
        public double Latitude
        {
            set
            {
                if (value < -90)
                {
                    value = -90;
                }
                else if (value > 90)
                {
                    value = 90;
                }
                this.latitude = value;
                NotifyPropertyChanged("latitude");
                coordinatePoint = new Point(this.Latitude, this.Longitude);
                DeterminePlace(coordinatePoint);
            }
            get
            {
                return this.latitude;
            }
        }

        private void DeterminePlace(Point coordinates)
        {
            double lat = coordinates.X;
            double lon = coordinates.Y;

            if ((lat > 20 && lat < 80) && (lon > -150 && lon <= -90))
            {
                this.Place = "We are in \n North America!";
            }
            else if ((lat > -60 && lat < 10) && (lon > -80 && lon < -35))
            {
                this.Place = "We are in \n South America!";
            }
            else if ((lat > -38 && lat < 30) && (lon > -20 && lon < 35))
            {
                this.Place = "We are in \n Africa!";
            }
            else if ((lat > 0 && lat < 80) && (lon > 30 && lon <= 180))
            {
                this.Place = "We are in \n Asia!";
            }
            else if ((lat > 40 && lat < 70) && (lon > -10 && lon < 30))
            {
                this.Place = "We are in \n Europe!";
            }
            else if ((lat > -50 && lat < -15) && (lon > 120 && lon < 150))
            {
                this.Place = "We are in \n Australia!";
            }
            else if ((lat >= -90 && lat < -70) && (lon >= -180 && lon <= 180))
            {
                this.Place = "We are in \n Antarctica!";
            }
            else
            {
                this.Place = "We are above the Ocean,\n don't fall :)";
            }
        }

    }
}
