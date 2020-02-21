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
        User = 1, Operator = 2, Administrator = 3
    }
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            IsLogin = false;
            handleArgument();
            FinsComm.Init();
            if (!FinsComm.Instance.TestCommunication())
            {
                MessageBox.Show($"HMI 通訊中斷! (請檢查{HmiConfig.Instance.FinsAddr}是否可正常通訊)");
            }
            //bool ret = false;
            //byte[] sendData = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
            //ret = FinsComm.Instance.WriteWord(FinsComm.MemAreaWordCode.DM_Word, 0, sendData);
            //Debug.WriteLine($"WriteWord ret={ret} {string.Join(",", sendData)}");
            //ret = FinsComm.Instance.ReadWord(FinsComm.MemAreaWordCode.DM_Word, 0, (uint) sendData.Length/2, out byte[] recv);
            //Debug.WriteLine($"ReadWord ret={ret} len={recv.Length} {string.Join(",", recv)}");
            //bool[] data = new bool[] {true, false, true, false };
            //ret = FinsComm.Instance.WriteBit(FinsComm.MemAreaBitCode.WR_Bit, 30, 0, in data);
            //Debug.WriteLine($"WriteBit ret={ret} len={data.Length} {string.Join(",", data)}");
            //ret = FinsComm.Instance.ReadBit(FinsComm.MemAreaBitCode.WR_Bit, 30, 0, 4, out bool[] boolArray);
            //Debug.WriteLine($"ReadBit ret={ret} len={boolArray.Length} {string.Join(",", boolArray)}");

        }

        private void handleArgument()
        {
            string[] args = Environment.GetCommandLineArgs();
            var dictionary = new Dictionary<string, string>();
            for (int index = 1; index < args.Length; index += 2)
            {                
                dictionary.Add(args[index], args[index + 1]);
            }

            if(dictionary.TryGetValue("fins_addr", out string fins_addr))
            {
                HmiConfig.Instance.FinsAddr = fins_addr;                
            }
        }

        public static MainWindow Instance { get; set; }
        public bool IsLogin { get; set; }
        public AccessLevel AccessLevel { get; set; }

        public static void Logout()
        {
            Instance.RemoveBackEntry();
            Instance.IsLogin = false;
            Instance.Navigate(new Login());
        }
    }
}
