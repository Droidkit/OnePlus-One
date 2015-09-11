﻿using System;
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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net;
using System.ComponentModel;
using System.Management;
using Microsoft.Win32;

namespace DroidKit_OnePlus_One
{
    public partial class MainWindow : MetroWindow
    {
        string doclocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"DroidKit");
        WebClient webclient;
        Stopwatch sw = new Stopwatch();
        ManagementEventWatcher watcheradd = new ManagementEventWatcher();
        WqlEventQuery queryadd = new WqlEventQuery();
        ManagementEventWatcher watcherremove = new ManagementEventWatcher();
        WqlEventQuery queryremove = new WqlEventQuery();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow1_Initialized(object sender, EventArgs e)
        {
            BackgroundWorker detect = new BackgroundWorker();
            detect.DoWork += new DoWorkEventHandler(detect_work);
            detect.RunWorkerAsync();

            queryadd = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcheradd.EventArrived += new EventArrivedEventHandler(watcher_deviceadded);
            watcheradd.Query = queryadd;
            watcheradd.Start();

            queryremove = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherremove.EventArrived += new EventArrivedEventHandler(watcher_deviceremoved);
            watcherremove.Query = queryremove;
            watcherremove.Start();
            if (queryadd.Equals(true))
            {
                watcherremove.Stop();
                watcherremove.Start();
            }
            if (queryremove.Equals(true))
            {
                watcherremove.Stop();
                watcherremove.Start();
            }
            
            }
        public void detect_work(object sender, DoWorkEventArgs e)
        {
            try
            {
                Process process = new System.Diagnostics.Process();
                ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardInput = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "adb.exe";
                startInfo.Arguments = "shell getprop ro.build.version.release";
                process = Process.Start(startInfo);
                process.WaitForExit(500000);

                Process pr = new System.Diagnostics.Process();
                ProcessStartInfo siy = new System.Diagnostics.ProcessStartInfo();
                siy.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                siy.RedirectStandardInput = false;
                siy.CreateNoWindow = true;
                siy.RedirectStandardOutput = true;
                siy.RedirectStandardError = true;
                siy.UseShellExecute = false;
                siy.FileName = "adb.exe";
                siy.Arguments = "shell getprop ro.product.name";
                pr = Process.Start(siy);
                pr.WaitForExit(500000);

                Process pro = new System.Diagnostics.Process();
                ProcessStartInfo PSI = new System.Diagnostics.ProcessStartInfo();
                PSI.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                PSI.RedirectStandardInput = false;
                PSI.CreateNoWindow = true;
                PSI.RedirectStandardOutput = true;
                PSI.RedirectStandardError = true;
                PSI.UseShellExecute = false;
                PSI.FileName = "adb.exe";
                PSI.Arguments = "get-state";
                pro = Process.Start(PSI);
                pro.WaitForExit(500000);
                Mode.Dispatcher.BeginInvoke(new Action(() => Mode.Content = pro.StandardOutput.ReadToEnd()));
                AV.Dispatcher.BeginInvoke(new Action(() => AV.Content = process.StandardOutput.ReadToEnd()));
                Device.Dispatcher.BeginInvoke(new Action(() => Device.Content = pr.StandardOutput.ReadToEnd()));

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void watcher_deviceremoved(object sender, EventArrivedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(()=> AV.Content = ""));
            Dispatcher.BeginInvoke(new Action (()=> Device.Content = ""));
            Dispatcher.BeginInvoke(new Action(() => Mode.Content = "Device Not Connected..."));
        }
        public void watcher_deviceadded(object sender, EventArrivedEventArgs e)
        {
            try
            {
                Process process = new System.Diagnostics.Process();
                ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardInput = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "adb.exe";
                startInfo.Arguments = "shell getprop ro.build.version.release";
                process = Process.Start(startInfo);
                process.WaitForExit(500000);

                Process pr = new System.Diagnostics.Process();
                ProcessStartInfo siy = new System.Diagnostics.ProcessStartInfo();
                siy.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                siy.RedirectStandardInput = false;
                siy.CreateNoWindow = true;
                siy.RedirectStandardOutput = true;
                siy.RedirectStandardError = true;
                siy.UseShellExecute = false;
                siy.FileName = "adb.exe";
                siy.Arguments = "shell getprop ro.product.name";
                pr = Process.Start(siy);
                pr.WaitForExit(500000);

                Process pro = new System.Diagnostics.Process();
                ProcessStartInfo PSI = new System.Diagnostics.ProcessStartInfo();
                PSI.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                PSI.RedirectStandardInput = false;
                PSI.CreateNoWindow = true;
                PSI.RedirectStandardOutput = true;
                PSI.RedirectStandardError = true;
                PSI.UseShellExecute = false;
                PSI.FileName = "adb.exe";
                PSI.Arguments = "get-state";
                pro = Process.Start(PSI);
                pro.WaitForExit(500000);
                Mode.Dispatcher.BeginInvoke(new Action(() => Mode.Content = pro.StandardOutput.ReadToEnd()));
                AV.Dispatcher.BeginInvoke(new Action(() => AV.Content = process.StandardOutput.ReadToEnd()));
                Device.Dispatcher.BeginInvoke(new Action(() => Device.Content = pr.StandardOutput.ReadToEnd()));
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void adb_backup_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog1.Title = "Save Backup";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.InitialDirectory = doclocation;
            saveFileDialog1.ShowDialog();
            saveFileDialog1.Filter = "Android Backup File | *.ab";
            if (saveFileDialog1.CheckFileExists == true && saveFileDialog1.CheckPathExists == true)
            {
                ProcessStartInfo process = new ProcessStartInfo();
                process.CreateNoWindow = false;
                process.FileName = "adb.exe";
                process.Arguments = "backup -apk -all -f \"" + saveFileDialog1.FileName + "\"";
                process.RedirectStandardError = true;
                process.RedirectStandardOutput = true;
                process.UseShellExecute = false;
                var backup = Process.Start(process);
            }
        }

        private void adb_restore_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Restore Backup";
            openFileDialog.Filter = "Android Backup File | *.ab";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.ShowDialog();
            if (openFileDialog.CheckFileExists == true && openFileDialog.CheckPathExists == true)
            {
            ProcessStartInfo process = new ProcessStartInfo();
            process.CreateNoWindow = false;
            process.FileName = "adb.exe";
            process.Arguments = "backup -apk -all -f \"" + openFileDialog.FileName + "\"";
            process.RedirectStandardError = true;
            process.RedirectStandardOutput = true;
            process.UseShellExecute = false;
            var restore = Process.Start(process);
            }
        }

        private void select_apk_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog apkselect = new Microsoft.Win32.OpenFileDialog();
            apkselect.Title = "Select .APK";
            apkselect.CheckFileExists = true;
            apkselect.CheckPathExists = true;
            apkselect.Filter = "Android Package Kit | *.APK";
            apkselect.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            apkselect.ShowDialog();
            if (apkselect.CheckPathExists == true && apkselect.ValidateNames == true)
            {
                ProcessStartInfo process = new ProcessStartInfo();
                process.CreateNoWindow = false;
                process.FileName = "adb.exe";
                process.Arguments = "install "+apkselect.FileName;
                process.RedirectStandardError = true;
                process.RedirectStandardOutput = true;
                process.UseShellExecute = false;
                var restore = Process.Start(process);
            }
        }

