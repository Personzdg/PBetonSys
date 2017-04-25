using Newtonsoft.Json.Linq;
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
    [AllowAnonymous]
    [MvcMenuFilter(false)]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.CnName = "企业管理系统";
            //ViewBag.EnName = "Enterprise Mangange System";
            //return View();
            return Mms();
        }

        public ActionResult Mms()
        {
            ViewBag.CnName = "龙鼎混凝土管理系统";
            ViewBag.EnName = "Engineering Material Mangange System";
            return View("Index");
        }

        public JsonResult DoAction(JObject request)
        {
            var message = new ClerkService().Login(request);
            return Json(message, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Logout()
        {
            FormsAuth.SingOut();
            return Redirect("~/Login");
        }
	}
}