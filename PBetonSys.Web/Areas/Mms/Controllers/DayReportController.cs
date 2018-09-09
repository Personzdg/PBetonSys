using PBetonSys.Core;
using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class DayReportController : Controller 
    {
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    Provide_DateTime = DateTime.Now.ToString("yyyy-MM-dd"),
                }
            };
            return View(model);
        }
    
    }


    public class DayReportApiController : ApiController
    {
        public dynamic GetDayReportList(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["Provide_DateTime"];
            if (queryDate.Contains("到"))
            {
                strartDate = queryDate.Split('到')[0];
                endDate = queryDate.Split('到')[1];
            }
            else
            {
                strartDate = queryDate;
                endDate = queryDate;
            }
            List<DayReport> retList = new DayReportService().GetDayReportData(strartDate, endDate);
            return retList;
        }

        public dynamic GetTotalDayReport(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["Provide_DateTime"];
            if (queryDate.Contains("到"))
            {
                strartDate = queryDate.Split('到')[0];
                endDate = queryDate.Split('到')[1];
            }
            else
            {
                strartDate = queryDate;
                endDate = queryDate;
            }
            List<DayReport> retList = new DayReportService().GetTotalDayReport(strartDate, endDate);
            return retList;
        }

    }

}
