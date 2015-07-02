using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KSystem.Settings
{
    public static class AppSettings
    {
        public static T GetSettings<T>(string key) where T : IConvertible
        {
            string result = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(result))
                return (T)Convert.ChangeType(result, typeof(T));
            else
                return default(T);
        }
    }
}