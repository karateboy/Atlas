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
    public enum AccessLevel
    {
        User=1, Operator=2, Administrator=3
    }
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            IsLogin = false;
        }

        public static MainWindow Instance { get; set; }
        public bool IsLogin { get; set; }
        public AccessLevel AccessLevel { get; set; }

        public static void Logout()
        {
            Instance.RemoveBackEntry();
            Instance.IsLogin = false;
            Instance.Navigate(new Login());
        }
    }
}
