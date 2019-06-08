using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluepenbukkit
{
    class Bukkit
    {
      public static Process p;
        public static bool Exited = false;
        public static void Start(LogControl sender)
        {
            p = new Process();
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
            p.OutputDataReceived += new DataReceivedEventHandler(sender.OutputData);
            p.ErrorDataReceived += new DataReceivedEventHandler(sender.OutputData);
            p.Start();
            p.BeginOutputReadLine();
            Exited = true;
        }
        private static void ExitedEvent(object sender, EventArgs e)
        {
            p.Dispose();
            Exited = false;
        }
    }
}
