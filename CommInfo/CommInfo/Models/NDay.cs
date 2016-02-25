using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class NDay
    {
        List<News> news = new List<News>();

        public string NewsDay { get; set; }
        public List<News> News 
        { 
            get { return news; }
        }
    }
}