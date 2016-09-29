using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaeyeonFanManagerSite.DAL;
using TaeyeonFanManagerSite.Models;
using TaeyeonFanManagerSite.ViewModels;

namespace TaeyeonFanManagerSite.Controllers
{
    public class HomeController : Controller
    {
        private TeamContext db = new TeamContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<JoinedDateDateGroup> data = from fan in db.Fans
                                                   group fan by fan.JoinedDate into dateGroup
                                                   select new JoinedDateDateGroup()
                                                   {
                                                       JoinedDateDate = dateGroup.Key,
                                                       FanCount = dateGroup.Count()
                                                   };

            //// SQL version of the above LINQ code.
            //string query = "SELECT JoinedDate, COUNT(*) AS FanCount "
            //    + "FROM Person "
            //    + "WHERE Discriminator = 'Fan' "
            //    + "GROUP BY JoinedDate";
            //IEnumerable<JoinedDateDateGroup> data = db.Database.SqlQuery<JoinedDateDateGroup>(query);

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult getLoginData(User obj)
        {
            var user = db.Users.Where(x => x.UserName.Equals(obj.UserName) && x.Password.Equals(obj.Password)).FirstOrDefault();
            return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult UserLogin()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}