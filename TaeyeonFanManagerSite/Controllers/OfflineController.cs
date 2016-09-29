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
using System.Data.Entity.Infrastructure;

namespace TaeyeonFanManagerSite.Controllers
{
    public class OfflineController : Controller
    {
        private TeamContext db = new TeamContext();

        // GET: /Offline/
        public ActionResult Index(int? SelectedConcept)
        {
            var concepts = db.Concepts.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedConcept = new SelectList(concepts, "ConceptID", "Name", SelectedConcept);
            int conceptID = SelectedConcept.GetValueOrDefault();

            IQueryable<Offline> offlines = db.Offlines
                .Where(c => !SelectedConcept.HasValue || c.ConceptID == conceptID)
                .OrderBy(d => d.OfflineID)
                .Include(d => d.Concept);
            //var sql = offlines.ToString();
            return View(offlines.ToList());
        }

        // GET: /Offline/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offline offline = db.Offlines.Find(id);
            if (offline == null)
            {
                return HttpNotFound();
            }
            return View(offline);
        }

        // GET: /Offline/Create
        public ActionResult Create()
        {
            PopulateConceptsDropDownList();
            return View();
        }

        // POST: /Offline/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OfflineID,Title,TicketPrices,ConceptID")] Offline offline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Offlines.Add(offline);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateConceptsDropDownList(offline.ConceptID);
            return View(offline);
        }

        // GET: /Offline/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offline offline = db.Offlines.Find(id);
            if (offline == null)
            {
                return HttpNotFound();
            }
            PopulateConceptsDropDownList(offline.ConceptID);
            return View(offline);
        }

        // POST: /Offline/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offlineToUpdate = db.Offlines.Find(id);
            if (TryUpdateModel(offlineToUpdate, "",
               new string[] { "Title", "TicketPrices", "ConceptID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateConceptsDropDownList(offlineToUpdate.ConceptID);
            return View(offlineToUpdate);
        }

        private void PopulateConceptsDropDownList(object selectedConcept = null)
        {
            var conceptsQuery = from d in db.Concepts
                                   orderby d.Name
                                   select d;
            ViewBag.ConceptID = new SelectList(conceptsQuery, "ConceptID", "Name", selectedConcept);
        } 

        // GET: /Offline/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offline offline = db.Offlines.Find(id);
            if (offline == null)
            {
                return HttpNotFound();
            }
            return View(offline);
        }

        // POST: /Offline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offline offline = db.Offlines.Find(id);
            db.Offlines.Remove(offline);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateOfflineTicketPrices()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateOfflineTicketPrices(int? multiplier)
        {
            if (multiplier != null)
            {
                ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("UPDATE Offline SET TicketPrices = TicketPrices * {0}", multiplier);
            }
            return View();
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
