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
    /// AccountConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class AccountConfigPage : Page
    {
        public AccountConfigPage()
        {
            InitializeComponent();
        }

        private void btnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new Main());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.GoBack();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("成功更新", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
