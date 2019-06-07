using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace bluepenbukkit
{
    class init
    {
        public static readonly string ProPath = Environment.CurrentDirectory;
        public static JObject C_JObject;
        public static string C_Name;
        public static string ServerListPath = init.ProPath + "\\UserData\\Example_ServerList.json";
       public static JObject rss = JObject.Parse(System.IO.File.ReadAllText(ServerListPath));
        public static bool Exited = false;
    }
}
