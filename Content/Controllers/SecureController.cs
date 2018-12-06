using Model.DTOs;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Content.Controllers
{
    public class SecureController : BaseController
    {
        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult Authen(UserAuthenDTO authen)
        {
            ServiceProvider.AuthenticationService.UserAuthentication(authen);
            return Content(JsonConvert.SerializeObject(""), "application/json");
        }
    }
}