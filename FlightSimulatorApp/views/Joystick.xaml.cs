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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X_Val", typeof(double), typeof(Joystick), null);


        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y_Val", typeof(double), typeof(Joystick), null);

        public double X_Val
        {
            get { return Convert.ToDouble(GetValue(XProperty)); }
            set { SetValue(XProperty, value); }
        }

        public double Y_Val
        {
            get { return Convert.ToDouble(GetValue(YProperty)); }
            set { SetValue(YProperty, value); }
        }

        public delegate void EmptyJoystickEventHandler(Joystick sender);

        public event EmptyJoystickEventHandler Released;

        public event EmptyJoystickEventHandler Captured;

        private Point _startPos;
        private double canvasWidth, canvasHeight;
        private readonly Storyboard centerKnob;
        private const double maxValX = 170;
        private const double minValX = -170;
        private const double maxValY = 170;
        private const double minValY = -170;



        public Joystick()
        {
            InitializeComponent();

            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            Knob.MouseMove += Knob_MouseMove;

            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPos = e.GetPosition(Base);
            canvasWidth = Base.ActualWidth;
            canvasHeight = Base.ActualHeight;
            Captured?.Invoke(this);
            Knob.CaptureMouse();

            centerKnob.Stop();
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Knob.IsMouseCaptured) return;

            Point newPos = e.GetPosition(Base);

            Point deltaPos = new Point(newPos.X - _startPos.X, newPos.Y - _startPos.Y);

            //If the distance is bigger than the size of the joystic it doesn't move.

            double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y));
            if (distance >= canvasWidth / 2 || distance >= canvasHeight / 2)
                return;

            //Normalize X and y in [-1,1].

            Y_Val = -1 * (2 * ((deltaPos.Y + maxValY) / (maxValY - minValY)) - 1);
            X_Val = 2 * ((deltaPos.X + maxValX) / (maxValX - minValX)) - 1;

            knobPosition.X = deltaPos.X;
            knobPosition.Y = deltaPos.Y;
        }

        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
        }

        private void CenterKnob_Completed(object sender, EventArgs e)
        {
            X_Val = Y_Val = 0;
            Released?.Invoke(this);
        }
    }
}
