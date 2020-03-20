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

namespace FlightSimulatorApp.views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private Point p1 = new Point();
        public Joystick()
        {
            InitializeComponent();
        }
        private void CenterKnob_Completed(object sender,EventArgs e)
        {
            
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                p1 = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x2 = e.GetPosition(sender as Joystick).X;
                double y2 = e.GetPosition(sender as Joystick).Y;
                Point p2 = new Point(x2, y2);
                double length = Length(p1, p2);
                if (length < Base.Width / 2)
                {
                    knobPosition.X = p2.X - p1.X;
                    knobPosition.Y = p2.Y - p1.Y;
                }

            }
        }

        //calculates length between 2 points
        private double Length(Point first, Point second)
        {
            return Math.Sqrt((second.X - first.X) * (second.X - first.X) + (second.Y - first.Y) * (second.Y - first.Y));
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
    }
}
