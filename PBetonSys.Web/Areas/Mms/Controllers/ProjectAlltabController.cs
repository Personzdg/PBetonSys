using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class ProjectAlltabController : Controller
    {
        public ActionResult Index()
        {
            List<ProjectAlltab> list = new ProjectAlltabService().GetProjectAlltabData(); 
            return View(list);
        }

    }
}


