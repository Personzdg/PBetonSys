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
    public class TaskController : Controller
    {
        //
        // GET: /Mms/Task/
        public ActionResult Index()
        {
            return View();
        }
	}

    public class TaskApiController : ApiController
    {
        public dynamic GetTaskList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                       [Task_id],[House_id],[Cont_ID],[Clin_ID],[Place],[Strong],[Fall],[Infiltrate] ,[Amount] ,[Pump],[KanZhe],[Pump_vehicle],[LinkName],[Telephon],[ViseName],[Auditing],[CheckDateTime],[Provide_DateTime] ,[Remark],[State],[Wjj],[ShowFlag],[ContractUnit] ,[TempProject],[Task_inside_code],[AuditingFlag],[NetID],[Type]
                    </select>
                    <from>
                        Task
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new TaskService().GetDynamicListWithPaging(pQuery);
            return result;
        }
    }
}