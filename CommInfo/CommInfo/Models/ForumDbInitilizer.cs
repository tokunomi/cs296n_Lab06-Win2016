using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CommInfo.Models
{
    //public class ForumDbInitilizer : DropCreateDatabaseAlways<CommInfoContext>
    public class ForumDbInitilizer : DropCreateDatabaseIfModelChanges<CommInfoContext>
    {
        Forum forum = new Forum();
        protected override void Seed(CommInfoContext context)
        {
            //DateTime seedDate = new DateTime().ToLongDateString(2016, 01, 01);
            DateTime seedDate = new DateTime(2016, 01, 01);

            // Member Seed
            Member memb001 = new Member { NameFirst = "John", NameLast = "Watson", UserName = "John", Email = "jwatson@test.com" };  // UserName, Email from Identity
            Member memb002 = new Member { NameFirst = "David", NameLast = "Yamamoto", UserName = "David", Email = "bigwavedave@test.com" };
            Member memb003 = new Member { NameFirst = "Carrie", NameLast = "Tam", UserName = "Carrie", Email = "carrietam@test.com" };
            Member memb004 = new Member { NameFirst = "Nani", NameLast = "Kealoha", UserName = "Nani", Email = "nanik@test.com" };

            // Fora seed
            Forum frm001 = new Forum { ForumID = 01, ForumName = "Forum 1" };
            Forum frm002 = new Forum { ForumID = 02, ForumName = "Forum 2" };

            // Thread ("Topics") seed
            //Thread trd001 = new Thread { Topic = "Aiea Vs. Farrington" };
            //Thread trd002 = new Thread { Topic = "Surf Hui Planing Meeting" };
            //Thread trd003 = new Thread { Topic = "Aiea Book Club" };
            //Thread trd004 = new Thread { Topic = "Kite Club" };
            Thread trd001 = new Thread { MemberID = 01, Topic = "Aiea Vs. Farrington" };
            Thread trd002 = new Thread { MemberID = 02, Topic = "Surf Hui Planing Meeting" };
            Thread trd003 = new Thread { MemberID = 03, Topic = "Aiea Book Club" };
            Thread trd004 = new Thread { MemberID = 04, Topic = "Kite Club" };
            //Thread trd001 = new Thread { ThreadID = 01, Topic = "Test Topic 1" };
            //Thread trd002 = new Thread { ThreadID = 02, Topic = "Test Topic 2"};

            // Messages seed
            Message msg01 = new Message { MemberID = 01, Date = seedDate, From = "John", Subject = "Did you see the game?", Body = "The Alii trashed the Bulldogs!" };
            Message msg02 = new Message { MemberID = 02, Date = seedDate, From = "David", Subject = "Meeting at Aiea Library this Friday", Body = "We're planning our next surf meet. Anyone want to come?" };
            Message msg03 = new Message { MemberID = 03, Date = seedDate, From = "Carrie", Subject = "Bookclub meeting this week?", Body = "Are we having a meeting this Thursday?" };
            Message msg04 = new Message { MemberID = 04, Date = seedDate, From = "Nani", Subject = "Pearl Harbor Park Picnic", Body = "Bring a kite and the keiki! We have the musubi ready." };
            //Message msg01 = new Message { Date = seedDate, Subject = "Did you see the game?" };
            //Message msg02 = new Message { Date = seedDate, Subject = "Meeting at Aiea Library this Friday" };
            //Message msg03 = new Message { Date = seedDate, Subject = "Bookclub meeting this week?" };
            //Message msg04 = new Message { Date = seedDate, Subject = "Pearl Harbor Park Picnic" };
            //Message msg01 = new Message { Date = seedDate, From = "John", Subject = "Did you see the game?" };
            //Message msg02 = new Message { Date = seedDate, From = "David", Subject = "Meeting at Aiea Library this Friday" };
            //Message msg03 = new Message { Date = seedDate, From = "Carrie", Subject = "Bookclub meeting this week?" };
            //Message msg04 = new Message { Date = seedDate, From = "Nani", Subject = "Pearl Harbor Park Picnic" };

            trd001.Members.Add(memb001);
            trd002.Members.Add(memb002);
            trd003.Members.Add(memb003);
            trd004.Members.Add(memb004);
            trd001.Messages.Add(msg01);
            trd002.Messages.Add(msg02);
            trd003.Messages.Add(msg03);
            trd004.Messages.Add(msg04);
            frm001.Threads.Add(trd001);
            frm001.Threads.Add(trd002);
            frm002.Threads.Add(trd003);
            frm002.Threads.Add(trd004);
            frm001.Members.Add(memb001);
            frm001.Members.Add(memb002);
            frm001.Members.Add(memb003);
            frm001.Members.Add(memb004);
            frm002.Members.Add(memb001);
            frm002.Members.Add(memb002);
            frm002.Members.Add(memb003);
            frm002.Members.Add(memb004);
            context.Fora.Add(frm001);
            context.Fora.Add(frm002);
            //trd001.Messages.Add(msg01);
            //trd001.Messages.Add(msg02);
            //trd001.Messages.Add(msg03);
            //trd002.Messages.Add(msg04);
            //frm001.Threads.Add(trd001);
            //frm002.Threads.Add(trd002);
            //frm001.Members.Add(memb001);
            //frm001.Members.Add(memb002);
            //frm001.Members.Add(memb003);
            //frm001.Members.Add(memb004);
            //frm002.Members.Add(memb001);
            //frm002.Members.Add(memb002);
            //frm002.Members.Add(memb003);
            //frm002.Members.Add(memb004);
            //context.Fora.Add(frm001);
            //context.Fora.Add(frm002);
            
            //context.Threads.Add(trd001);
            //forum.Threads.Add(trd001);

            base.Seed(context);  // default
        }
    }
}