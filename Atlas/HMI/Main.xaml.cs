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
using System.Windows.Threading;
using OMRON.Compolet.SYSMAC;

namespace HMI
{
    /// <summary>
    /// Main.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class Main : Page
    {
        private DispatcherTimer dispatcherTimer = null;
        private void OnTimerTick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString();
        }

        private async Task sysmacPlcStateAsync()
        {
            {
                var (ret, boolArray) = await Sysmac.ReadBitAsync(SysmacCSBase.MemoryTypes.WR, 30, 0, 2);
                if (ret)
                {
                    _ = Dispatcher.BeginInvoke(new Action(() =>
                    {
                        IsOnSite = boolArray[0];
                        IsSystemHalt = boolArray[1];
                    }));
                }
                else
                    Trace.WriteLine("Fail to read IsOnSite.");
            }
            {
                var (ret, dataCh) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 320);
                if (ret)
                {
                    _ = Dispatcher.BeginInvoke(new Action(() =>
                    {
                        DataChannelNum = dataCh;
                    }));
                }
                else
                    Trace.WriteLine("Fail to read DatCh.");
            }
            {
                var (ret, flow) = await Sysmac.ReadFloatAsync(SysmacCSBase.MemoryTypes.DM, 1849);
                if (ret)
                {
                    _ = Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Flow = flow;
                    }));
                }
                else
                    Trace.WriteLine("Fail to read Flow.");
            }

            {
                var (ret, ctrlName) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.WR, 69);
                if (ret)
                {
                    uint value = (uint)ctrlName;
                    _ = Dispatcher.BeginInvoke(new Action(() =>
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
                    Trace.WriteLine("Fail to read Ctrl Name.");
            }

            {
                var (ret, systemState) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 493);
                if (ret)
                {
                    int idx = (systemState - 1) % HmiConfig.Instance.AppConfig.SysState.Count();
                    lblSystemState.Content = HmiConfig.Instance.AppConfig.SysState[idx];
                }
                else
                    Trace.WriteLine("Fail to read System State.");
            }
            {
                var (ret, seq) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 490);

                if (ret)
                    txtExecSeq.Text = $"{seq}";
            }
            {
                var (ret, sampleChanel) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 0);
                if (ret)
                {
                    txtSampleChannel.Text = (sampleChanel % 100).ToString();
                }
            }
            {
                var (ret, cleanTime) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.WR, 242);
                var (ret1, showStep) = await Sysmac.ReadBitAsync(SysmacCSBase.MemoryTypes.WR, 90, 2);
                var (ret2, stepNo) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.WR, 243);
                var (ret3, cleanTime2) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 4);
                if (ret && ret1 && ret2 && ret3)
                {
                    Debug.WriteLine($"showStep={showStep}");
                    Debug.WriteLine($"cleanTime2={cleanTime2}");
                    var step = HmiConfig.Instance.AppConfig.Step[stepNo % HmiConfig.Instance.AppConfig.Step.Count()];

                    txtCleanTime.Text = $"{cleanTime}:{cleanTime2}";
                }
            }
            {
                var (ret, sampleTime) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 5, 2);
                if (ret)
                    txtSampleTime.Text = $"{sampleTime[0]}:{sampleTime[1]}";
            }
            {
                var (ret, analysisTime) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 9, 2);
                if (ret)
                    txtAnalysisTime.Text = $"{analysisTime[0]}:{analysisTime[1]}";
            }
            {
                var (ret, analysisChannel) = await Sysmac.ReadIntAsync(SysmacCSBase.MemoryTypes.DM, 23);
                if (ret)
                    txtAnalysisChannel.Text = analysisChannel.ToString();
            }
        }

        public Main()
        {
            InitializeComponent();
            CurrentTime = DateTime.Now.ToString();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.0);
            dispatcherTimer.Tick += OnTimerTick;
            dispatcherTimer.Start();

            // Init DP state
            _ = sysmacPlcStateAsync();
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
                _ = Sysmac.WriteBitAsync(SysmacCSBase.MemoryTypes.WR, 30, 0, value);
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
                _ = Sysmac.WriteBitAsync(SysmacCSBase.MemoryTypes.WR, 30, 1, value);
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
            _ = Sysmac.WriteBitAsync(SysmacCSBase.MemoryTypes.WR, 0, 1, true);
            MainWindow.Logout();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.RemoveBackEntry();
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            btnConfig.IsEnabled = false;
            _ = Sysmac.WriteBitAsync(SysmacCSBase.MemoryTypes.WR, 11, 0, true);
            MainWindow.Instance.Navigate(new ConfigPage());
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            btnStatus.IsEnabled = false;
            _ = Sysmac.WriteBitAsync(SysmacCSBase.MemoryTypes.WR, 11, 2, true);
            MainWindow.Instance.Navigate(new SysStatusPage());
        }

        private void txtExecSeq_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtExecSeq.Text;
            int newIntValue = int.Parse(newText);
            Debug.WriteLine($"txt = {newText}");
            _ = Sysmac.WriteIntAsync(SysmacCSBase.MemoryTypes.DM, 490, newIntValue);
        }
    }
}
