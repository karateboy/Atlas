﻿using System;
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
    /// Main.xaml 的互動邏輯
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

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
