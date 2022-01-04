using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmedOut.Console
{
    public class ConfigProvider
    {
        private const string WEB3_PRIV_KEY = "web3key";

        public static string GetETH_PrivKey() => GetValueFromEnv(WEB3_PRIV_KEY, "");

        private static T GetValueFromEnv<T>(string key, T defaultValue)
        {
            T value;
            try
            {
                value = (T)Convert.ChangeType(Environment.GetEnvironmentVariable(key), typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }

            return value;
        }//        private static string GetValueFromEnv(string key)
    }
}
