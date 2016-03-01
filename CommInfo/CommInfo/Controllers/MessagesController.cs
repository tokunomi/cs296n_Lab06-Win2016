using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommInfo.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CommInfo.Controllers
{
    public class MessagesController : Controller
    {
        private CommInfoContext db = new CommInfoContext();

        // GET: Messages
        public ActionResult Index()
        {
            return View(GetThreadsAndMessages(0));
            //return View(db.Messages.ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageViewModel message = GetMessageAndThread(id);
            //Message message = db.Messages.Find(id);


            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.FromList =
                new SelectList(db.Users.OrderBy(m => m.UserName), "MemberID", "UserName");  // Using Identity's UserName?
                //new SelectList(db.Members.OrderBy(m => m.Username), "MemberID", "Username");

            ViewBag.ThreadList =
                new SelectList(db.Threads.OrderBy(t => t.Topic), "ThreadID", "Topic");

            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "MessageID,Date,From,Subject,Body, ThreadItem, ThreadList, MemberItem, FromList")] MessageViewModel messageVM, int? ThreadList, int FromList, string topicName)
        {
            if (ModelState.IsValid)
            {
                Member fromMember = (from mb in db.Users
                                 where mb.MemberID == FromList  // can't seem to use Identity's Id
                                 select mb).FirstOrDefault();
                //Member fromMember = (from mb in db.Members
                //                 where mb.MemberID == FromList  // can't seem to use Identity's Id
                //                 select mb).FirstOrDefault();
                //Message messageFrom = (from m in db.Messages
                //                   where m.MessageID == FromList
                //                   select m).FirstOrDefault();

                Thread thread = (from t in db.Threads
                                 where t.ThreadID == ThreadList
                                 select t).FirstOrDefault();

                if (thread == null)
                {
                    thread = new Thread();
                    thread.Topic = topicName;
                    //thread.Topic = messageVM.ThreadItem.Topic;
                    //db.Threads.Add(new Thread { Topic = messageVM.ThreadItem.Topic }); //
                    db.Threads.Add(thread); 
                }

                //Message message = new Message()
                //{
                //    Date = messageVM.Date,
                //    From = fromMember.Username,
                //    Subject = messageVM.Subject,
                //    Body = messageVM.Body,
                //    ThreadID = thread.ThreadID,
                //    MemberID = fromMember.MemberID
                //};

                Message message = new Message();
                    message.Date = messageVM.Date;
                    message.From = fromMember.UserName;  // this should be Identity's UserName
                    //message.From = fromMember.Username;
                    message.Subject = messageVM.Subject;
                    message.Body = messageVM.Body;
                    //message.MemberID = fromMember.Id;    
                    message.MemberID = fromMember.MemberID;

                thread.Messages.Add(message);

                //db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messageVM);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.FromList =
                new SelectList(db.Users.OrderBy(m => m.UserName), "MemberID", "UserName");
                //new SelectList(db.Members.OrderBy(m => m.Username), "MemberID", "Username");

            //ViewBag.ThreadList =
            //    new SelectList(db.Threads.OrderBy(t => t.Topic), "ThreadID", "Topic");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageViewModel message = GetMessageAndThread(id);
            //Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "MessageID,Date,From,Subject,Topic,Body, ThreadItem, MemberItem, FromList")] Message message, int FromList, string topicName)   // from Message model
        //public ActionResult Edit([Bind(Include = "MessageID,Date,From,Subject,Body, MemberItem, ThreadItem, FromList")] MessageViewModel messageVM, int FromList, string topicName)  
        //public ActionResult Edit([Bind(Include = "MessageID,Date,From,Subject,Body")] Message message)  // removed 'To' from Bind
        {
            if (ModelState.IsValid) // problem with Model 
            {
                db.Entry(message).State = EntityState.Modified;  
                //db.Entry(messageVM).State = EntityState.Modified;  // ERROR: System.InvalidOperationException: The entity type MessageViewModel is not part of the model for the current context.
                db.SaveChanges();  // FK conflict on UPDATE
                return RedirectToAction("Index");
            }
            return View(message);
            //return View(messageVM);
        }

        // GET: Messages/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Messages/Search
        public ActionResult Search()
        {
            return View();
        }


        // POST: Messages/Search
        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            List<MessageViewModel> messageVMs = new List<MessageViewModel>();
            // Get a list of messages whose Subject matches the search term
            var messages = from m in db.Messages
                           where m.Subject.Contains(searchTerm)
                               select m;

            //
            foreach (Message m in messages)
            {
                // get the threads
                var thread = (from t in db.Threads
                              where t.ThreadID == m.ThreadID
                              select t).FirstOrDefault();
                // Create a view model for the message and put the list of view models
                messageVMs.Add(new MessageViewModel() { Subject = m.Subject,
                                                        ThreadItem = thread,
                                                        Date = m.Date,
                                                        From = m.From,
                                                        Body = m.Body,
                                                        MessageID = m.MessageID
                });
            }

            // if there is only one message, display it
            if (messageVMs.Count == 1)
            {
                return View("Details", messageVMs[0]);
            }

            // if there are more than one message, put it in a list
            else
            {
                return View("Index", messageVMs);
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

        // ///////////////////////////////////////////////////////
        private List<MessageViewModel> GetThreadsAndMessages(int? messageId)
        {
            var messages = new List<MessageViewModel>();

            var threads = from thread in db.Threads.Include("Messages")
                         select thread;

            foreach (Thread t in threads)
            {
                foreach (Message m in t.Messages)
                {
                    if (m.MessageID == messageId || 0 == messageId)
                    {
                        var messageVM = new MessageViewModel();
                        messageVM.Date = m.Date;
                        messageVM.MessageID = m.MessageID;
                        messageVM.From = m.From;
                        messageVM.Subject = m.Subject;
                        messageVM.Body = m.Body;
                        messageVM.ThreadItem = t;
                        messages.Add(messageVM);
                    }
                }
            }
            return messages;
        }

        // for Message Details; returns a single message
        private MessageViewModel GetMessageAndThread(int? messageId)  
        {
            MessageViewModel MessageVm = (from m in db.Messages
                                    join t in db.Threads on m.ThreadID equals t.ThreadID
                                    where m.MessageID == messageId
                                    select new MessageViewModel
                                    {
                                        MessageID = m.MessageID,
                                        Date = m.Date,
                                        From = m.From,
                                        Subject = m.Subject,
                                        Body = m.Body,
                                        ThreadItem = t
                                    }).FirstOrDefault();
            return MessageVm;
        }

    }
}
