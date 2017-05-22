﻿using PBetonSys.Core;
using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class ClinetController : Controller
    {
        //This is A Test
        // GET: /Mms/Clinet/
        public ActionResult Index()
        {
            return View();
        }
    }
    public class ClinetApiController : ApiController
    {
        public dynamic GetClientList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                       [Clinet_id] ,[Name],[CheckDateTime],[State],[Remark],[SimpleName],[ClerkID],[LinkName],[LinkPhon],[WXCode],[Password]
                    </select>
                    <from>
                        Clinet
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ClientService().GetDynamicListWithPaging(pQuery);
            return result;
        }

        public dynamic GetComboClinet()
        {
            RequestWrapper query = new RequestWrapper();
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                        [Clinet_id] ,[Name]
                    </select>
                    <from>
                        Clinet
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ClientService().GetDynamicList(pQuery);
            return result;
        }

        public string GetNewCode()
        {
            var service = new ClientService();
            return service.GetNewKey("Clinet_id", "maxplus").PadLeft(3, '0');
        }

        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
            <settings>
                <table>
                    Clinet
                </table>
                <where>
                    <field name='Clinet_id' cp='equal' variable='Clinet_id'></field>
                </where>
            </settings>");
            var service = new ClientService();
            var result = service.Edit(null, listWrapper, data);
        }
    }
}
