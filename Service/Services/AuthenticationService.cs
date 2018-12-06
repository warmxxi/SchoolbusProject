using Common.Helpers;
using Service.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
    public class AuthenticationService : SqlDataAccess
    {
        public string TestSqlDataAccess()
        {
            return DataConfigHelper.Config.Server.Server_Name;
        }
    }
}
