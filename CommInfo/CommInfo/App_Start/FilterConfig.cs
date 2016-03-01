using System.Web;
using System.Web.Mvc;

namespace CommInfo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());  // not added yet -- this makes the entire app secure by default
        }
    }
}


