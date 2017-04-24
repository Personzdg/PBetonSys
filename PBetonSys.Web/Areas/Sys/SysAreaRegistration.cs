﻿using PBetonSys.Web.App_Start.webstack;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Sys
{
    public class SysAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Sys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "PBetonSys.Web.Areas." + this.AreaName + ".Controllers" }
            );

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                this.AreaName + "Api",
                "api/" + this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, action = RouteParameter.Optional, id = RouteParameter.Optional, namespaceName = new string[] { string.Format("PBetonSys.Web.Areas.{0}.Controllers", this.AreaName) } },
                new { action = new StartWithConstraint() }
            );
        }
    }
}