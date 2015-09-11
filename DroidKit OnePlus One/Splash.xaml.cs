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
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net;
namespace DroidKit_OnePlus_One
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : MetroWindow
    {
        string doclocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"DroidKit");
        public Splash()
        {
            InitializeComponent();
           
        }
        internal void SpashScreen_Shown(object sender, RoutedEventArgs e)
        {
            BackgroundWorker loadapp = new BackgroundWorker();
            loadapp.DoWork += new DoWorkEventHandler(load_splash);
            loadapp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(load_done);
            loadapp.RunWorkerAsync();
        }

        private void load_splash(object sender, DoWorkEventArgs e)
        {
            if (Directory.Exists(doclocation))
            { }
            if (!Directory.Exists(doclocation))
            { Directory.CreateDirectory(doclocation); }
            ProcessStartInfo startup = new ProcessStartInfo();
            startup.CreateNoWindow = true;
            startup.FileName = "adb.exe";
            startup.Arguments = "start-server";
            startup.RedirectStandardError = true;
            startup.RedirectStandardOutput = true;
            startup.UseShellExecute = false;
            var process = Process.Start(startup);
            process.WaitForExit(50000);
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        WebClient update = new WebClient();
                        Stream run;
                        run = client.OpenRead("http://repo.itechy21.com/updatematerial.txt");
                        StreamReader reader = new StreamReader(run);
                        String content = reader.ReadLine();
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                        string version = fvi.FileVersion;
                        Version a = new Version(version);
                        Version b = new Version(content);
                        if (b > a)
                        {
                            Dispatcher.BeginInvoke(new Action(() => status.Text = "Update available!"));
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() => status.Text = "No Update available"));
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Dispatcher.BeginInvoke(new Action (() => status.Text = "No Internet Connection." + "\n "+ex.Message));
                Thread.Sleep(2000);
            }
            
        }
             private void load_done(object sender, RunWorkerCompletedEventArgs e)
        {
               MainWindow m = new MainWindow();
               status.Text = "Finished!";
               Thread.Sleep(1500);
               m.Show();
               this.Close();
        }
    }
}