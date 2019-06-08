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
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
namespace bluepenbukkit
{
    /// <summary>
    /// CreateWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CreateWindow : Window
    {
        string ServerName;
        public delegate void ShowEventHandler();
        public event ShowEventHandler ShowSendEvent;
        public CreateWindow()
        {
            InitializeComponent();
            CreateControl.NameControl nameControl = new CreateControl.NameControl(); //NameControl 생성합니다.
            WIndowGrid.Children.Add(nameControl); //WindowGrid에 nameControl를 추가합니다.
            nameControl.NextEvent += (string name) =>
            {

                ServerName = name; //서버이름을 ServerName에 저장합니다.
                WIndowGrid.Children.Remove(nameControl); //Grid에 추가되어있던 nameXControl을 제거합니다.
                JarChoice(); //메소드 실행함.
            };
        }
        private void JarChoice()
        {
            CreateControl.JarChoiceControl jarChoiceControl = new CreateControl.JarChoiceControl(); //JarChoiceControl 생성합니다.
            WIndowGrid.Children.Add(jarChoiceControl); //WindowGrid에 jarChoiceControl를 추가합니다.
            jarChoiceControl.JarLoadEvent += (bool JarLoad) => //유저가 선택하게 된다면 이벤트가 발생합니다.
            {

                CreateControl.Jar.ExistingControl existingControl = new CreateControl.Jar.ExistingControl(); //existingControl 생성합니다.
                CreateControl.Jar.NewControl newControl = new CreateControl.Jar.NewControl(); //newControl 생성합니다.

                if (JarLoad == true) //기존 파일을 불러온다고 한다면
                {
                    WIndowGrid.Children.Add(existingControl);
                    existingControl.FileEvent += (string ServerFileName_) => { Finish(ServerFileName_); };

                }
                else // 새 파일을 불러온다고 한다면
                {
                    WIndowGrid.Children.Add(newControl);
                    newControl.NewFileEvent += (string ServerFileName_) => { Finish(ServerFileName_); };
                    //유저가 다음으로 넘어간다면 NewFileEventHandler 메소드가 실행되도록 설정합니다.
                }
            };
        }
        private void Finish(string ServerFileName) //NewControl에서 넘어감
        {
            string Path = init.ProPath + "\\UserData\\Servers\\"; //Servers 폴더 위치 변수를 생성합니다.
            int i = 0; //폴더갯수
            if (Directory.Exists(Path)) //해당 폴더가 있는지 확인
            {
                DirectoryInfo di = new DirectoryInfo(Path); //DirectoryInfo 생성
                foreach (var item in di.GetDirectories()) //폴더 갯수와 변수 i 값이 똑같아집니다.
                    i++;
                DirectoryInfo ServerFolder = new DirectoryInfo(Path + "ServerFolder_" + i);
                ServerFolder.Create(); //폴더 생성함
            }
            else //없을시
            {
                MessageBox.Show(init.ProPath + "\\UserData\\Server 폴더가 없습니다!\n블루펜버킷을 종료합니다.");
                Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill(); //프로그램을 종료
                return;
            }
            init.rss.Add(new JProperty(ServerName,
                new JObject(
                    new JProperty("path", "ServerFolder_" + i),
                    new JProperty("jarPath", ServerFileName)))); //rss에 추가된 서버 정보를 추가합니다.
            System.IO.File.WriteAllText(init.ServerListPath, init.rss.ToString()); //수정된 정보를 저장합니다.
            MessageBox.Show("서버 생성되었습니다!"); //성공했다고 알림
            ShowSendEvent();
            Close();


        }
    }

}
