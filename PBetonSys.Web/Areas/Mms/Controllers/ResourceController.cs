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
    public class ResourceController : Controller
    {
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    IndateTime =DateTime.Now.ToString("yyyy-MM-dd "),
                    name = "",
                    MTType="",
                }
            };
            return View(model);
        }

    }

    public class ResourceApiController : ApiController
    {
        public dynamic GetResourceList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.IndateTime'>
                    <select>
                        a.id,a.Provide_id,a.IndateTime,a.TypeName,a.Voiture_id ,a.silo_id,a.a_gross,a.a_empty,a.a_suttle,a.a_unit,a.a_price,a.a_money,
                        a.MTType,a.AffFlag,a.BackDatetime,a.a_suttleSJ ,b.name
                    </select>
                    <from>
                         resource as a 
                         join provide as b 
                         on (a.provide_id=b.pr_id)
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                     <field name='a.TypeName'       cp='like'   ></field>
                    <field name='a.MTType'           cp='like' ></field>   
                    <field name='a.IndateTime'     cp='daterange' ></field>
            

          </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ResourceService().GetDynamicListWithPaging(pQuery);
            return result;
        }

        public dynamic GetTotalResourceList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
               <settings>
                    <select>
                        sum(isnull(a.a_gross,0)) as a_gross  , sum(isnull(a.a_empty,0)) as a_empty , sum(isnull(a.a_suttle,0)) as a_suttle, sum(isnull(a_money,0)) as a_money
                       
                    </select>
                    <from>
                         resource as a 
                         join provide as b 
                         on (a.provide_id=b.pr_id)
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                     <field name='a.TypeName'       cp='like'   ></field>
                    <field name='a.MTType'           cp='like' ></field>   
                    <field name='a.IndateTime'     cp='daterange' ></field>
            

          </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ResourceService().GetDynamic(pQuery);
          
            return result;
        }
    }

}