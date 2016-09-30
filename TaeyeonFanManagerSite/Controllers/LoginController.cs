using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaeyeonFanManagerSite.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getLoginData(User obj)
        {
            TaeyeonFanManagerSiteDBEntities db = new TaeyeonFanManagerSiteDBEntities();
            var user = db.Users.Where(x => x.Username.Equals(obj.Username) && x.Password.Equals(obj.Password)).FirstOrDefault();
            return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
	}
}