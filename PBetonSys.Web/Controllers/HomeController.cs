using PBetonSys.Core;
using PBetonSys.Web.App_Start.webstack;
using PBetonSys.Web.Areas.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBetonSys.Web.Controllers
{
    [MvcMenuFilter(false)]
    public class HomeController : Controller
    {
        /// <summary>
        /// 系统首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var loginer = FormsAuth.GetUserData<LoginerBase>();
            ViewBag.Title = "系统";
            ViewBag.UserName = loginer.UserName;
            ViewBag.Settings = new ClerkService().GetCurrentUserSettings();

            return View();
        }
	}
}