using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class CommInfoContext : IdentityDbContext<Member>  // using Member instead of AppUser
    //public class CommInfoContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CommInfoContext() : base("name=CommInfoContext")
        {
        }

        public System.Data.Entity.DbSet<CommInfo.Models.Message> Messages { get; set; }
        public System.Data.Entity.DbSet<CommInfo.Models.Thread> Threads { get; set; }

        public System.Data.Entity.DbSet<CommInfo.Models.Forum> Fora { get; set; }

        //public System.Data.Entity.DbSet<CommInfo.Models.Member> Members { get; set; }  // using Identity's User model
    
    }
}
