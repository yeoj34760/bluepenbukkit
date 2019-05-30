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
using Newtonsoft.Json.Linq;
namespace bluepenbukkit
{
    /// <summary>
    /// ServerListButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ServerListButton : UserControl
    {
        string ServerName;
        public ServerListButton(string ServerName_)
        {
            InitializeComponent();
            ServerName = ServerName_;
            ServerNameLabel.Content = ServerName_;
            CheckRectangle.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.CheckRectangleHidden();
            init.C_JObject = init.rss[ServerName] as JObject;
            init.C_Name = ServerName;
            CheckRectangle.Visibility = Visibility.Visible;
        }
    }
}
