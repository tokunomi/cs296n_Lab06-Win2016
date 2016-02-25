using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class MessageViewModel
    {
        Thread thread = new Thread();
        Member member = new Member();
        List<Member> members = new List<Member>();

        public int MessageID { get; set; }
        public int ThreadID { get; set; }
        public int MemberID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }  // redundant?
        public string Topic { get; set; } // not sure about this, but Edit may need it
        public string Body { get; set; }

        public List<Member> Members 
        {
            get { return members;}
        }

        public Thread ThreadItem { get { return thread; } set { thread = value; } }

        public Member MemberItem { get { return member; } set { member = value; } }
    }
}