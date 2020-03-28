using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private static Mutex mutex;
        private NetworkStream stream;

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
        //private Point coordinate;
        private double lon;
        private double lat;
        private string coordinates;

        public MySimulatorModel(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.stop = false;
            mutex = new Mutex();
            //coordinate = new Point(0, 0);
        }

        public void connect(string ip, int port)
        {
            tcpClient.Connect(ip, port);
            this.stream = this.tcpClient.GetStream();
        }
        public void disconnect()
        {
            stop = true;
            this.stream.Close();
            this.tcpClient.Close();
            //tcpClient.Dispose();
        }
        public void writeToServer(String message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            this.stream.Write(data, 0, data.Length);
        }

        public String readFromServer()
        {
            Byte[] data = new Byte[256];
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            return responseData;
        }
        public void start()
        {
            // reading
            new Thread(delegate ()
            {
                while (!stop)
                {
                    mutex.WaitOne();

                    //  get dashboard values
                    writeToServer("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    Indicated_heading_deg = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/gps/indicated-vertical-speed\n");
                    Gps_indicated_vertical_speed = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    Gps_indicated_ground_speed_kt = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    Airspeed_indicator_indicated_speed_kt = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/gps/indicated-altitude-ft\n");
                    Gps_indicated_altitude_ft = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Attitude_indicator_internal_roll_deg = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Attitude_indicator_internal_pitch_deg = Double.Parse(readFromServer());
                    writeToServer("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter_indicated_altitude_ft = Double.Parse(readFromServer());

                    //  get map values
                    writeToServer("get /position/latitude-deg\n");
                    lat = Double.Parse(readFromServer());
                    writeToServer("get /position/longitude-deg\n");
                    lon = Double.Parse(readFromServer());
                    Coordinates = Convert.ToString(lat + "," + lon);
                    mutex.ReleaseMutex();
                    Thread.Sleep(250);
                }
            }).Start();

            // writing
            new Thread(delegate ()
            {
                while (!stop)
                {
                    while (this.getQueueVariables().Count > 0)
                    {
                        message = this.getQueueVariables().Dequeue();
                        mutex.WaitOne();
                        writeToServer(message);
                        message = readFromServer();
                        mutex.ReleaseMutex();
                        message = "";
                        //Thread.Sleep(250);
                    }
                }
            }).Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // notify and set in the server communication

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
