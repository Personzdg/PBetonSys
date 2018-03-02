using PBetonSys.Core;
using PBetonSys.Web.App_Start.webstack;
using PBetonSys.Web.Areas.Mms.Models;
using PBetonSys.Web.Areas.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class S_ConfectController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [MvcMenuFilter(false)]
        public ActionResult LookupS_Confect()
        {
            return View();
        }

    }

    public class S_ConfectApiController : MmsBaseApi<S_Confect, S_ConfectService>
    {
        public dynamic GetS_ConfectList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.ID'>
                    <select>
                       a.inside_id,a.Strong,a.Pump, a.Fall ,a.State 
                    </select>
                    <from>
                       S_Confect as a 
                    </from>
                    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
                        <field name='a.Strong'       cp='startwith'  ></field>
                        <field name='a.Fall'       cp='like'   ></field>
                        <field name='a.Pump'       cp='like'   ></field>
                    </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            pQuery.GroupBy("a.inside_id,Strong,State,Fall,Pump,a.ID");
            var result = masterService.GetDynamicListWithPaging(pQuery);
            return result;
        }




        public dynamic GetLookupS_Confect(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.id'>
                    <select>
                        a.*
                    </select>
                    <from>
                        S_Confect as a
                       
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='a.inside_id'       cp='startwith'  ></field>
   
  </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicListWithPaging(pQuery);
            return result;
        }




    }

}