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
                OnPropertyChanged();
            }
                
        }

        public Param2 Parameter2
        {
            get => settings.param2;
            set
            {
                settings.param2 = value;
                OnPropertyChanged();
            }
        }

        public int Parameter3
        {
            get => settings.param3;
            set
            {
                settings.param3 = value;
                OnPropertyChanged();
            }
        }

        public bool Parameter4
        {
            get => settings.param4;
            set
            {
                settings.param4 = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            settings = new Settings();
            settings.LoadSettings();
        }

        public ICommand SaveCommand
        {
            get { return new DelegateCommand(Save); }
        }

        public void Save()
        {
            settings.SaveSettings();
            Application.Current.Shutdown();
        }

    }
}
