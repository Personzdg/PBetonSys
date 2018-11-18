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
    public class TransSurplController : Controller
    {
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    inCheckDatetime = DateTime.Now.ToString("yyyy-MM-dd"),
                   
                }
            };
            return View(model);
        }
    }

    public class TransSurplApiController : ApiController
    {
        public dynamic GetTransSurplList(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["inCheckDatetime"];
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
            List<TransSurpl> retList = new TransSurplService().GetTransSurplData(strartDate, endDate);
            return retList;
        }

        public dynamic GetTotalTransSurpl(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["inCheckDatetime"];
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
            List<TransSurpl> retList = new TransSurplService().GetTotalTransSurplData(strartDate, endDate);
            return retList;
        }

    }

}