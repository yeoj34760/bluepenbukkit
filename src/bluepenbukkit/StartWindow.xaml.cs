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
using System.Windows.Shapes;

namespace bluepenbukkit
{
    /// <summary>
    /// startWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        public delegate void ShowEventHandler();
        public event ShowEventHandler ShowSendEvent;
        LogControl logControl = new LogControl();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Bukkit.Exited == false)
            {
                Close();
                ShowSendEvent();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowGird.Children.Clear();
            WindowGird.Children.Add(logControl);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void WindowGird_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            logControl.Width = e.NewSize.Width;
            logControl.Height = e.NewSize.Height;
        }
    }
}
