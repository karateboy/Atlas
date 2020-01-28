using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace HMI
{
    public class MtConfig
    {
        public int Seq { get; set; }
        public bool Selected { get; set; }
        public string Name { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Unit { get; set; }

        public string Input { get; set; }
        public string Output { get; set; }
        public decimal Value { get; set; }
    }
    /// <summary>
    /// MtConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class MtConfigPage : Page
    {
        private ObservableCollection<MtConfig> minusIonConfig;
        private ObservableCollection<MtConfig> positiveIonConfig;
        private void InitizlizeIonConfig()
        {
            minusIonConfig = new ObservableCollection<MtConfig>
            {
                new MtConfig
                {
                    Seq = 11,
                    Name = "F-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 12,
                    Name = "Cl-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 13,
                    Name = "NO2-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 14,
                    Name = "Br-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 15,
                    Name = "NO3-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 16,
                    Name = "SO4-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 17,
                    Name = "PO4-",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
            };
            positiveIonConfig = new ObservableCollection<MtConfig>
            {
                new MtConfig
                {
                    Seq = 1,
                    Name = "Na+",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 2,
                    Name = "NH4+",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 3,
                    Name = "K+",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 4,
                    Name = "Mg2+",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                },
                new MtConfig
                {
                    Seq = 5,
                    Name = "Ca2+",
                    Selected = true,
                    Min = 0.00M,
                    Max = 100.00M,
                    Unit = "ppbv",
                    Input = "ASCII",
                    Output = "DIGIT",
                    Value = 0.00M
                }
            };
        }
        public MtConfigPage()
        {
            InitializeComponent();
            InitizlizeIonConfig();
            switchIonType();
            mtConfig.ItemsSource = IonConfigs;
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
            DependencyProperty.Register("IsPositiveIon", typeof(bool), typeof(MtConfigPage), new PropertyMetadata(false));


        public ObservableCollection<MtConfig> IonConfigs
        {
            get { return (ObservableCollection<MtConfig>)GetValue(IonConfigsProperty); }
            set { SetValue(IonConfigsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IonConfigs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IonConfigsProperty =
            DependencyProperty.Register("IonConfigs", typeof(ObservableCollection<MtConfig>), typeof(MtConfigPage), new PropertyMetadata(new ObservableCollection<MtConfig>()));

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
