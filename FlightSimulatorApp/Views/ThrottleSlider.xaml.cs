

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ThrottleSlider.xaml
    /// </summary>
    public partial class ThrottleSlider : UserControl
    {
        public ThrottleSlider()
        {
            InitializeComponent();
        }

        private void MySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double val = Convert.ToDouble(e.NewValue);
            string msg = String.Format("Throttle: {0}", Math.Round(val, 2));
            this.TheValue.Text = msg;
        }

        public double ThrottleValue
        {
            get { return (double)GetValue(ThrottleValueProperty); }
            set
            {
                SetValue(ThrottleValueProperty, value);
            }
        }

        public static readonly DependencyProperty ThrottleValueProperty =
            DependencyProperty.Register("ThrottleValue", typeof(double), typeof(ThrottleSlider));
    }
}
