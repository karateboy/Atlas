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
    /// ConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class ConfigPage : Page
    {
        public ConfigPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.GoBack();
        }

        private void btnSortConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new SortConfigPage());
        }

        private void btnChannelConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new ChannelConfigPage());
        }

        private void btnMtConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new MtConfigPage());
        }

        private void btnMeasureConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new MeasureConfigPage());
        }

        private void btnFlowConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new FlowConfigPage());
        }

        private void btnAlertConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new AlertConfigPage());
        }
    }
}
