using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class Thread
    {
        List<Message> message = new List<Message>();
        List<Member> member = new List<Member>();

        public int ThreadID { get; set; }
        public int MemberID { get; set; }
        //public int MessageID { get; set; }

        public List<Message> Messages 
        {
            get { return message; }
        }
        public List<Member> Members
        {
            get { return member; }
        }
        [StringLength(60)]
        public string Topic { get; set; }
    }
}