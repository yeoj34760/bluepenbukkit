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

namespace bluepenbukkit.CreateControl
{
    /// <summary>
    /// JarChoiceControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class JarChoiceControl : UserControl
    {
        public delegate void JarLoadEventHandler(bool JarLoad);
        public event JarLoadEventHandler JarLoadEvent;
        public JarChoiceControl()
        {
            InitializeComponent();
        }

        private void ExistingFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(init.ProPath + "\\UserData\\Jar\\");
            int i = 0;
            foreach (var item in di.GetFiles())
                i++;

            if (i == 0)
            {
                MessageBox.Show("파일이 없습니다.");
                return;
            }
            JarLoadEvent(true);
        }

        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            JarLoadEvent(false);
        }
    }
}
