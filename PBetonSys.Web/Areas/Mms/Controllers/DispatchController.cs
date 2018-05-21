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
    public class DispatchController : Controller
    {
        //
        // GET: /Mms/Dispatch/
        public ActionResult Index()
        {
            var model = new
            {
                dataSource = new 
                {
                    houseItems= new DispatchService().GetHouseList(),
                    carItems=new DispatchService().GetCarList(),
                    driveItems = new DispatchService().GetDriveList(),
                    pumpItems = new DispatchService().GetPumpList()
                }
            };
            return View(model);
        }
	}

    public class DispatchApiController : ApiController
    {
        public dynamic GetDispatchList()
        {
            var result = new DispatchService().GetDispatchList();
            return result;
        }

        public dynamic GetHouseList()
        {
            return new DispatchService().GetHouseList();
        }

        public dynamic GetDriveList()
        {
            return new DispatchService().GetDriveList();
        }

        /// <summary>
        /// 根据合同号获取运输单列表
        /// </summary>
        /// <param name="contId">合同号</param>
        /// <returns></returns>
        public dynamic GetTransportByContId(string contId)
        {
            return new DispatchService().GetTransportByContId(contId);

        }

       /// <summary>
        /// 获取任务编号
       /// </summary>
       /// <param name="hous_id"></param>
       /// <param name="task_ID"></param>
       /// <returns></returns>
        public dynamic GetTranspID(string hous_id, string task_ID)
        {
            return new DispatchService().GetTranspID(hous_id, task_ID);
        }


        /// <summary>
        /// 编辑调度列表
        /// </summary>
        /// <param name="data"></param>
        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                        <table>
                            Transport
                        </table>
                        <where>
                            <field name='Transp_ID' cp='equal' variable='Transp_ID'></field>
                        </where>
                    </settings>");

            var service = new DispatchService();
            var result = service.Edit(null, listWrapper, data);
        }
    }
}