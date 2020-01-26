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
    class HmiConfig
    {
        public HmiConfig()
        {
            Load();
        }

        List<Account> Accounts { get; set; }

        void Load()
        {
            
            
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
