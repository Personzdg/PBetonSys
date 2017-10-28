using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Mms/Report/
        public ActionResult Index()
        {
            List<SilotReport>  list=new SilotReportService().GetSilotReportData();
            return View(list);
        }
	}
}