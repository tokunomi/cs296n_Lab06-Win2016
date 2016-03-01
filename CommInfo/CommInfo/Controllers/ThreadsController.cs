using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommInfo.Models;
using System.Collections;

namespace CommInfo.Controllers
{
    public class ThreadsController : Controller
    {
        private CommInfoContext db = new CommInfoContext();

        // GET: Threads
        public ActionResult Index()
        {
            return View(db.Threads.ToList());
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);

            //List(IEnumerable(MessageViewModel)) messageVM = new List<IEnumerable(MessageViewModel)>;
            //IEnumerable<MessageViewModel> messageVM;
            List<MessageViewModel> messageVM = new List<MessageViewModel>();
            // needs an IEnumerable

            // Get the messages
            var messages = from m in db.Messages
                           select m;

            // In a loop:
            foreach (Message m in messages)
            {
                // TODO: Get the thread that contains each message
                var topic = (from t in db.Threads
                             where t.ThreadID == m.ThreadID
                             select t).FirstOrDefault();
                // Create a View model for the message and put it in the list of view models
                messageVM.Add(new MessageViewModel() { Date = m.Date,
                                                       From = m.From,
                                                       /*Topic = m.ThreadItem.Topic,
                                                       Subject = m.Subject,*/
                                                       Body = m.Body 
                                                      });
            }

            if (thread == null)
            {
                return HttpNotFound();
            }

            // if there is just one message, display it
            if (messageVM.Count == 1)
            {
                return View("Details", messageVM[0]);
            }
            // if there is more than one book display the list of books
            else
            {
                return View("Index", messageVM);
            }

            //return View(thread);
        }

        // GET: Threads/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThreadID")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thread);
        }

        // GET: Threads/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadID")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thread);
        }

        // GET: Threads/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Threads.Find(id);
            db.Threads.Remove(thread);
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
