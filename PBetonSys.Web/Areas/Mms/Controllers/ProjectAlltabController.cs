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
    public class ProjectAlltabController : Controller
    {
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    CheckDateTime=DateTime.Now.ToString("yyyy-MM-dd"),
                }
            };
            return View(model);
        }

    }


    public class ProjectAlltabApiController : ApiController 
    {
        public dynamic GetProjectAlltabList(RequestWrapper query)
        {
            string strartDate = "";
            string endDate="";
            string queryDate = query["CheckDateTime"];
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
            List<ProjectAlltab> retList = new ProjectAlltabService().GetProjectAlltabData(strartDate, endDate);
            return retList;
        }

        public dynamic GetTotalProjectAlltabList(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["CheckDateTime"];
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

            var ret = new ProjectAlltabService().GetProjectAlltabData(strartDate, endDate);
            return ret;

        }
    }

}


