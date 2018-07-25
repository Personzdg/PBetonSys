using Newtonsoft.Json.Linq;
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
     public class GatheringController : Controller
     {
         // GET: /Mms/Gathering/
         public ActionResult Index()
         {
             var model = new
             {
                 form = new
                 {
                     Gathering_ID = "",
                     ProjectName = "",
                     CheckDateTime = ""
                 }
             };
             return View(model);
         }
         /// 收款确认
         /// </summary>
         public ActionResult Affirm()
         {
             var model = new
             {
                 form = new
                 {
                     Gathering_ID = "",
                     ProjectName = "",
                     CheckDateTime = ""
                 }
             };
             return View(model);
         }
//////////////////////////////////////
         public ActionResult AffirmDetail(string id= "")
         {
             var model = new
             {
                 form = new
                 {
                     Gathering_ID = id
                 }
             };
             return View(model);
         }
         public ActionResult GatheringList()
         {
             return View();
         }


    }

     public class GatheringApiController : ApiController
     {
         public dynamic GetGatheringList(RequestWrapper query)
         {
             query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                       a.Gathering_ID, a.SysCont_id, a.CheckDateTime, a.ReceiveMoney, a.Other, a.ReceiveType, a.AffirmFlag, a.ClerkName,  a.Remark, a.SalseName, a.GatheringPerson, a.Bank, a.Accounts, a.AcCode,a.AffirmDateTime,b.ProjectName,c.Name,c.Clinet_id  
                    </select>
                    <from>
                        Gathering as a join Contract as b on (a.Syscont_id=b.SysCont_id) join Clinet  as c on (b.Clinet_id=c.Clinet_id)
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='a.Gathering_ID'       cp='startwith'  ></field>
    <field name='b.ProjectName'       cp='like'   ></field>
    <field name='a.CheckDateTime'          cp='daterange'  ></field>
  </where>
                </settings>");
             var pQuery = query.ToParamQuery();
             var result = new GatheringService().GetDynamicListWithPaging(pQuery);
             return result;
         }

         public dynamic GetTotalReceiveMoney(RequestWrapper query)
         {
             query.LoadSettingXmlString(@"
            <settings>
                <select>
                   SUM(drm.ReceiveMoney) ReceiveMoney ,  SUM(drm.Other) Other
                </select>
                <from>
                    Gathering drm
                </from>
                <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
                    <field name='drm.CheckDateTime'                cp='daterange'      ></field>
                </where>
            </settings>");
             var pQuery = query.ToParamQuery();
             var result = GatheringService.Instance().GetField<dynamic>(pQuery);
             return result;
         }
///////////////////////////
         public dynamic GetTotalOther(RequestWrapper query)
         {
             query.LoadSettingXmlString(@"
            <settings>
                <select>
                   SUM(drm.Other) Other
                </select>
                <from>
                    Gathering drm
                </from>
                <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
                    <field name='drm.CheckDateTime'                cp='daterange'      ></field>
                </where>
            </settings>");
             var pQuery = query.ToParamQuery();
             var result = GatheringService.Instance().GetField<string>(pQuery);
             return result;
         }

  /////////////////

         [System.Web.Http.HttpPost]
         public void Edit(dynamic data)
         {
             var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                        <table>
                            Gathering
                        </table>
                        <where>
                            <field name='Gathering_ID' cp='equal' variable='Gathering_ID'></field>
                        </where>
                    </settings>");

//            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
//                    <settings>
//                        <table>
//                            Gathering_Detail
//                        </table>
//                        <where>
//                            <field name='Gathering_ID' cp='equal' variable='Gathering_ID'></field>
//                        </where>
//                    </settings>");

            var service = new GatheringService();
            //var fromId = data.form.Gathering_ID.Value;
            //ParamDelete pd = ParamDelete.Instance().From("Gathering_Detail").AndWhere("Gathering_ID", fromId); //删除之前的记录
            service.AddDetail(data.form.Gathering_ID.Value.ToString(), data.form.ReceiveMoney.Value.ToString());
            var result = service.Edit(formWrapper, null, data);

         }


//////////////////////
         public dynamic GetGatheringDetaiList(string Gathering_ID)
         {
             var query = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                          <select>
                             ID,Gathering_ID, CheckDateTime, ReceiveMoney, Remark, CheckFlag
                        </select>
                        <from>
                            Gathering_Detail
                       </from>
                    </settings>");
             var pQuery = query.ToParamQuery();
             pQuery.AndWhere("Gathering_ID", Gathering_ID);
             return new GatheringService().GetDynamicList(pQuery);
         }


         public string GetNewCode()
         {
             var service = new GatheringService();
             return service.GetNewCode();
         }


         [System.Web.Http.HttpPost]
         public void EditDetail(dynamic data)
         {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                                 <settings>
                                     <table>
                                         Gathering_Detail
                                     </table>
                                     <where>
                                         <field name='ID' cp='equal' variable='ID'></field>
                                     </where>
                                 </settings>");

             var service = new GatheringService();

             if (data["list"] != null && listWrapper != null)
             {
                 foreach (JProperty item in data["list"].Children())
                 {
                     if (item.Name == "inserted") 
                     {
                         foreach (var row in item.Value.Children())
                         {
                             row["CheckDateTime"] = DateTime.Now;
                         }
                     }
                 }
             }
             var gathering_ID = data.Gathering_ID.Value;
             //ParamDelete pd = ParamDelete.Instance().From("Gathering_Detail").AndWhere("Gathering_ID", fromId); //删除之前的记录
             var result = service.Edit(null, listWrapper, data);
             service.ReSetReceiveMoney(gathering_ID);

         }
     }



}