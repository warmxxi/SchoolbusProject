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
        public void UserRegister(UserAuthenDTO User)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = "[SP_ADD_USER]";
                command.Parameters.Add(new SqlParameter("@FIRST_NAME", User.Username));
                command.Parameters.Add(new SqlParameter("@LAST_NAME", User.Password));
                command.CommandType = CommandType.StoredProcedure;
                this.ExecuteNonQuery(command);
            }
        }
    }
}
