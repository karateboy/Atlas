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
using static System.Diagnostics.Trace;

namespace HMI
{
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.RemoveBackEntry();
        }



        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(Login), new PropertyMetadata(string.Empty));



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            try
            {
                var matched = HmiConfig.Instance.AccountList.Find(account => {
                    return account.ID == UserName && account.Password == password.Password;
                });
                MainWindow.Instance.IsLogin = true;
                MainWindow.Instance.AccessLevel = matched.AccessLevel;
                MainWindow.Instance.Navigate(new Main());
            }
            catch (Exception)
            {
                MessageBox.Show("錯誤的帳號或密碼!");
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            UserName = string.Empty;
            password.Password = string.Empty;
            
        }
    }
}
