using PBetonSys.Core;
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
    public class ContractController : Controller
    {
        //
        // GET: /Mms/Contract/
        public ActionResult Index()
        {
            var model = new
            {
                urls = "/mms/Contract/edit",
                form = new
                {
                    Cont_ID = "",
                    SalseName=""
                }
            };
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var data = new ContractApiController().GetEditMaster(id);
            var finshFlagList = new List<dynamic>();
            finshFlagList.Add(new { text = "否", value = "0" });
            finshFlagList.Add(new { text = "是", value = "1" });
            var model = new
            {
                form = data.form,
                urls = "/api/mms/Contract/edit/",
                dataSource = new
                {
                    FinshFlagList = finshFlagList,
                    clerkList = new ClerkService().GetDynamicList(ParamQuery.Instance().Select("Name as value,Name as text")),
                    constructionList = new ClientService().GetDynamicList(ParamQuery.Instance().Select("Clinet_id as value,Name as text"))    
                },
                //defaultForm = new mms_receive().Extend(new
                //{
                //    BillNo = id,
                //    BillDate = DateTime.Now,
                //    DoPerson = userName,
                //    ReceiveDate = DateTime.Now,
                //    SupplyType = codeService.GetDefaultCode("SupplyType"),
                //    PayKind = codeService.GetDefaultCode("PayType"),
                //}),
                //defaultRow = new
                //{
                //    CheckNum = 1,
                //    Num = 1,
                //    UnitPrice = 0,
                //    Money = 0,
                //    PrePay = 0
                //},
                setting = new
                {
                    postFormKeys = new string[] { "SysCont_ID" },
                    postListFields = new string[] { "SysCont_ID", "Cont_ID", "ProjectName", "ProjectAddr", "Interva", "Clinet_id", "CheckDateTime", "SalseName", "BossName", "SalseRecevice", "LinkPhon", "MobilePhon", "Strong", "Amount", "Price", "FinshFlag", "FinShDateTime", "EndPaymentDatetime", "paymentType", "GatheringRatio", "Remark", "StatDate", "GatheringDate", "EndPaymentMonth", "ContType", "SimpleName", "LinkName", "WXCode", "Password" }
                }
            };
            return View(model);
        }
	}

    public class ContractApiController : MmsBaseApi<Contract, ContractService>
    {
        public dynamic GetContractList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                   [SysCont_ID]
                  ,[Cont_ID]
                  ,[ProjectName]
                  ,[ProjectAddr]
                  ,[Interva]
                  ,[Clinet_id]
                  ,[CheckDateTime]
                  ,[SalseName]
                  ,[BossName]
                  ,[SalseRecevice]
                  ,[LinkPhon]
                  ,[MobilePhon]
                  ,[Strong]
                  ,[Amount]
                  ,[Price]
                  ,[FinshFlag]
                  ,[FinShDateTime]
                  ,[EndPaymentDatetime]
                  ,[paymentType]
                  ,[GatheringRatio]
                  ,[Remark]
                  ,[StatDate]
                  ,[GatheringDate]
                  ,[EndPaymentMonth]
                  ,[ContType]
                  ,[SimpleName]
                  ,[LinkName]
                  ,[WXCode]
                  ,[Password]
                    </select>
                    <from>
                        Contract
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicListWithPaging(pQuery);
            return result;
        }

        public string GetNewCode()
        {
            var service = new ContractService();
            return service.GetNewKey("SysCont_ID", "maxplus").PadLeft(3, '0');
        }

//        public ActionResult Edit(dynamic data)
//        {
//            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
//            <settings>
//                <table>
//                    Clinet
//                </table>
//                <where>
//                    <field name='SysCont_ID' cp='equal' variable='SysCont_ID'></field>
//                </where>
//            </settings>");
//            var service = new ContractService();
//            var result = service.Edit(null, listWrapper, data);
//        }

       
    }
}