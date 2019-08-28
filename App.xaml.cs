using Sharpsaver.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Sharpsaver
{
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            if (args.Args.Length == 0 || args.Args[0].ToLower().StartsWith("/s"))
            {
                //Start the screensaver in full-screen mode.
                var screensaverWindow = new ScreensaverView();
                screensaverWindow.Show();
            }
            else if (args.Args[0].ToLower().StartsWith("/p"))
            {
                //Display a preview of the screensaver using the specified window handle.
                IntPtr previewHwnd = new IntPtr(Convert.ToInt32(args.Args[1]));
                var previewWindow = new ScreensaverView(previewHwnd);
                previewWindow.Show();
            }
            else if (args.Args[0].ToLower().StartsWith("/c"))
            {
                //Show the configuration settings dialog box.
                var settingsWindow = new SettingsView();
                settingsWindow.Show();
            }
        }
    }
}
