using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaeyeonFanManagerSite.Models;
using TaeyeonFanManagerSite.DAL;
using TaeyeonFanManagerSite.ViewModels;
using System.Data.Entity.Infrastructure;

namespace TaeyeonFanManagerSite.Controllers
{
    public class IdolController : Controller
    {
        private TeamContext db = new TeamContext();

        // GET: /Idol/
        public ActionResult Index(int? id, int? offlineID)
        {
            var viewModel = new IdolIndexData();
            viewModel.Idols = db.Idols
                .Include(i => i.FanSign)
                .Include(i => i.Offlines.Select(c => c.Concept))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.IdolID = id.Value;
                viewModel.Offlines = viewModel.Idols.Where(
                    i => i.ID == id.Value).Single().Offlines;
            }

            if (offlineID != null)
            {
                ViewBag.OfflineID = offlineID.Value;
                // Lazy loading
                //viewModel.JoinedDates = viewModel.Offlines.Where(
                //    x => x.OfflineID == offlineID).Single().JoinedDates;
                // Explicit loading
                var selectedOffline = viewModel.Offlines.Where(x => x.OfflineID == offlineID).Single();
                db.Entry(selectedOffline).Collection(x => x.JoinedDates).Load();
                foreach (JoinedDate joinedDate in selectedOffline.JoinedDates)
                {
                    db.Entry(joinedDate).Reference(x => x.Fan).Load();
                }

                viewModel.JoinedDates = selectedOffline.JoinedDates;
            }

            return View(viewModel);
        }

        // GET: /Idol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idol idol = db.Idols.Find(id);
            if (idol == null)
            {
                return HttpNotFound();
            }
            return View(idol);
        }

        // GET: /Idol/Create
        public ActionResult Create()
        {
            var idol = new Idol();
            idol.Offlines = new List<Offline>();
            PopulateAssignedOfflineData(idol); 
            return View();
        }

        // POST: /Idol/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,MeetDate,FanSign")]Idol idol, string[] selectedOfflines)
        {
            if (selectedOfflines != null)
            {
                idol.Offlines = new List<Offline>();
                foreach (var offline in selectedOfflines)
                {
                    var offlineToAdd = db.Offlines.Find(int.Parse(offline));
                    idol.Offlines.Add(offlineToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Idols.Add(idol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedOfflineData(idol);
            return View(idol);
        }

        // GET: /Idol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idol idol = db.Idols
                .Include(i => i.FanSign)
                .Include(i => i.Offlines)
                .Where(i => i.ID == id)
                .Single();

            PopulateAssignedOfflineData(idol);

            if (idol == null)
            {
                return HttpNotFound();
            }
            return View(idol);
        }

        private void PopulateAssignedOfflineData(Idol idol)
        {
            var allOfflines = db.Offlines;
            var idolOfflines = new HashSet<int>(idol.Offlines.Select(c => c.OfflineID));
            var viewModel = new List<AssignedOfflineData>();
            foreach (var offline in allOfflines)
            {
                viewModel.Add(new AssignedOfflineData
                {
                    OfflineID = offline.OfflineID,
                    Title = offline.Title,
                    Assigned = idolOfflines.Contains(offline.OfflineID)
                });
            }
            ViewBag.Offlines = viewModel;
        }

        // POST: /Idol/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedOfflines)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var idolToUpdate = db.Idols
               .Include(i => i.FanSign)
               .Include(i => i.Offlines)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(idolToUpdate, "",
               new string[] { "LastName", "FirstMidName", "MeetDate", "FanSign" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(idolToUpdate.FanSign.Location))
                    {
                        idolToUpdate.FanSign = null;
                    }

                    UpdateIdolOfflines(selectedOfflines, idolToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedOfflineData(idolToUpdate);
            return View(idolToUpdate);
        }

        private void UpdateIdolOfflines(string[] selectedOfflines, Idol IdolToUpdate)
        {
            if (selectedOfflines == null)
            {
                IdolToUpdate.Offlines = new List<Offline>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedOfflines);
            var instructorCourses = new HashSet<int>
                (IdolToUpdate.Offlines.Select(c => c.OfflineID));
            foreach (var course in db.Offlines)
            {
                if (selectedCoursesHS.Contains(course.OfflineID.ToString()))
                {
                    if (!instructorCourses.Contains(course.OfflineID))
                    {
                        IdolToUpdate.Offlines.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.OfflineID))
                    {
                        IdolToUpdate.Offlines.Remove(course);
                    }
                }
            }
        }

        // GET: /Idol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idol idol = db.Idols.Find(id);
            if (idol == null)
            {
                return HttpNotFound();
            }
            return View(idol);
        }

        // POST: /Idol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Idol idol = db.Idols
              .Include(i => i.FanSign)
              .Where(i => i.ID == id)
              .Single();

            db.Idols.Remove(idol);

            var concept = db.Concepts
                .Where(d => d.IdolID == id)
                .SingleOrDefault();
            if (concept != null)
            {
                concept.IdolID = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
