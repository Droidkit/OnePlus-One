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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace DroidKit_OnePlus_One
{
    public partial class flash_stock : MetroWindow
    {
        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"DroidKit\stock");
        public flash_stock()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path + @"\stock.zip"))
            {
                {
                    if (GB16.IsChecked == false && GB64.IsChecked == false)
                    {
                        MessageBox.Show("Please select either the 16 or 64 GB software appropriate to your phone model.");
                        return;
                    }
                    if (GB16.IsChecked == true)
                    {
                        BackgroundWorker flash64 = new BackgroundWorker();
                        flash64.DoWork += new DoWorkEventHandler(doflash64);
                        flash64.RunWorkerCompleted += new RunWorkerCompletedEventHandler(doneflash64);


                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            flash64.RunWorkerAsync();
                        }
                    }
                    if (GB64.IsChecked == true)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {

                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                Process p = new Process();
                                ProcessStartInfo info = new ProcessStartInfo();
                                info.FileName = "cmd.exe";
                                info.RedirectStandardInput = true;
                                info.UseShellExecute = false;
                                info.RedirectStandardOutput = true;
                                info.RedirectStandardError = true;
                                p.StartInfo = info;
                                p.Start();
                                using (StreamWriter sw = p.StandardInput)
                                {
                                    if (sw.BaseStream.CanWrite)
                                    {
                                        sw.WriteLine("@title Flash");
                                        sw.WriteLine("fastboot flash modem" + path + "NON-HLOS.bin");
                                        sw.WriteLine("fastboot flash sbl1" + path + "sbl1.mbn");
                                        sw.WriteLine("fastboot flash dbi" + path + "sdi.mbn");
                                        sw.WriteLine("fastboot flash aboot" + path + "emmc_appsboot.mbn");
                                        sw.WriteLine("fastboot flash rpm" + path + "rpm.mbn");
                                        sw.WriteLine("fastboot flash tz" + path + "tz.mbn");
                                        sw.WriteLine("fastboot flash LOGO" + path + "logo.bin");
                                        sw.WriteLine("fastboot flash oppostanvbk" + path + "static_nvbk.bin");
                                        sw.WriteLine("fastboot flash recovery" + path + "recovery.img");
                                        sw.WriteLine("fastboot flash system" + path + "system.img");
                                        sw.WriteLine("fastboot flash boot" + path + "boot.img");
                                        sw.WriteLine("fastboot flash cache" + path + "cache.img");
                                        sw.WriteLine("fastboot flash userdata" + path + " userdata_64G.img");
                                        sw.WriteLine("fastboot reboot");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

       
        private void doflash64(object sender, DoWorkEventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            p.StartInfo = info;
            p.Start();
            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("@title Flash");
                    sw.WriteLine("fastboot flash modem" + path + "/NON-HLOS.bin");
                    sw.WriteLine("fastboot flash sbl1" + path + "/sbl1.mbn");
                    sw.WriteLine("fastboot flash dbi" + path + "/sdi.mbn");
                    sw.WriteLine("fastboot flash aboot" + path + "/emmc_appsboot.mbn");
                    sw.WriteLine("fastboot flash rpm" + path + "/rpm.mbn");
                    sw.WriteLine("fastboot flash tz" + path + "/tz.mbn");
                    sw.WriteLine("fastboot flash LOGO" + path + "/logo.bin");
                    sw.WriteLine("fastboot flash oppostanvbk" + path + "/static_nvbk.bin");
                    sw.WriteLine("fastboot flash recovery" + path + "/recovery.img");
                    sw.WriteLine("fastboot flash system" + path + "/system.img");
                    sw.WriteLine("fastboot flash boot" + path + "/boot.img");
                    sw.WriteLine("fastboot flash cache" + path + "/cache.img");
                    sw.WriteLine("fastboot flash userdata" + path + "/userdata.img");
                    sw.WriteLine("fastboot reboot");
                }
            }
            p.WaitForExit(90000000);
        }
        private void doneflash64(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}