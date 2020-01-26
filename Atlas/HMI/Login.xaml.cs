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
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class Login : Page
    {
        private MainWindow mMainWindow;
        public Login(MainWindow mMainWindow)
        {
            this.mMainWindow = mMainWindow;
            InitializeComponent();            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.mMainWindow.RemoveBackEntry();
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
            if(UserName == "user" && password.Password == "user")
            {
                this.mMainWindow.IsLogin = true;
                this.mMainWindow.AccessLevel = AccessLevel.User;
                this.mMainWindow.Navigate(new About());
            }else if (UserName == "op" && password.Password == "op")
            {
                this.mMainWindow.IsLogin = true;
                this.mMainWindow.AccessLevel = AccessLevel.Operator;
                this.mMainWindow.Navigate(new About());
            }
            else if (UserName == "admin" && password.Password == "admin")
            {
                this.mMainWindow.IsLogin = true;
                this.mMainWindow.AccessLevel = AccessLevel.Administrator;
                this.mMainWindow.Navigate(new About());
            }
            else
            {
                MessageBox.Show("錯誤的帳號或密碼!");
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {

        }
    }
}
