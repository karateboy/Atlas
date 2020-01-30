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

namespace HMI
{
    /// <summary>
    /// SysStatusPage.xaml 的互動邏輯
    /// </summary>
    public partial class SysStatusPage : Page
    {
        public SysStatusPage()
        {
            InitializeComponent();
        }

        private void btnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new Main());
        }

        private void btnAlarmQuery_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new EventPage());
        }
    }
}
