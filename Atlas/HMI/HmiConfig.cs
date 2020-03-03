using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using HMI.Properties;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace HMI
{
    public class Account
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public AccessLevel AccessLevel { get; set; }

        public override string ToString()
        {
            return $"ID->{ID}, Password->{Password}, AC=>{AccessLevel}";
        }
    }

    public class AccessLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"AccessLevel: convert ${value.GetType().FullName} to enum");
            if (value is AccessLevel ac)
            {
                return Enum.GetName(typeof(AccessLevel), ac);
            }
            else
            {
                throw new Exception($"Unexpected type {value.GetType().FullName}");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"AccessLevel: convertBack ${value.GetType().FullName} to enum");
            if (value is string str && Enum.TryParse<AccessLevel>(str, out AccessLevel ac))
            {
                return ac;
            }
            else
            {
                throw new Exception($"Unexpected type {value.GetType().FullName}");
            }
        }
    }

    public class JsonConfig
    {
        public List<string> AI_ID;
        public List<string> Unit;
        public List<string> SysState;
        public List<string> Step;

        public override string ToString() => $"AI_ID:{string.Join(",", AI_ID.ToArray())}" +
            $" Unit:{string.Join(",", Unit.ToArray())}" +
            $"SysState:{string.Join(",", SysState.ToArray())}" +
            $"Step:{string.Join(",", Step.ToArray())}";
    }

    sealed class HmiConfig
    {
        private static volatile HmiConfig instance;
        public static HmiConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HmiConfig();
                }

                return instance;
            }
        }

        private string finsAddr = string.Empty;
        public string FinsAddr
        {
            get => finsAddr; set
            {
                finsAddr = value;
                HMI.Properties.Settings.Default.Save();
            }
        }

        public JsonConfig AppConfig;
        private HmiConfig()
        {
            var setting = HMI.Properties.Settings.Default;
            AccountList = new List<Account>();
            LoadAccount();
            FinsAddr = setting.finsAddr;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Config\config.json");
            string json = File.ReadAllText(path);

            AppConfig = JsonConvert.DeserializeObject<JsonConfig>(json);
        }

        public List<Account> AccountList { get; set; }

        private void LoadAccount()
        {
            var setting = HMI.Properties.Settings.Default;
            int num_account = setting.userList.Count;
            for (int i = 0; i < num_account; i++)
            {
                var account = new Account
                {
                    ID = setting.userList[i],
                    Password = setting.passwordList[i],
                    AccessLevel = (AccessLevel)Enum.Parse(typeof(AccessLevel), setting.accessLevelList[i])
                };
                AccountList.Add(account);
            }
        }
        public void SaveAccount()
        {
            //Serialized AccountList
            HMI.Properties.Settings.Default.userList.Clear();
            HMI.Properties.Settings.Default.passwordList.Clear();
            HMI.Properties.Settings.Default.accessLevelList.Clear();
            foreach (Account account in AccountList)
            {
                HMI.Properties.Settings.Default.userList.Add(account.ID);
                HMI.Properties.Settings.Default.passwordList.Add(account.Password);
                HMI.Properties.Settings.Default.accessLevelList.Add(account.AccessLevel.ToString());
            }

            HMI.Properties.Settings.Default.Save();
        }
    }
}
