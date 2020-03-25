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
        
        private Point _location;
       
        public MyMap()
        {
            InitializeComponent();
            this._location = new Point(32.002644, 34.888781);
        }
      
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
