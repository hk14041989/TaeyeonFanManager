using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaeyeonFanManagerSite.Models;
using TaeyeonFanManagerSite.DAL;
using System.Data.Entity.Infrastructure;

namespace TaeyeonFanManagerSite.Controllers
{
    public class ConceptController : Controller
    {
        private TeamContext db = new TeamContext();

        // GET: /Concept/
        public async Task<ActionResult> Index()
        {
            var concepts = db.Concepts.Include(c => c.Administrator);
            return View(await concepts.ToListAsync());
        }

        // GET: /Concept/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Commenting out original code to show how to use a raw SQL query.
            //Concept concept = await db.Concepts.FindAsync(id);

            // Create and execute raw SQL query.
            string query = "SELECT * FROM Concept WHERE ConceptID = @p0";
            Concept concept = await db.Concepts.SqlQuery(query, id).SingleOrDefaultAsync();
            if (concept == null)
            {
                return HttpNotFound();
            }
            return View(concept);
        }

        // GET: /Concept/Create
        public ActionResult Create()
        {
            ViewBag.IdolID = new SelectList(db.Idols, "ID", "FullName");
            return View();
        }

        // POST: /Concept/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ConceptID,Name,Budget,StartDate,IdolID")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Concepts.Add(concept);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdolID = new SelectList(db.Idols, "ID", "FullName", concept.IdolID);
            return View(concept);
        }

        // GET: /Concept/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = await db.Concepts.FindAsync(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdolID = new SelectList(db.Idols, "ID", "FullName", concept.IdolID);
            return View(concept);
        }

        // POST: /Concept/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Name", "Budget", "StartDate", "IdolID", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var conceptToUpdate = await db.Concepts.FindAsync(id);
            if (conceptToUpdate == null)
            {
                Concept deletedConcept = new Concept();
                TryUpdateModel(deletedConcept, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The concept was deleted by another user.");
                ViewBag.InstructorID = new SelectList(db.Idols, "ID", "FullName", deletedConcept.IdolID);
                return View(deletedConcept);
            }

            if (TryUpdateModel(conceptToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(conceptToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Concept)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Concept)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("Name", "Current value: "
                                + databaseValues.Name);
                        if (databaseValues.Budget != clientValues.Budget)
                            ModelState.AddModelError("Budget", "Current value: "
                                + String.Format("{0:c}", databaseValues.Budget));
                        if (databaseValues.StartDate != clientValues.StartDate)
                            ModelState.AddModelError("StartDate", "Current value: "
                                + String.Format("{0:d}", databaseValues.StartDate));
                        if (databaseValues.IdolID != clientValues.IdolID)
                            ModelState.AddModelError("InstructorID", "Current value: "
                                + db.Idols.Find(databaseValues.IdolID).FullName);
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        conceptToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.IdolID = new SelectList(db.Idols, "ID", "FullName", conceptToUpdate.IdolID);
            return View(conceptToUpdate);
        }

        // GET: /Concept/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = await db.Concepts.FindAsync(id);
            if (concept == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(concept);
        }

        // POST: /Concept/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Concept concept)
        {
            try
            {
                db.Entry(concept).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = concept.ConceptID });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(concept);
            }
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
