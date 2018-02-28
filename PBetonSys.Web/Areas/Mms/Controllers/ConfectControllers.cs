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
    public class ConfectController : Controller
    {

        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    Confect_ID = "",
                    ProjectName = "",
                    CheckDateTime = ""
                }
            };
            return View(model);
        }

    }

    public class ConfectApiController : ApiController 
    {

        public dynamic GetConfectList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                        a.* ,b.ProjectName
                    </select>
                    <from>
                        Confect as a
                        left join Contract as b on a.Cont_ID=b.Cont_ID
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='a.Confect_ID'       cp='startwith'  ></field>
    <field name='b.ProjectName'       cp='like'   ></field>
    <field name='a.Provide_DateTime'          cp='daterange'  ></field>
  </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new TaskService().GetDynamicListWithPaging(pQuery);
            return result;
        }


        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                        <table>
                            Confect
                        </table>
                        <where>
                            <field name='Confect_ID' cp='equal' variable='Confect_ID'></field>
                        </where>
                    </settings>");

            var service = new TaskService();
            var result = service.Edit(null, listWrapper, data);

        }
    
     }


}