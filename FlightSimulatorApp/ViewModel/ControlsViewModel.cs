using FlightSimulatorApp.Model.Interface;


namespace FlightSimulatorApp.ViewModel
{
	public class ControlsViewModel
	{
		private readonly ISimulatorModel _simulatorModel;

		public ControlsViewModel (ISimulatorModel simulatorModel)
		{
			_simulatorModel = simulatorModel;
		}
        public double VM_aileron
        {
            set { _simulatorModel.Aileron = value; }
        }
        public double VM_Elevator
        {
            set { _simulatorModel.Elevator = value; }
        }
        public double VM_Rudder
        {
            set { _simulatorModel.Rudder = value; }
        } 
        public double VM_Throttle
        {
            set { _simulatorModel.Throttle = value; }
        }
    }
}

