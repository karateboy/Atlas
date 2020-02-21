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
using System.Threading;

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
            CurrentTime = DateTime.Now.ToString();

            // Init DP state
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (FinsComm.Instance.ReadBit(FinsComm.MemAreaBitCode.WR_Bit, 30, 0, 2, out bool[] boolArray))
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        IsOnSite = boolArray[0];
                        IsSystemHalt = boolArray[1];
                    }));
                }
                else
                {
                    Trace.WriteLine("Fail to read IsOnSite.");
                }

                if (FinsComm.Instance.ReadInt(FinsComm.MemAreaWordCode.DM_Word, 320, 1, out int[] dataCh))
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        DataChannelNum = dataCh[0];
                    }));
                }
                else
                {
                    Trace.WriteLine("Fail to read DatCh.");
                }


                if (FinsComm.Instance.ReadFloat(FinsComm.MemAreaWordCode.DM_Word, 1849, 1, out float[] flow))
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Flow = flow[0];
                    }));
                }
                else
                {
                    Trace.WriteLine("Fail to read Flow.");
                }

                if (FinsComm.Instance.ReadInt(FinsComm.MemAreaWordCode.WR_Word, 69, 1, out int[] ctrlName))
                {
                    uint value = (uint)ctrlName[0];
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (((value >> 5) & 1) != 0)
                            ControllerName = " AMC Manifold Controller";

                        if (((value >> 6) & 1) != 0)
                            ControllerName = " EGA Manifold Controller";

                        if (((value >> 7) & 1) != 0)
                            ControllerName = " PWA Manifold Controller";

                        if (((value >> 8) & 1) != 0)
                            ControllerName = " EWA Manifold Controller";
                    }));
                }
                else
                {
                    Trace.WriteLine("Fail to read Ctrl Name.");
                }
            });
        }

        public string CurrentTime
        {
            get { return (string)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(string), typeof(Main), new PropertyMetadata(string.Empty));



        public bool IsOnSite
        {
            get { return (bool)GetValue(IsOnSiteProperty); }
            set
            {
                SetValue(IsOnSiteProperty, value);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    FinsComm.Instance.WriteBit(FinsComm.MemAreaBitCode.WR_Bit, 30, 0, new bool[] { value });
                });
            }
        }

        // Using a DependencyProperty as the backing store for IsOnSite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOnSiteProperty =
            DependencyProperty.Register("IsOnSite", typeof(bool), typeof(Main), new PropertyMetadata(true));



        public bool IsSystemHalt
        {
            get { return (bool)GetValue(IsSystemHaltProperty); }
            set
            {
                SetValue(IsSystemHaltProperty, value);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    FinsComm.Instance.WriteBit(FinsComm.MemAreaBitCode.WR_Bit, 30, 1, new bool[] { value });
                });
            }
        }

        // Using a DependencyProperty as the backing store for IsSystemHalt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSystemHaltProperty =
            DependencyProperty.Register("IsSystemHalt", typeof(bool), typeof(Main), new PropertyMetadata(false));



        public int DataChannelNum
        {
            get { return (int)GetValue(DataChannelNumProperty); }
            set { SetValue(DataChannelNumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataChannelNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataChannelNumProperty =
            DependencyProperty.Register("DataChannelNum", typeof(int), typeof(Main), new PropertyMetadata(0));

        public float Flow
        {
            get { return (float)GetValue(FlowProperty); }
            set { SetValue(FlowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Flow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowProperty =
            DependencyProperty.Register("Flow", typeof(float), typeof(Main), new PropertyMetadata(0f));



        public string ControllerName
        {
            get { return (string)GetValue(ControllerNameProperty); }
            set { SetValue(ControllerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControllerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControllerNameProperty =
            DependencyProperty.Register("ControllerName", typeof(string), typeof(Main), new PropertyMetadata(""));




        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            btnLogout.IsEnabled = false;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                FinsComm.Instance.WriteBit(FinsComm.MemAreaBitCode.WR_Bit, 0, 1, new bool[] { true });
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindow.Logout();
                }));
            });

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.RemoveBackEntry();
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            btnConfig.IsEnabled = false;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                FinsComm.Instance.WriteBit(FinsComm.MemAreaBitCode.WR_Bit, 11, 0, new bool[] { true });
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindow.Instance.Navigate(new ConfigPage());
                }));
            });

        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            btnStatus.IsEnabled = false;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                FinsComm.Instance.WriteBit(FinsComm.MemAreaBitCode.WR_Bit, 11, 2, new bool[] { true });
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindow.Instance.Navigate(new SysStatusPage());
                }));
            });
        }

    }
}
