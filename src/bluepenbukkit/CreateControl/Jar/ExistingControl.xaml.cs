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

namespace bluepenbukkit.CreateControl.Jar
{
    /// <summary>
    /// ExistingControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ExistingControl : UserControl
    {
        public delegate void FileEventHandler(string JarName); //대리자 생성
        public event FileEventHandler FileEvent;//이벤트 생성
        string JarName;
        List<Button> Buttons = new List<Button>();
        public ExistingControl()
        {
            InitializeComponent();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(init.ProPath + "\\UserData\\Jar\\");
            foreach (var item in di.GetFiles())
            {
                var button = new Button {
                    Content = item.Name,
                    Width = 350,
                    Height = 25,
                    Style = Application.Current.Resources["ButtonStyle1"] as Style,
                    BorderBrush = null,
                    Background = new SolidColorBrush(Color.FromRgb(35,35,35)),
                    Foreground = Brushes.White

            };
                button.Click += new RoutedEventHandler(buttons_Click);
                Buttons.Add(button);
                JarListWrapPanel.Children.Add(button);
            }
        }
        private void buttons_Click(object sender, RoutedEventArgs e)
        {
           Button button = sender as Button;
            JarName = button.Content.ToString();
            foreach (var b in Buttons)
            {
                b.Background = new SolidColorBrush(Color.FromRgb(35, 35, 35));
            }
            button.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (JarName != null)
            {
                FileEvent(JarName);
            }
        }
    }
}
