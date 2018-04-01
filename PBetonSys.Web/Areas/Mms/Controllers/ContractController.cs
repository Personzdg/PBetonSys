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
                    ProjectName="",
                    SalseName = "",
                    CheckDateTime=""
                }
            };
            return View(model);
        }

        [MvcMenuFilter(false)]
        public ActionResult LookupContract()
        {
            return View();
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
                defaultForm = new
                {
                    //ClientName="",
                    Clinet_id="",
                    FinshFlag=false,
                    ProjectName="",
                    ProjectAddr="",
                    SimpleName="",
                    SalseRecevice="",
                    SalseName="",
                    CheckDateTime="",
                    BossName="",
                    LinkPhon="",
                    LinkName = "",
                    Password="",
                    WXCode="",
                    Interva="",
                    Amount="",
                    FinShDateTime="",
                    Strong="",
                    Price="",
                    StatDate="",
                    paymentType="",
                    GatheringRatio="",
                    GatheringDate="",
                    EndPaymentDatetime="",
                    EndPaymentMonth="",
                    ContType="",
                    Remark=""
                },
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
                <settings defaultOrderBy='a.CheckDateTime'>
                    <select>
                        a.*,b.Name as ClientName
                    </select>
                    <from>
                        Contract as a
                        left join Clinet as b on a.Clinet_id=b.Clinet_id
                    </from>
                    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
                        <field name='a.Cont_ID'       cp='startwith'  ></field>
                        <field name='a.ProjectName'       cp='like'   ></field>
                        <field name='a.SalseName'       cp='like'   ></field>
                        <field name='a.CheckDateTime'          cp='daterange'  ></field>
                    </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicListWithPaging(pQuery);
            return result;
        }

        public override dynamic GetEditMaster(string id)
        {
            var query = RequestWrapper
    .InstanceFromRequest()
    .SetRequestData("SysCont_ID", id)
    .LoadSettingXmlString(@"
                <settings defaultOrderBy='a.CheckDateTime'>
                    <select>
                        a.*,b.Name as ClientName
                    </select>
                    <from>
                        Contract as a
                        left join Clinet as b on a.Clinet_id=b.Clinet_id
                    </from>
                    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
                        <field name='a.SysCont_ID' cp='equal'  ></field>
                    </where>
                </settings>");

            var pQuery = query.ToParamQuery();
            var result = new { form = masterService.GetDynamic(pQuery) };
            return result;
        }

        public dynamic GetComboContract()
        {
            RequestWrapper query = new RequestWrapper();
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.CheckDateTime'>
                    <select>
                       a.SysCont_ID as Cont_ID ,a.ProjectName,a.Clinet_id,b.Name
                    </select>
                    <from>
                        Contract as a
                        inner join Clinet as b on a.Clinet_id=b.Clinet_id
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicList(pQuery);
            return result;
        }

        public dynamic GetLookupContract(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.CheckDateTime'>
                    <select>
                         a.SysCont_ID as  Cont_ID,a.ProjectName,a.Clinet_id,b.Name
                    </select>
                    <from>
                        Contract as a
                        inner join Clinet as b on a.Clinet_id=b.Clinet_id
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='a.Cont_ID'       cp='startwith'  ></field>
    <field name='a.ProjectName'       cp='like'       ></field>
  </where>
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