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
// To add check for updates! and exception
namespace DroidKit_OnePlus_One
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : MetroWindow
    {

        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "/DroidKit");

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
            if (Directory.Exists(path))
            { }
            else
            { Directory.CreateDirectory(path); }
            ProcessStartInfo startup = new ProcessStartInfo();
            startup.CreateNoWindow = true;
            startup.FileName = "adb.exe";
            startup.Arguments = "start-server";
            startup.RedirectStandardError = true;
            startup.RedirectStandardOutput = true;
            startup.UseShellExecute = false;
            var process = Process.Start(startup);
            process.WaitForExit(50000);

            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://repo.itechy21.com/updatematerial.txt");
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadLine();

            Version a = new Version("0.0.0.1");
            Version b = new Version(content);
            if (b > a)
            {
                Dispatcher.BeginInvoke(new Action(() => status.Content = "Update available!"));
                Thread.Sleep(100);
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() => status.Content = "No Update available"));
                Thread.Sleep(100);
            };
        }
        private void load_done(object sender, RunWorkerCompletedEventArgs e)
        {
               MainWindow m = new MainWindow();
               status.Content = "Finished!";
               m.Show();
               this.Close();
        }
    }
}