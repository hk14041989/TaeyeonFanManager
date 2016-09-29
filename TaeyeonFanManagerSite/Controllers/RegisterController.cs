﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaeyeonFanManagerSite.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public ActionResult Index()
        {
            return View();
        }

        //Registration form saving code
        [HttpPost]
        public JsonResult Register(User u)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                using (TaeyeonFanManagerSite5Entities dc = new TaeyeonFanManagerSite5Entities())
                {
                    //check username available
                    var user = dc.Users.Where(a => a.UserName.Equals(u.UserName)).FirstOrDefault();
                    if (user == null)
                    {
                        dc.Users.Add(u);
                        dc.SaveChanges();
                        message = "Success";
                    }
                    else
                    {
                        message = "UserName not available!";
                    }
                }
            }
            else
            {
                message = "Failed!";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
	}
}