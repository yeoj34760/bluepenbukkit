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

namespace bluepenbukkit.CreateControl.Jar
{
    /// <summary>
    /// NewControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewControl : UserControl
    {

        public NewControl()
        {
            InitializeComponent();
        }
        public delegate void NewFileEventHandler(string ServerFileName); //대리자 생성
        public event NewFileEventHandler NewFileEvent;//이벤트 생성
        string ServerFileName_, ServerFilePath;
        private void Button_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); //드래그 앤 드롭 파일 위치를 불러옴
            string fileName = System.IO.Path.GetFileName(files[0]); //파일 명을 저장
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(init.ProPath + "\\UserData\\Jar\\");
            foreach (var item in di.GetFiles())
            {
                if (fileName == item.Name)
                {
                    MessageBox.Show("파일 명이 중복합니다!");
                    return;
                }
            }

            FileNameLabel.Content = fileName; //FileNameLabel에 파일 명을 적습니다.
            ServerFileName_ = fileName;
            ServerFilePath = files[0]; //경로를 ServerFilePath에 저장
        }

        private void Button_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy; //드래그를 반응함?
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ServerFileName_ != null)
            {
                File.Copy(ServerFilePath, init.ProPath + "\\UserData\\Jar\\" + ServerFileName_); //불러운 파일을 복사합니다.
                NewFileEvent(ServerFileName_); //다음
            }
            else
            {
                MessageBox.Show("파일을 불러오세요.");
            }
        }
    }
}
