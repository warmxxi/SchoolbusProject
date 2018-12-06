using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public static class ServiceProvider
    {
        private static AuthenticationService authenticationService { get; set; }
        public static AuthenticationService AuthenticationService
        {
            get
            {
                if (authenticationService==null)
                {
                    authenticationService = new AuthenticationService();
                }

                return authenticationService;
            }
        }
    }
}
