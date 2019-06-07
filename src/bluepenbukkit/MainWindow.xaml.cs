using System;
using System.Collections.Generic;
using System.IO;
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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<ServerListButton> serverListButtons = new List<ServerListButton>(); //서버 리스트 버튼
        public MainWindow()
        {
            InitializeComponent();
            ServerListLoad();
        }
        private void ServerListLoad()
        {
            ServerListPanel.Children.Clear(); //불러오기전에 서버리스트패널을 정리함.
            foreach (var J in init.rss.Properties()) //서버리스트를 불러옵니다.
            {
                ServerListButton serverListButton = new ServerListButton(J.Name);
                serverListButtons.Add(serverListButton);
                ServerListPanel.Children.Add(serverListButton);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        bool Max = false;
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            if (Max == false) { WindowState = WindowState.Maximized ; Max = true; }
            else
            { WindowState = WindowState.Normal; Max = false; }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        public static void CheckRectangleHidden()
        {
            foreach (ServerListButton s in serverListButtons)
                s.CheckRectangle.Visibility = Visibility.Hidden;
        }

        private void ServerDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (init.C_Name != null)
            {
                init.rss.Property(init.C_Name).Remove();
                File.WriteAllText(init.ProPath + "\\UserData\\Example_ServerList.json", init.rss.ToString());
                ServerListLoad();
                init.C_Name = null;
                init.C_JObject = null;
            }
            else MessageBox.Show("Server를 선택하세요.");
        }

        private void ServerCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow();
            createWindow.Owner = this;
            createWindow.Width = this.Width * 0.7;
            createWindow.Height = this.Height * 0.7;
            createWindow.ShowSendEvent += () =>
            {
                ServerListLoad();
            };
            createWindow.ShowDialog();
        }

        private void ServerStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (init.C_JObject != null)
            {
                StartWindow startWindow = new StartWindow();
                startWindow.ShowSendEvent += new StartWindow.ShowEventHandler(WIndowShow);
                Hide();
                startWindow.Show();
            }

        }

        private void WIndowShow()
        {
            Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
