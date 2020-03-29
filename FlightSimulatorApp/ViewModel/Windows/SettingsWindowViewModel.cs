using FlightSimulator.Model;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulatorApp.ViewModel.Windows
{
    public class SettingsWindowViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel _model;

        public SettingsWindowViewModel(ISimulatorModel model)
        {
            this._model = model;
        }

        public string FlightServerIP
        {
            get { return _model.FlightServerIP; }
            set
            {
                _model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public string FlightInfoPort
        {
            get { return _model.FlightInfoPort; }
            set
            {
                _model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }


    /*    public void SaveSettings()
        {
            _model.SaveSettings();
        }

        public void ReloadSettings()
        {
            _model.ReloadSettings();
        }

        #region Commands
        #region ClickCommand
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            _model.SaveSettings();
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        private void OnCancel()
        {
            _model.ReloadSettings();
        }
        #endregion
        #endregion*/
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}

