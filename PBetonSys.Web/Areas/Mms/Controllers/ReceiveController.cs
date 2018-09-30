using PBetonSys.Core;
using PBetonSys.Utils;
using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class ReceiveController : Controller 
    {
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    ctime = DateTime.Now.ToString("yyyy-MM-dd"),
                }
            };
            return View(model);
        }


        public ActionResult ProjectNameReceivedt()
        {

            var flagtypeList = new List<dynamic>();
            flagtypeList.Add(new { text = "单方", value = "0" });
            flagtypeList.Add(new { text = "全部", value = "1" });

            var model = new
            {

                dataSource = new
                {

                    FlagtypeList = flagtypeList,

                },

                form = new
                {
                   
                    ctime = DateTime.Now.ToString("yyyy-MM-dd"),
                   flag=1
                }
            };
            return View(model);
        }
    
    }

    public class ReceiveApiController : ApiController
    {
        public dynamic GetReceiveData(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["ctime"];
            //int flag = Convert.ToInt32(query["flag"]);
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
            int index = ZConvert.To<int>(query["page"]);
            int pageSize = ZConvert.To<int>(query["rows"]);
            //dynamic result = new ExpandoObject();
            //result.rows = new ReceiveService().GetReceiveData(strartDate, endDate);
            //result.total = this.queryRowCount(param, result.rows);
            //List<Receive> retList = new ReceiveService().GetReceiveData(strartDate, endDate);
            //return retList;
            return new ReceiveService().GetReceiveData(strartDate, endDate,  index, pageSize);
        }

        public dynamic GetTotalReceiveData(RequestWrapper query)
        {
            string strartDate = "";
            string endDate = "";
            string queryDate = query["ctime"];
           
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

            var ret = new ReceiveService().GetTotalReceiveData(strartDate, endDate);
           return ret;
        }//消耗明细查询


        public dynamic GetProjectNameReceivedt(RequestWrapper query)//工程消耗汇总查询
        {
            string strartDate = "";
            string endDate = "";
            int flag =Convert.ToInt32(query["flag"]);
            string queryDate = query["ctime"];
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
            int index = ZConvert.To<int>(query["page"]);
            int pageSize = ZConvert.To<int>(query["rows"]);

            return new ReceiveService().GetProjectNameReceivedtData(strartDate, endDate,flag, index, pageSize);
        }

        public dynamic GetTotalProjectNameReceivedt(RequestWrapper query)//工程消耗汇总查询
        {
            string strartDate = "";
            string endDate = "";
            int flag = Convert.ToInt32(query["flag"]);
            string queryDate = query["ctime"];
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

            var ret = new ReceiveService().GetTotalProjectNameReceivedtData(strartDate, endDate, flag);
            return ret;
          
        }

    }


}