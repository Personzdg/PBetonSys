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
    public class SalseReportController : Controller
    {
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    DateTime = DateTime.Now.ToString("yyyy-MM-dd "),
                    ProjectName = "",
                    //MTType = "",
                }
            };
            return View(model);
        }


        public ActionResult PumpCartCostList(bool Pump=true)
        {
            var model = new
            {
                form = new
                {
                    DateTime = DateTime.Now.ToString("yyyy-MM-dd "),
                    //ProjectName = "",
                    Pump = Pump,
                    Pump_No=""
                 
                }
            };
            return View(model);
        }


        public ActionResult DriveLaborageCostList()
        {
            var model = new
            {
                form = new
                {
                    CheckDateTime = DateTime.Now.ToString("yyyy-MM-dd "),
                    CartID="",
                   ProjectName=""

                }
            };
            return View(model);
        }

    }

    public class SalseReportApiController : ApiController
    {
        public dynamic GetSalseReportList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.DateTime'>
                    <select>
                       a.DateTime,a.Strong,a.Price,isnull(a.Amount,0) as Amount , isnull(a.Money,0) as Money, isnull(a.OtherMoney,0) as OtherMoney,a.Pump_no,a.Pump,a.Place,a.WJj,a.Remark,b.ProjectName,b.SalseName,c.name 
                    </select>
                    <from>
                         settlement..SalseReport as a join settlement..Contract as b on (a.Syscont_id=b.SysCont_id) 
                         join settlement..Clinet as c on (b.Clinet_id=c.Clinet_id)
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                     <field name='b.ProjectName'       cp='like'   ></field>
                    <field name='a.DateTime'     cp='daterange' ></field>
            

          </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ResourceService().GetDynamicListWithPaging(pQuery);
            return result;
        }//销售日报

        public dynamic GetTotalSalseReportList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
               <settings>
                    <select>
                        sum(isnull(a.Amount,0)) as Amount  , sum(isnull(a.Money,0)) as Money, sum(isnull(OtherMoney,0)) as OtherMoney
                       
                    </select>
                    <from>
                         settlement..SalseReport as a join settlement..Contract as b on (a.Syscont_id=b.SysCont_id) 
                         join settlement..Clinet as c on (b.Clinet_id=c.Clinet_id)
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                    <field name='b.ProjectName'       cp='like'   ></field>
                    <field name='a.DateTime'     cp='daterange' ></field>
            

          </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ResourceService().GetDynamic(pQuery);

            return result;
        }//销售日报合计


        public dynamic GetPumpCartCostList(RequestWrapper query)///泵车费用列表
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.datetime'>
                    <select>
                             datetime ,  sum(isnull(amount,0)) as Amount ,b.Price ,(sum(isnull(amount,0))*b.Price) as Summoney  ,Pump_No 
                    </select>
                    <from>
                          settlement..SalseReport as a left join settlement..PumpCart as b on (a.Pump_No=b.PumpCartName)
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                     <field name='Pump_No'       cp='like'   ></field>
                     <field name='a.datetime'     cp='daterange' ></field>
                    <field name='Pump'     cp='equal' ></field>

                  

          </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            pQuery.GroupBy(" Pump_No, datetime ,b.Price");
            var result = new ResourceService().GetDynamicListWithPaging(pQuery);
            return result;
        }

        public dynamic GetTotalPumpCartCostList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
               <settings>
                    <select>
                          sum(isnull(amount,0)) as Amount  ,sum(isnull(amount,0)*b.Price) as Summoney 
                    </select>    
                   <from>
                          settlement..SalseReport as a left join settlement..PumpCart as b on (a.Pump_No=b.PumpCartName)
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                     <field name='Pump_No'       cp='like'   ></field>
                     <field name='a.datetime'     cp='daterange' ></field>
                    <field name='Pump'     cp='equal' ></field>
            

               </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ResourceService().GetDynamic(pQuery);

            return result;
        }///泵车费用列表合计

        public dynamic GetDriveLaborageCostList(RequestWrapper query)///搅拌车运费费用列表
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.CheckDatetime'>
                    <select>
                        b.ProjectName, a.KM,a.DriveName,a.CartID,a.CheckDateTime,a.Interva,a.Amount,a.Sum_Money,a.SjAmount,a.SJSum_money,a.Remark
                    </select>
                    <from>
                          settlement..DriveLaborage as a left join  settlement..Contract as b on (a.SysCont_id=b.SysCont_ID) 
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                     <field name='a.CartID'       cp='like'   ></field>
                     <field name='b.ProjectName'       cp='like'   ></field>
                     <field name='a.CheckDatetime'     cp='daterange' ></field>
          </where>


                </settings>");
            var pQuery = query.ToParamQuery();
           
            var result = new ResourceService().GetDynamicListWithPaging(pQuery);
            return result;
        }


        public dynamic GetTotalDriveLaborageCostList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
               <settings>
                    <select>
                          sum(isnull(a.Amount,0)) as Amount  ,sum(isnull(a.Sum_Money,0)) as Sum_Money,sum(isnull(a.SjAmount,0)) as SjAmount,sum(isnull(a.SJSum_money,0)) as SJSum_money 
                    </select>    
                   <from>
                           settlement..DriveLaborage as a left join  settlement..Contract as b on (a.SysCont_id=b.SysCont_ID) 
                    </from>
                    <where defaultForAll='false'  defaultIgnoreEmpty='true' >
                   <field name='a.CartID'       cp='like'   ></field>
                   <field name='b.ProjectName'       cp='like'   ></field>
                     <field name='a.CheckDatetime'     cp='daterange' ></field>
            

               </where>


                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ResourceService().GetDynamic(pQuery);

            return result;
        }///搅拌车费用列表合计


    }
}