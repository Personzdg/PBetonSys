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
    public class SiloStatController : Controller
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


    public class SiloStatApiController : ApiController
    {
        public dynamic GetSiloStatList(RequestWrapper query)
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
            List<SiloStat> retList = new SiloStatService().GetSiloStat(strartDate);  //(strartDate, endDate);
            return retList;
        }


        public dynamic GetTotalSiloStat(RequestWrapper query)
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
            List<SiloStat> retList = new SiloStatService().GetTotalSiloStat(strartDate);  //(strartDate, endDate);
            return retList;
        }


        //

        public dynamic GetSNTotalSiloStat(RequestWrapper query)
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
            var ret = new SiloStatService().GetSNTotalSiloStat(strartDate);
            return ret;

        }

    }


}