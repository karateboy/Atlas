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
using System.Diagnostics;

namespace HMI
{
    /// <summary>
    /// Main.xaml 的互動邏輯
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            CurrentTime = DateTime.Now.ToString();
            Debug.WriteLine("CurrentTime=>"+CurrentTime);
        }

        private readonly DateTime dateTime = new DateTime();


        public string CurrentTime
        {
            get { return (string)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(string), typeof(Main), new PropertyMetadata(string.Empty));



        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Logout();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.RemoveBackEntry();
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new ConfigPage());
        }
    }
}
