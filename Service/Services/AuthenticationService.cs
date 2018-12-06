using Common.Helpers;
using Model.DTOs;
using Service.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Service.Services
{
    public class AuthenticationService : SqlDataAccess
    {
        public void UserAuthentication(UserAuthenDTO User)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = "[SP_User_Authentication]";
                command.Parameters.Add(new SqlParameter("@Username", User.Username));
                command.Parameters.Add(new SqlParameter("@Password", User.Password));
                command.CommandType = CommandType.StoredProcedure;
                this.ExecuteNonQuery(command);
            }
        }
    }
}
