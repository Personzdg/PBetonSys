using PBetonSys.Web.App_Start.webstack;
using System.Web;
using System.Web.Mvc;

namespace PBetonSys.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new MvcHandleErrorAttribute());
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
           // filters.Add(new MvcDisposeFilter());
            filters.Add(new MvcMenuFilter());
        }
    }
}
