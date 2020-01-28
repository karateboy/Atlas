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
namespace HMI
{
    public class AlertThreshold
    {
        public decimal Lo { get; set; }
        public decimal Hi { get; set; }
        public decimal HH { get; set; }
    }
    public class AlertConfig
    {
        public string Name { get; set; }
        public AlertThreshold[] Thresholds { get; set; }
    }
    /// <summary>
    /// AlertConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class AlertConfigPage : Page
    {
        public AlertConfigPage()
        {
            InitializeComponent();
            alertConfig.ItemsSource = AlertConfigs;
            switchIonType();
        }

        static AlertThreshold[] defaultAlertConfig = new AlertThreshold[4]
                {
                    new AlertThreshold
                    {
                        Lo = 0.00M,
                        Hi = 100.0M,
                        HH = 100.0M
                    },
                    new AlertThreshold
                    {
                        Lo = 0.00M,
                        Hi = 100.0M,
                        HH = 100.0M
                    },
                    new AlertThreshold
                    {
                        Lo = 0.00M,
                        Hi = 100.0M,
                        HH = 100.0M
                    },
                    new AlertThreshold
                    {
                        Lo = 0.00M,
                        Hi = 100.0M,
                        HH = 100.0M
                    }
                };

        private ObservableCollection<AlertConfig> minusIonAlertConfig = new ObservableCollection<AlertConfig>
        {
            new AlertConfig
            {
                Name = "F-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "Cl-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "NO2-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "Br-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "NO3-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "SO4-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "PO4-",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            }
        };

        private ObservableCollection<AlertConfig> positiveIonAlertConfig = new ObservableCollection<AlertConfig>
        {
             new AlertConfig
            {
                Name = "Na+",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "NH4+",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "K+",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "Mg2+",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            },
            new AlertConfig
            {
                Name = "Ca+",
                Thresholds = (AlertThreshold[])defaultAlertConfig.Clone()
            }
        };



        public ObservableCollection<AlertConfig> AlertConfigs
        {
            get { return (ObservableCollection<AlertConfig>)GetValue(AlertConfigsProperty); }
            set { SetValue(AlertConfigsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlertConfigs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlertConfigsProperty =
            DependencyProperty.Register("AlertConfigs", typeof(ObservableCollection<AlertConfig>), typeof(AlertConfigPage), new PropertyMetadata(new ObservableCollection<AlertConfig>()));

        public bool IsPositiveIon
        {
            get { return (bool)GetValue(IsPositiveIonProperty); }
            set { SetValue(IsPositiveIonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPositiveIon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPositiveIonProperty =
            DependencyProperty.Register("IsPositiveIon", typeof(bool), typeof(AlertConfigPage), new PropertyMetadata(false));

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

        private void btnSwitchIon_Click(object sender, RoutedEventArgs e)
        {
            switchIonType();
        }

        private void switchIonType()
        {
            IsPositiveIon = !IsPositiveIon;

            if (IsPositiveIon)
            {
                header.Content = "系統參數:警報設定(陽離子)";
                AlertConfigs.Clear();
                foreach (var config in positiveIonAlertConfig)
                {
                    AlertConfigs.Add(config);
                }
            }
            else
            {
                header.Content = "系統參數:警報設定(陰離子)";
                AlertConfigs.Clear();
                foreach (var config in minusIonAlertConfig)
                {
                    AlertConfigs.Add(config);
                }
            }

        }
    }
}
