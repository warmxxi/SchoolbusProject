using Model.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Content.Resources
{
    public class UserAccountService
    {
        public UserAccountDTO UserAccount {
            get
            {
                string result = HttpContext.Current.Session["UserAccount"].ToString();
                return result == null ? default(UserAccountDTO) : JsonConvert.DeserializeObject<UserAccountDTO>(result);
            }
            set
            {
                string data = JsonConvert.SerializeObject(value);
                HttpContext.Current.Session["UserAccount"] = data;
            }
        }
    }
}