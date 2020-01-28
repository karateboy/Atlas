using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace HMI
{
    /// <summary>
    /// SortConfigPage.xaml 的互動邏輯
    /// </summary>
    /// 

    public class SeqEntry
    {
        public ushort ID { get; set; }
        string[] seq;
        public string[] Seq
        {
            get
            {
                return seq;
            }
            set
            {
                seq = value;
            }
        }
    }

    public partial class SortConfigPage : Page
    {
        public ObservableCollection<SeqEntry> NormalSeqList
        {
            get { return (ObservableCollection<SeqEntry>)GetValue(NormalSeqListProperty); }
            set { SetValue(NormalSeqListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalSeqList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalSeqListProperty =
            DependencyProperty.Register("NormalSeqList", typeof(ObservableCollection<SeqEntry>), typeof(SortConfigPage), new PropertyMetadata(null));



        public ObservableCollection<SeqEntry> OverSeqList
        {
            get { return (ObservableCollection<SeqEntry>)GetValue(OverSeqListProperty); }
            set { SetValue(OverSeqListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OverSeqList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverSeqListProperty =
            DependencyProperty.Register("OverSeqList", typeof(ObservableCollection<SeqEntry>), typeof(SortConfigPage), new PropertyMetadata(null));



        public ObservableCollection<SeqEntry> LockSeqList
        {
            get { return (ObservableCollection<SeqEntry>)GetValue(LockSeqListProperty); }
            set { SetValue(LockSeqListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LockSeqList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LockSeqListProperty =
            DependencyProperty.Register("LockSeqList", typeof(ObservableCollection<SeqEntry>), typeof(SortConfigPage), new PropertyMetadata(null));




        public SortConfigPage()
        {
            InitializeComponent();
            NormalSeqList = new ObservableCollection<SeqEntry>
            {
                new SeqEntry
                {
                    ID = 0, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 10, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 20, Seq = new string[10]
                },
                 new SeqEntry
                {
                    ID = 30, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 40, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 50, Seq = new string[10]
                },
            };
            foreach (SeqEntry entry in NormalSeqList)
            {
                for (int i = 0; i < 10; i++)
                {
                    entry.Seq[i] = $"{entry.ID + i + 1}";
                }
            }
            normalSeq.ItemsSource = NormalSeqList;

            OverSeqList = new ObservableCollection<SeqEntry>
            {
                new SeqEntry
                {
                    ID = 0, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 10, Seq = new string[10]
                },
            };
            foreach (SeqEntry entry in OverSeqList)
            {
                for (int i = 0; i < 10; i++)
                {
                    entry.Seq[i] = $"{0}";
                }
            }
            overSeq.ItemsSource = OverSeqList;

            LockSeqList = new ObservableCollection<SeqEntry>
            {
                new SeqEntry
                {
                    ID = 0, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 10, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 20, Seq = new string[10]
                },
                new SeqEntry
                {
                    ID = 30, Seq = new string[10]
                },
            };
            foreach (SeqEntry entry in LockSeqList)
            {
                for (int i = 0; i < 10; i++)
                {
                    if(entry.ID + i < 32)
                        entry.Seq[i] = $"{0}";
                    else
                        entry.Seq[i] = "X";
                }
            }
            lockSeq.ItemsSource = LockSeqList;            
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
            MessageBox.Show("成功更新", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("SortConfigPage loaded");
        }
    }
}
