using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(init.ProPath + "\\UserData\\Servers\\" + init.rss[J.Name]["path"] + "\\plugins\\");
                int Plugins = 0;
                if (di.Exists == true)
                    Plugins = di.GetFiles().Length;
                ServerListButton serverListButton = new ServerListButton(J.Name, Plugins);
                serverListButton.Margin = new Thickness(5); //버튼 간격을 설정합니다.
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
            if (Max == false) { WindowState = WindowState.Maximized; Max = true; }
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

            if (init.C_Name == null) //유저가 서버를 선택안할시
            {
                MessageBox.Show("Server를 선택하세요.");
                return;
            }
            if (MessageBox.Show("정말로 삭제하시겠습니까?\n삭제되면 영구적으로 복구할 수가 없습니다.", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                //정말로 삭제할 것인지 물음
                return;//안하면 리턴
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(init.ProPath + "\\UserData\\Servers\\" + init.C_JObject["path"]);
            di.Delete(true); //폴더삭제 예시 : ServerFolder_0
            init.rss.Property(init.C_Name).Remove(); //서버리스트 파일 목록을 삭제함
            File.WriteAllText(init.ServerListPath, init.rss.ToString()); //변경된 서버리스트 파일를 저장함.
            ServerListLoad();//서버리스트 버튼을 새로고침
            init.C_Name = null; //서버 이름을 null로 처리
            init.C_JObject = null; //서버 정보를 null로 처리


        }

        private void ServerCreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow(); 
            createWindow.Owner = this; //부모폼 설정합니다.
            createWindow.Width = this.Width * 0.7; //부모폼 크기의 0.7로 설정합니다.
            createWindow.Height = this.Height * 0.7;
            createWindow.ShowSendEvent += () =>
            {
                ServerListLoad();
            }; // 서버생성되거나 취소하면 서버리스트 버튼을 다시 새로고침
            createWindow.ShowDialog();
        }

        private void ServerStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (init.C_Name == null) //서버 선택안할시
            {
                MessageBox.Show("Server를 선택하세요.");
                return;
            }
            StartWindow startWindow = new StartWindow();
            startWindow.Owner = this;
            startWindow.ShowSendEvent += new StartWindow.ShowEventHandler(WIndowShow);
            Hide();
            startWindow.Show();

        }

        private void WIndowShow()
        {
            Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ServerFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(init.ProPath + "\\UserData\\Servers\\" + init.C_JObject["path"].ToString()); //ServerFolder_? 폴더를 엽니다.
        }
    }
}
