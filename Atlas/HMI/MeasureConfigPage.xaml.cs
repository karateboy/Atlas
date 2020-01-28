using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace HMI
{
    public class MeasureConfig
    {
        public string Name { get; set; }
        public decimal RawValue { get; set; }
        public decimal Coeff1 { get; set; }
        public decimal Coeff2 { get; set; }
        public decimal Coeff3 { get; set; }
        public bool Selected { get; set; }
        public decimal Max { get; set; }

        public decimal Swing { get; set; }
        public decimal Value { get; set; }
    }
    /// <summary>
    /// MeasureConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class MeasureConfigPage : Page
    {
        private ObservableCollection<MeasureConfig> minusIonConfig;
        private ObservableCollection<MeasureConfig> positiveIonConfig;
        private void InitizlizeIonConfig()
        {
            minusIonConfig = new ObservableCollection<MeasureConfig>
            {
                new MeasureConfig
                {
                    Name = "F-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.4600M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "Cl-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2300M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "NO2-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2100M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "Br-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.1600M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "NO3-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2300M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "SO4-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2300M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "PO4-",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2300M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
            };
            positiveIonConfig = new ObservableCollection<MeasureConfig>
            {
                new MeasureConfig
                {
                    Name = "Na+",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.4600M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "NH4+",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2300M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "K+",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2100M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "Mg2+",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.1600M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                },
                new MeasureConfig
                {
                    Name = "Ca2+",
                    RawValue = 0.00M,
                    Coeff1 = 0.000M,
                    Coeff2 = 0.2300M,
                    Coeff3 = 0.00M,
                    Selected = true,
                    Max = 0.00M,
                    Swing = 0.00M,
                    Value = 0.00M
                }
            };
        }
        public MeasureConfigPage()
        {
            InitializeComponent();
            InitizlizeIonConfig();
            switchIonType();
            measureConfig.ItemsSource = IonConfigs;
        }


        private void switchIonType()
        {
            IsPositiveIon = !IsPositiveIon;

            if (IsPositiveIon)
            {
                header.Content = "系統參數:係數設定(陽離子)";
                IonConfigs.Clear();
                foreach(var config in positiveIonConfig)
                {
                    IonConfigs.Add(config);
                }                
            }
            else
            {
                header.Content = "系統參數:係數設定(陰離子)";
                IonConfigs.Clear();
                foreach (var config in minusIonConfig)
                {
                    IonConfigs.Add(config);
                }
            }

        }
        public bool IsPositiveIon
        {
            get { return (bool)GetValue(IsPositiveIonProperty); }
            set { SetValue(IsPositiveIonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPositiveIon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPositiveIonProperty =
            DependencyProperty.Register("IsPositiveIon", typeof(bool), typeof(MeasureConfigPage), new PropertyMetadata(false));


        public ObservableCollection<MeasureConfig> IonConfigs
        {
            get { return (ObservableCollection<MeasureConfig>)GetValue(IonConfigsProperty); }
            set { SetValue(IonConfigsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IonConfigs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IonConfigsProperty =
            DependencyProperty.Register("IonConfigs", typeof(ObservableCollection<MeasureConfig>), typeof(MeasureConfigPage), new PropertyMetadata(new ObservableCollection<MeasureConfig>()));

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
    }
}
