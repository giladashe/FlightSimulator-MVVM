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
    /// Interaction logic for SliderAileron.xaml
    /// </summary>
    public partial class ThrottleSlider : UserControl
    {
        public ThrottleSlider()
        {
            InitializeComponent();
        }

        private void MySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int val = Convert.ToInt32(e.NewValue);
            string msg = String.Format("Throttle: {0}", val);
            this.TheValue.Text = msg;
            ThrottleValue = ThrottleSlider1.Value;

           

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
