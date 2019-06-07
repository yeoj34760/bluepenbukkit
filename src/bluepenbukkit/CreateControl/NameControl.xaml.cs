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
    /// NameControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NameControl : UserControl
    {
        public delegate void NextEventHandler(string ServerName);
        public event NextEventHandler NextEvent;
        public NameControl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NextEvent(ServerNameTextBox.Text);
        }
    }
}
