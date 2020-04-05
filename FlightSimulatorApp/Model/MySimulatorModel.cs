using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace FlightSimulatorApp.Model
{
    public class MySimulatorModel : ISimulatorModel
    {
        private TcpClient tcpClient;
        volatile Boolean stop;
        private Queue<string> updateVariablesQueue = new Queue<string>();
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
                connect(ip, Convert.ToInt32(port));
                start();
            }
        }



        // Map.

        private double longitude;
        private double latitude;
        private string coordinates;

        //Error string.
        private string error;

        public MySimulatorModel()
        {
            this.tcpClient = null;
            this.stop = false;
            this.ip = ConfigurationManager.AppSettings["ip"];
            this.port = ConfigurationManager.AppSettings["port"];
            /*this.latitude = 32.0055;
            this.longitude = 34.8854;
            Coordinates = Convert.ToString(latitude + "," + longitude);*/
        }

        public void connect(string ip, int port)
        {
            try
            {
                stop = false;
                this.tcpClient = new TcpClient();
                tcpClient.Connect(ip, port);
                this.stream = this.tcpClient.GetStream();
            }
            catch
            {
                this.Error = "Connection Error";
            }
        }
        public void disconnect()
        {
            stop = true;
            if (this.stream != null)
            {
                this.stream.Close();

            }
            this.tcpClient.Dispose();
            this.tcpClient.Close();
        }
        public void writeToServer(String message)
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

        public String readFromServer()
        {
            if (this.stream == null)
            {
                this.Error = "First Server!!";
                return null;
            }
            Byte[] data = new Byte[256];
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            return responseData;
        }
        public void start()
        {
            // Reading.

            new Thread(delegate ()
            {
                while (!stop)
                {
                    try
                    {
                        String accepted = "";

                        //  Get dashboard values.
                        writeToServer("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "indicated_heading_deg");
                        writeToServer("get /instrumentation/gps/indicated-vertical-speed\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "gps_indicated_vertical_speed");
                        writeToServer("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "gps_indicated_ground_speed_kt");
                        writeToServer("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "airspeed_indicator_indicated_speed_kt");
                        writeToServer("get /instrumentation/gps/indicated-altitude-ft\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "gps_indicated_altitude_ft");
                        writeToServer("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "attitude_indicator_internal_roll_deg");
                        writeToServer("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "attitude_indicator_internal_pitch_deg");
                        writeToServer("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "altimeter_indicated_altitude_ft");

                        //  Get map values
                        writeToServer("get /position/longitude-deg\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "longitude");
                        writeToServer("get /position/latitude-deg\n");
                        accepted = readFromServer();
                        handleMessage(accepted, "latitude");

                        while (this.getQueueVariables().Count > 0)
                        {
                            message = this.getQueueVariables().Dequeue();
                            writeToServer(message);
                            message = readFromServer();
                            message = "";
                        }
                        Thread.Sleep(250);
                    }
                    catch
                    {
                        Console.WriteLine("problem with connecting to the server");
                        if (this.stream != null)
                        {
                            this.stream.Flush();
                        }
                        continue;
                    }
                }
            }).Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        private void handleMessage(String accepted, String property)
        {
            if (accepted == "ERR\n")
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


        public Queue<string> getQueueVariables()
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
                this.latitude = value;
                NotifyPropertyChanged("Latitude");
            }
            get
            {
                return this.latitude;
            }
        }

    }
}
