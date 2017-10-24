using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Business
{
    public static class ConfigManager
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
        public static string ReservationEmail => ConfigurationManager.AppSettings["ReservationEmail"];
    }
}
