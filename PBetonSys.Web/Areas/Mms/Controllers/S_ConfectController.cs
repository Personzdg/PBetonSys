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
                <settings defaultOrderBy='a.inside_id'>
                    <select>
                       a.inside_id,a.Strong,a.Pump, a.Fall
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
            pQuery.GroupBy("a.inside_id,Strong,Fall,Pump");
            var result = masterService.GetDynamicList(pQuery);
            return result;
        }




        public dynamic GetLookupS_Confect(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='inside_id'>
                    <select>
                       MT_Size,Theory_Value,silo
                    </select>
                    <from>
                        S_Confect
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='inside_id'       cp='equal'  ></field>
   
  </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicList(pQuery);
            return result;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Inside_ID"></param>
        /// <param name="HousID"></param>
        /// <returns></returns>
        public dynamic GetLookupS_ConfectDetail(string Inside_ID, string HousID)
        {
            var pQuery = ParamQuery.Instance().Select("Inside_ID,Hous1Name,Hous2Name,Hous3Name,Hous4Name,MT_Size,Theory_Value,Tol,T1,MTType,MTSize,Provide_ID,MTCode,ZBCode,MT_Value ,Sequence,Range_To,range_From,NewMin,NewMax,Theory_Value,MT_Value,Ratio").From(string.Format(" LdS_confectToConfDetil('{0}','{1}')", Inside_ID, HousID));
            pQuery.ClearWhere();
            var result = masterService.GetDynamicList(pQuery);
            return result;
        }


    }

}