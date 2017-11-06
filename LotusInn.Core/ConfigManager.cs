using System.Configuration;

namespace LotusInn.Core
{
    public class ConfigManager
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["LotusInn"].ConnectionString;
        public static string APIEndPoint => ConfigurationManager.AppSettings["APIEndPoint"];
    }
}
