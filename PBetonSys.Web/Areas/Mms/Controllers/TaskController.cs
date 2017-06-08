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
            var model = new
            {
                form = new
                {
                    Task_id = "",
                    ProjectName = "",
                    Provide_DateTime = ""
                }
            };
            return View(model);
        }
    }

    public class TaskApiController : ApiController
    {
        public dynamic GetTaskList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                        a.* ,b.ProjectName
                    </select>
                    <from>
                        Task as a
                        left join Contract as b on a.Cont_ID=b.Cont_ID
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='a.Task_id'       cp='startwith'  ></field>
    <field name='b.ProjectName'       cp='like'   ></field>
    <field name='a.Provide_DateTime'          cp='daterange'  ></field>
  </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new TaskService().GetDynamicListWithPaging(pQuery);
            return result;
        }

        public string GetNewCode()
        {
            var service = new TaskService();
            return service.GetNewCode();
        }

        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                        <table>
                            Task
                        </table>
                        <where>
                            <field name='Task_id' cp='equal' variable='Task_id'></field>
                        </where>
                    </settings>");

            var service = new TaskService();
            var result = service.Edit(null, listWrapper, data);

        }

    }
}