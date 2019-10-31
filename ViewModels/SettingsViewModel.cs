using Sharpsaver.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace Sharpsaver.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private Settings settings;

        public Param1 Parameter1
        {
            get => settings.param1;
            set
            {
                settings.param1 = value;
                OnPropertyChanged("Parameter1");
            }
                
        }

        public Param2 Parameter2
        {
            get => settings.param2;
            set
            {
                settings.param2 = value;
                OnPropertyChanged("Parameter2");
            }
        }

        public int Parameter3
        {
            get => settings.param3;
            set
            {
                settings.param3 = value;
                OnPropertyChanged("Parameter3");
            }
        }

        public bool Parameter4
        {
            get => settings.param4;
            set
            {
                settings.param4 = value;
                OnPropertyChanged("Parameter4");
            }
        }

        public SettingsViewModel()
        {
            settings = new Settings();
            settings.LoadSettings();
        }

        public ICommand SaveCommand
        {
            get { return new DelegateCommand(new Action<object>(Save)); }
        }

        public void Save(object obj)
        {
            settings.SaveSettings();
            Application.Current.Shutdown();
        }

    }
}
