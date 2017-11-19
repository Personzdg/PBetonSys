using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class DispatchController : Controller
    {
        //
        // GET: /Mms/Dispatch/
        public ActionResult Index()
        {
            return View();
        }
	}

    public class DispatchApiController : ApiController
    {
        public dynamic GetDispatchList()
        {
            var result = new DispatchService().GetDispatchList();
            return result;
        }

    }
}