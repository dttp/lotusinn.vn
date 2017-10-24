using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using LotusInn.Admin.Models;
using Newtonsoft.Json;

namespace LotusInn.Admin.APIControllers
{
    public class AccountController : ApiController
    {
        [AcceptVerbs("POST")]
        public LoginResult Login(LoginInfo loginInfo)
        {
            var pass = ConvertToMD5(loginInfo.Password);            
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/user.json");
            var ser = JsonSerializer.Create();
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))                
            {
                var reader = new StreamReader(fs);
                var jsonReader = new JsonTextReader(reader);
                var users = ser.Deserialize<List<LoginInfo>>(jsonReader);

                var user = users.Find(u => u.UserName.Equals(loginInfo.UserName) && u.Password == pass);

                if (user != null)
                {
                    return new LoginResult
                    {
                        UserName = user.UserName,
                        AuthId = Guid.NewGuid().ToString()
                    };
                }
                throw new Exception("Invalid username or password");
            }
        }

        public static string ConvertToMD5(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
