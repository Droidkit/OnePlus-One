using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DroidKit_OnePlus_One
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var splash = new Splash();

            EventHandler closed = null;
            closed = (sender, args) =>
            {
                splash.Closed -= closed;
                MainWindow = new MainWindow();
                MainWindow.Closed += (o, eventArgs) => Application.Current.Shutdown();
                MainWindow.Show();
            };

            splash.Closed += closed;
            splash.Show();
        }
    }
}
