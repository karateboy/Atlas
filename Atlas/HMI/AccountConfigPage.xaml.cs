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
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HMI
{
    /// <summary>
    /// AccountConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class AccountConfigPage : Page
    {


        public ObservableCollection<Account> Accounts
        {
            get { return (ObservableCollection<Account>)GetValue(AccountsProperty); }
            set { SetValue(AccountsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Accounts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccountsProperty =
            DependencyProperty.Register("Accounts", typeof(ObservableCollection<Account>), typeof(AccountConfigPage), new PropertyMetadata(new ObservableCollection<Account>()));


        public AccountConfigPage()
        {
            InitializeComponent();
            Accounts.Clear();
            foreach (var account in HmiConfig.Instance.AccountList)
            {
                Accounts.Add(account);
            }
            accountGrid.ItemsSource = Accounts;
            accessLevelCombo.ItemsSource = Enum.GetValues(typeof(AccessLevel));
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
            //copy back 
            HmiConfig.Instance.AccountList.Clear();
            foreach (Account account in Accounts)
            {
                HmiConfig.Instance.AccountList.Add(account);
            }
            HmiConfig.Instance.SaveAccount();
            MessageBox.Show("成功更新", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
