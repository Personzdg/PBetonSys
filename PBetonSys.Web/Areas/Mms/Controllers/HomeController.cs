using PBetonSys.Web.App_Start.webstack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    [MvcMenuFilter(false)]
    public class HomeController : Controller
    {
        //
        // GET: /Mms/Home/
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("~/Login/mms");

            return View();
        }
	}
}