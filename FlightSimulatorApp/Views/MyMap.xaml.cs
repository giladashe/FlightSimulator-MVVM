using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for MyMap.xaml
    /// </summary>
    public partial class MyMap : UserControl
    {
        private MapViewModel vm_map;
        private double _lon;
        private double _lat;
       
        public MyMap(MapViewModel map)
        {
            InitializeComponent();
            this.vm_map = map;
            this._lon = 32.002644;
            this._lat = 34.888781;
        }
        public double Lon
        {
            get { return vm_map.VM_Lon; }
        }
        public double Lat
        {
            get { return vm_map.VM_Lat; }
        }

        public Point Location
        {
            get { return new Point(Lon, Lat); }
        }
    }
}
