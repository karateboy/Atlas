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
    public class AlertEvent
    {
        public bool Selected {get;set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CheckTime { get; set; }
        public string EventLevel { get; set; }
        public int Count { get; set; }
        public string Desc { get; set; }
    }

    /// <summary>
    /// EventPage.xaml 的互動邏輯
    /// </summary>
    public partial class EventPage : Page
    {


        public ObservableCollection<AlertEvent> AlertEventList
        {
            get { return (ObservableCollection<AlertEvent>)GetValue(AlertEventListProperty); }
            set { SetValue(AlertEventListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlertEventList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlertEventListProperty =
            DependencyProperty.Register("AlertEventList", typeof(ObservableCollection<AlertEvent>), typeof(EventPage), new PropertyMetadata(new ObservableCollection<AlertEvent>()));

        private List<AlertEvent> sudoAlertEventList = new List<AlertEvent>
        {
            new AlertEvent
            {
                Selected = false,
                StartTime = new DateTime(2020, 1,1),
                EndTime = new DateTime(2020,1,2),
                CheckTime = new DateTime(2020, 1,1),
                EventLevel = "M",
                Count = 4,
                Desc = "CH3 流量異常警報"
            },
            new AlertEvent
            {
                Selected = false,
                StartTime = new DateTime(2020, 1,1),
                EndTime = new DateTime(2020,1,2),
                CheckTime = new DateTime(2020, 1,1),
                EventLevel = "M",
                Count = 4,
                Desc = "CH4 流量異常警報"
            },
            new AlertEvent
            {
                Selected = false,
                StartTime = new DateTime(2020, 1,1),
                EndTime = new DateTime(2020,1,2),
                CheckTime = new DateTime(2020, 1,1),
                EventLevel = "M",
                Count = 1,
                Desc = "流量低限"
            }
        };
        private void obtainEventList()
        {
            AlertEventList.Clear();
            foreach(AlertEvent ae in sudoAlertEventList)
            {
                AlertEventList.Add(ae);
            }
            eventGrid.ItemsSource = AlertEventList;
        }
        public EventPage()
        {
            InitializeComponent();
            obtainEventList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.GoBack();
        }

        private void btnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Navigate(new Main());
        }

        private void btnDelSelected_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
