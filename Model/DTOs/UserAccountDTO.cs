using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class UserAccountDTO
    {
        public string User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_Address { get; set; }
        public string Profile_Name { get; set; }
        public string Profile_Image { get; set; }
        public UserPermissionDTO Permissions { get; set; }
    }

    public class UserPermissionDTO
    {
        public bool Is_Admin { get; set; }
    }

    public class UserAuthenDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Is_Remember { get; set; }
    }

    public class UserRegisterDTO
    {
        public string User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
