using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LotusInn.Admin.Models
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResult
    {
        public string UserName { get; set; }
        public string AuthId { get; set; }
    }
}