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
        User, Operator, Administrator
    }
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            IsLogin = false;
            hmiConfig = new HmiConfig();
            Trace.WriteLine("MainWindows");
        }

        readonly HmiConfig hmiConfig;
        public bool IsLogin { get; set; }
        public AccessLevel AccessLevel { get; set; }
        private void NavigationWindow_Activated(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                this.Navigate(new Login(this));
            }
        }
    }
}
