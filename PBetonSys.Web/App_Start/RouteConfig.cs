using Newtonsoft.Json.Linq;
using PBetonSys.Web.App_Start.webstack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PBetonSys.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute("Report", "report", "~/Content/page/report.aspx");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "PBetonSys.Web.Controllers" }
            );

            ModelBinders.Binders.Add(typeof(JObject), new JObjectModelBinder()); //for dynamic model binder
        }
    }
}
