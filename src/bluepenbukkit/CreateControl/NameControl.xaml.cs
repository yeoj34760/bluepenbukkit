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
            if (ServerNameTextBox.Text == "")
            {
                MessageBox.Show("서버 이름을 적으세요");
                return;
            }
            foreach (var J in init.rss.Properties())
            {
                if (J.Name == ServerNameTextBox.Text)
                {
                    MessageBox.Show("서버 이름이 중복됩니다. \n기존 있던 중복 서버를 삭제하거나 다른 이름으로 적으세요.");
                    return;
                }
            }
            NextEvent(ServerNameTextBox.Text);
        }
    }
}