        private void efs_backup_Click(object sender, RoutedEventArgs e)
        {
            var backup = Process.Start("efs backup.bat");
            backup.WaitForExit(500000);
            ProcessStartInfo copy = new ProcessStartInfo();
            copy.FileName = "adb.exe";
            copy.Arguments = "adb pull /sdcard/efs" + doclocation;
            copy.UseShellExecute = false;
            copy.RedirectStandardError = true;
            copy.RedirectStandardOutput = true;
            var process = Process.Start(copy);
            save.Visibility = System.Windows.Visibility.Visible;
            save.Content = "Saved to " + doclocation;
        }

        private void efs_restore_Click(object sender, RoutedEventArgs e)
        {
            var efsrestore = Process.Start("efs restore.bat");
        }

        private void root_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(doclocation + "\root.zip"))
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
                        sw.WriteLine("@title Root");
                        sw.WriteLine("adb push " + doclocation + "\root.zip" + "/sdcard/");
                        sw.WriteLine("adb reboot recovery");
                        sw.WriteLine("adb reboot recovery");
                        sw.WriteLine("adb wait-for-device");
                        sw.WriteLine("adb shell twrp install /sdcard/root.zip");
                        sw.WriteLine("adb reboot");
                    }
                }
                p.WaitForExit(500000);
                MessageBox.Show("Your device should now have SuperSu Installed.");
            }
            else
            {
                MessageBox.Show("I cant find the root.zip file." + "/n" + "Please find it for me.");
                OpenFileDialog findrootzip = new OpenFileDialog();
                findrootzip.Title = "Restore Backup";
                findrootzip.Filter = "Android Backup File | *.ab";
                findrootzip.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                findrootzip.ShowDialog();
                if (findrootzip.CheckFileExists == true && findrootzip.CheckPathExists == true)
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
                            sw.WriteLine("@title Root");
                            sw.WriteLine("adb push " + findrootzip.FileName + "\root.zip" + "/sdcard/");
                            sw.WriteLine("adb reboot recovery");
                            sw.WriteLine("adb reboot recovery");
                            sw.WriteLine("adb wait-for-device");
                            sw.WriteLine("adb shell twrp install /sdcard/root.zip");
                            sw.WriteLine("adb reboot");
                        }
                    }

                }
            }
        }

        private void OOS_Dload_Click(object sender, RoutedEventArgs e)
        {
            using (webclient = new WebClient())
            {
                if (File.Exists(doclocation + "/OOS.zip"))
                {
                    MessageBox.Show("You have already downloaded the file. You do not need to download it again!");
                }
                if (!File.Exists(doclocation + "/OOS.zip"))
                {
                    try
                    {
                        using (var client = new WebClient())
                        {
                            using (var stream = client.OpenRead("http://www.google.com"))
                            {
                                sw.Start();
                                Status.Text = "Downloading...";
                                bar.Value = 0;
                                try { webclient.DownloadFileAsync(new Uri("https://s3.amazonaws.com/oxygenos.oneplus.net/ONE_12_A.01_150813.zip"), doclocation + "/OOS.zip"); }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    if (File.Exists(doclocation + "/OOS.zip"))
                                    { File.Delete(doclocation + "/OOS.zip"); }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "/n" + "In other words somthing went wrong... (Check your internet is working!)");
                    }
                    webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progressOOS);
                    webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompleteOOS);
                }
            }
        }

        private void progressOOS(object sender, DownloadProgressChangedEventArgs e)
        {
            labelSpeed.Text = string.Format("{0} mb/s", (e.BytesReceived / 1000000d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            bar.Value = e.ProgressPercentage;
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        private void CompleteOOS(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled == true)
            {
                webclient.CancelAsync();
                if (File.Exists(doclocation+"/OOS.zip"))
                { File.Delete(doclocation+"/OOS.zip"); }
                MessageBox.Show("Download has been cancelled.");
                bar.Value = 0;
                Status.Text = "Cancelled";
                labelDownloaded.Text = "0mb / 0mb";
                labelSpeed.Text = "0mb/s";
            }
            if (File.Exists(doclocation+"/OOS.zip"))
            {
                MessageBox.Show("Download completed!");
                Status.Text = "Download Completed";
            }
                if (e.Cancelled == false && !File.Exists(doclocation+"/OOS.zip"))
                {
                    MessageBox.Show("There was an internal error. Please report this on the forum thread!");
                    bar.Value = 0;
                    Status.Text = "Internal Erorr";
                    labelDownloaded.Text = "0mb / 0mb";
                    labelSpeed.Text = "0mb/s";
                }
            }
        

        private void flash_OOS_Click(object sender, RoutedEventArgs e)
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
                    sw.WriteLine("@title Oxygen OS Flash");
                    sw.WriteLine("adb push " + doclocation+"/OOS.zip " +"/sdcard");
                    sw.WriteLine("adb reboot recovery");
                    sw.WriteLine("adb wait-for-device");
                    sw.WriteLine("adb shell twrp install /sdcard/OOS.zip");
                }
            }
        }

        private void COS_Dload_Click(object sender, RoutedEventArgs e)
        {
            using (webclient = new WebClient())
            {

                if (File.Exists(doclocation + "/stock.zip"))
                    if (File.Exists(doclocation + "/stock/boot.img"))
                    {
                        MessageBox.Show("You have already downloaded the file. You do not need to download it again!");
                    }
                if (!File.Exists(doclocation + "/stock.zip"))
                {
                    try
                    {
                        using (var client = new WebClient())
                        {
                            using (var stream = client.OpenRead("http://www.google.com"))
                            {
                                sw.Start();
                                bar.Value = 0;
                                Status.Text = "Downloading...";
                                try { webclient.DownloadFileAsync(new Uri("http://builds.cyngn.com/factory/bacon/cm-12.0-YNG1TAS2I3-bacon-signed-fastboot.zip"), doclocation + "/stock.zip"); }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    if (File.Exists(doclocation + "/stock.zip"))
                                    { File.Delete(doclocation + "/stock.zip"); }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "/n" + "In other words somthing went wrong... (Check your internet is working!)");
                    }
                    webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progressCOS);
                    webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompleteCOS);
                }

            }
        }
        private async void CompleteCOS(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled == true)
            {
                if (File.Exists(doclocation + "/stock.zip"))
                { File.Delete(doclocation + "/stock.zip"); }
                MessageBox.Show("Download has been cancelled.");
                bar.Value = 0;
                Status.Text = "Cancelled";
                labelDownloaded.Text = "0mb / 0mb";
                labelSpeed.Text = "0mb/s";
            }
            if (File.Exists(doclocation + "/stock.zip"))
            {
                MessageBox.Show("Download completed!");
                Status.Text = "Extracting zip...";
                string zipdoclocation = doclocation+"/stock.zip";
                string extractdoclocation = doclocation+"/stock";
                if (File.Exists(doclocation + "/stock.zip"))
                {
                    {
                        Status.Text = "Unziping...";
                        await Task.Run(() =>  System.IO.Compression.ZipFile.ExtractToDirectory(zipdoclocation, extractdoclocation));
                        MessageBox.Show("Unzip Complete. You can now flash back to stock!");
                    }
                }
            }
            if (e.Cancelled == false && !File.Exists(doclocation+"/stock.zip"))
            {
                MessageBox.Show("There was an internal error. Please report this on the forum thread!");
                bar.Value = 0;
                Status.Text = "Cancelled";
                labelDownloaded.Text = "0mb / 0mb";
                labelSpeed.Text = "0mb/s";
            }
        }
        private void progressCOS(object sender, DownloadProgressChangedEventArgs e)
        {
            labelSpeed.Text = string.Format("{0} mb/s", (e.BytesReceived / 1000000d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            bar.Value = e.ProgressPercentage;
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        private void flash_COS_Click(object sender, RoutedEventArgs e)
        {
            flash_stock win2 = new flash_stock();
            win2.Show();
        }

        private void flash_recovery_Click(object sender, RoutedEventArgs e)
        {
            if (select_recovery.SelectedItem == TWRP.Content)
            {
                ProcessStartInfo process = new ProcessStartInfo();
                process.CreateNoWindow = false;
                process.FileName = "fastboot.exe";
                process.Arguments = "flash recovery "+doclocation+"/twrp-2.8.7.0.img";
                process.RedirectStandardError = true;
                process.RedirectStandardOutput = true;
                process.UseShellExecute = false;
                var flashrecovery = Process.Start(process);
                flashrecovery.WaitForExit(500000);
            }
            if (select_recovery.SelectedItem == Philz.Content)
            {
                ProcessStartInfo process = new ProcessStartInfo();
                process.CreateNoWindow = false;
                process.FileName = "fastboot.exe";
                process.Arguments = "flash recovery "+doclocation+"/philz_touch.img";
                process.RedirectStandardError = true;
                process.RedirectStandardOutput = true;
                process.UseShellExecute = false;
                var flashrecovery = Process.Start(process);
                flashrecovery.WaitForExit(500000);
            }
            if (select_recovery.SelectedItem == Stockre.Content)
            {
                ProcessStartInfo process = new ProcessStartInfo();
                process.CreateNoWindow = false;
                process.FileName = "fastboot.exe";
                process.Arguments = "flash recovery "+doclocation+"/Stock.img";
                process.RedirectStandardError = true;
                process.RedirectStandardOutput = true;
                process.UseShellExecute = false;
                var flashrecovery = Process.Start(process);
                flashrecovery.WaitForExit(500000);
            }
            else
            { warning.Visibility = System.Windows.Visibility.Visible; }
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            watcherremove.Stop();
            watcheradd.Stop();
            ProcessStartInfo process = new ProcessStartInfo();
            process.CreateNoWindow = true;
            process.FileName = "adb.exe";
            process.Arguments = "kill-server";
            process.RedirectStandardError = true;
            process.RedirectStandardOutput = true;
            process.UseShellExecute = false;
            var exit = Process.Start(process);
            exit.WaitForExit(500000);
        }

        private void ul_bl_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo process = new ProcessStartInfo();
            process.CreateNoWindow = true;
            process.FileName = "fastboot.exe";
            process.Arguments = "oem unlock";
            process.RedirectStandardError = true;
            process.RedirectStandardOutput = true;
            process.UseShellExecute = false;
            var unlock = Process.Start(process);
            unlock.WaitForExit(500000);
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {   
            webclient.CancelAsync();         
            labelSpeed.Text = "0 mb/s";
            labelDownloaded.Text = "0 mb";
            Status.Text = "Cancelled";
            bar.Value = -0 ;
            if (File.Exists(doclocation+"/stock.zip"))
            {File.Delete(doclocation+"/stock.zip");}
            if (File.Exists(doclocation+"/OOS.zip"))
            { File.Delete(doclocation+"OOS.zip"); }
            
        }

        private void CFU_Click(object sender, RoutedEventArgs e)
        {
            CFU.Content = "Checking for updates...";
            WebClient client = new WebClient();
            Stream stream = null;
            try { stream = client.OpenRead("http://repo.itechy21.com/updatematerial.txt"); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadLine();

            Version a = new Version("0.0.0.1");
            Version b = new Version(content);
            if (b > a)
            {
                NewV.Content = "New version available";
                Process p = new System.Diagnostics.Process();
                ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
                si.RedirectStandardInput = true;
                si.RedirectStandardError = true;
                si.RedirectStandardOutput = true;
                si.CreateNoWindow = true;
                si.UseShellExecute = false;
                si.FileName = "adb.exe";
                si.Arguments = "kill-server";
                p = Process.Start(si);
                p.WaitForExit(500000);
                MessageBox.Show("Update found. It will now download!");
                try { webclient.DownloadFileAsync(new Uri("http://repo.itechy21.com/toolkit.exe"), doclocation+"/toolkit.exe"); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (File.Exists(doclocation + "/toolkit.exe"))
                    { File.Delete(doclocation + "/toolkit.exe"); }
                }
                webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
                webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(Complete);
            }
            if (a == b)
            {
                MessageBox.Show("No updates avalible...");
                CFU.Content = "Check for updates";
            }
            if (a > b)
            {
                MessageBox.Show("Your version number is different. Are you running a development build?");
            }
        }
        private void Complete(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                File.Delete(doclocation + "/toolkit.msi");
                MessageBox.Show("Download has been cancelled.");
                CFU.Content = "Check for updates";
            }

            if (File.Exists(doclocation + "/toolkit.exe"))
            {
                MessageBox.Show("Download completed! The Program will now exit and update...");
                Process.Start(doclocation + "/toolkit.exe");
            }
            if (!File.Exists(doclocation + "/toolkit.exe") && e.Cancelled == false)
            { MessageBox.Show("There has been an error downloading the update. Please download it from the forum thread..."); }
        }
        private void progress(object sender, DownloadProgressChangedEventArgs e)
        {
            CFU.Content = e.ProgressPercentage.ToString() + "%";
        }


    }
}
