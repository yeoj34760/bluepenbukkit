using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
namespace bluepenbukkit
{
    /// <summary>
    /// LogControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LogControl : UserControl
    {
        public LogControl()
        {
            InitializeComponent();
        }
        Process p = new Process();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(init.ProPath + "\\UserData\\Jar" + init.C_JObject["jarPath"].ToString());
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "java";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(ExitedEvent);
            p.StartInfo.WorkingDirectory = init.ProPath + "\\UserData\\Servers\\" + init.C_JObject["path"].ToString();
            p.StartInfo.Arguments = "-Xmx1G -Xms1G -jar " + init.ProPath + "\\UserData\\Jar\\" + init.C_JObject["jarPath"].ToString();
            p.OutputDataReceived += new DataReceivedEventHandler(OutputData);
            p.ErrorDataReceived += new DataReceivedEventHandler(OutputData);
            p.Start();
            p.BeginOutputReadLine();
            init.Exited = true;
        }
        private void ExitedEvent(object sender, EventArgs e)
        {
            init.Exited = false;
        }
        private void OutputData(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                if (e.Data != null)
                    PrintTextBox.Text += e.Data + "\n";
                PrintTextBox.ScrollToEnd(); //자동스크롤
            }));
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                p.StandardInput.WriteLine(InputTextBox.Text); //입력
                InputTextBox.Clear();//자동정리
            }
        }
    }
}
