using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Helpers
{
    public static class DataConfigHelper
    {
        public static ConfigModel Config
        {
            get
            {
                string jsonConfig = "";

                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "AppSetting.json"))
                {
                    jsonConfig = sr.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<ConfigModel>(jsonConfig);
            }
        }
    }

    public class ConfigModel
    {
        public ServerModel Server { get; set; }
        public SessionModel Session { get; set; }
    }

    public class ServerModel
    {
        public string Server_Name { get; set; }
        public string Server_Username { get; set; }
        public string Server_Password { get; set; }
        public string Database_Name { get; set; }
        public string ConnectionString
        {
            get
            {
                return "dasdsadasdsad";
            }
        }
    }

    public class SessionModel
    {
        public int Timeout { get; set; }
    }
}
