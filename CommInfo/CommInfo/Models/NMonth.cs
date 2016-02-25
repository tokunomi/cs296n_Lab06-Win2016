using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class NMonth
    {
        List<NDay> ndays = new List<NDay>();

        public List<NDay> NDays 
        {
            get { return ndays; }
        }
    }
}