using PBetonSys.Core;
using PBetonSys.Web.Areas.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Sys.Controllers
{
    public class CodeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

    public class CodeApiController : ApiController
    {

        public dynamic GetCodeType(RequestWrapper request)
        {
            request.LoadSettingXmlString(@"
<settings defaultOrderBy='Seq,CodeType'>
   <where defaultIgnoreEmpty='true'>
        <field name='CodeType'      cp='equal'></field>
        <field name='CodeTypeName'  cp='like' ></field>
    </where>
</settings>
");
            var result = new sys_codeTypeService().GetDynamicListWithPaging(request.ToParamQuery());
            return result;
        }

        public dynamic Get(RequestWrapper request)
        {
            request.LoadSettingXmlString(@"
<settings defaultOrderBy='Seq'>
   <where>
        <field name='CodeType' cp='equal' ignoreEmpty='true'></field>
    </where>
</settings>");
            var service = new sys_codeService();
            var result = service.GetModelListWithPaging(request.ToParamQuery());
            return result;
        }

        public string GetNewCode()
        {
            var service = new sys_codeService();
            return service.GetNewKey("Code", "maxplus").PadLeft(3, '0');
        }


        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>sys_code</table>
    <where>
        <field name='Code' cp='equal'></field>
    </where>
</settings>");
            var service = new sys_codeService();
            var result = service.Edit(null, listWrapper, data);
        }

        [System.Web.Http.HttpPost]
        public void EditCodeType(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>sys_codeType</table>
    <where>
        <field name='CodeType' cp='equal'></field>
    </where>
</settings>");
            var service = new sys_codeTypeService();
            var result = service.Edit(null, listWrapper, data);
        }
    }
}