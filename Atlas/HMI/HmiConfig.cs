using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using HMI.Properties;
namespace HMI
{
    class Account
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
    sealed class HmiConfig
    {
        private static volatile HmiConfig instance;
        public static HmiConfig Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new HmiConfig();
                }

                return instance;
            }
        }
 
        private HmiConfig()
        {
            AccountList = new List<Account>();
            Load();
        }
        public List<Account> AccountList { get; set; }

        private void Load()
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
        public void Save()
        {
            /*
            Trace.WriteLine($"Update {configFileName}");
            byte[] jsonUtf8Bytes;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(this, options);
            */
            //JsonSerializer.Serialize<HmiConfig>(this,)
        }
    }
}
