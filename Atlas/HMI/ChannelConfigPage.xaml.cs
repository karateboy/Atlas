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
    public class ChannelConfig
    {
        public int ID { get; set; }
        public bool Selected { get; set; }
        public bool Locked { get; set; }
        public string Name { get; set; }
        public int AlarmGroup { get; set; }
        public decimal Opening { get; set; }
        public TimeSpan CleanTimeSpan { get; set; }
        public TimeSpan SampleTimeSpan { get; set; }
        public TimeSpan AnalyzeTimeSpan { get; set; }
    }

    /// <summary>
    /// ChannelConfigPage.xaml 的互動邏輯
    /// </summary>
    public partial class ChannelConfigPage : Page
    {
        public ChannelConfigPage()
        {
            InitializeComponent();
            ChannelConfigs = new ObservableCollection<ChannelConfig>();
            for (int i = 0; i <= 32; i++)
            {
                var channl = new ChannelConfig
                {
                    ID = i,
                    Selected = true,
                    Locked = false,
                    Name = i == 0 ? "DI" : $"CH{i}",
                    AlarmGroup = i == 0 ? 0 : 1,
                    Opening = 0.00M,
                    CleanTimeSpan = new TimeSpan(0, 0, 0),
                    SampleTimeSpan = new TimeSpan(0, 30, 0),
                    AnalyzeTimeSpan = new TimeSpan(0, 29, 30)
                };
                ChannelConfigs.Add(channl);
            }

            channelConfigSeq.ItemsSource = ChannelConfigs;
        }



        public ObservableCollection<ChannelConfig> ChannelConfigs
        {
            get { return (ObservableCollection<ChannelConfig>)GetValue(ChannelConfigsProperty); }
            set { SetValue(ChannelConfigsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChannelConfigs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChannelConfigsProperty =
            DependencyProperty.Register("ChannelConfigs", typeof(ObservableCollection<ChannelConfig>), typeof(ChannelConfigPage), new PropertyMetadata(null));


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
    }
}
