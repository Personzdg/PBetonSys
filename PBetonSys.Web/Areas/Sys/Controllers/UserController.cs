using Newtonsoft.Json.Linq;
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
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var model = new
            {
                dataSource = new
                {
                    userType = new sys_codeService().GetValueTextListByType("UserType")
                }
            };
            return View(model);
        }
    }

    public class UserApiController : ApiController
    {
        public dynamic Get(RequestWrapper request)
        {
            request.LoadSettingXmlString(@"
<settings defaultOrderBy='ClerkID'>
   <where>
        <field name='ClerkID' cp='mapchild' extend='sys_userOrganizeMap,OrganizeCode,sys_organize' variable='OrganizeCode' ignoreEmpty='true'></field>
    </where>
</settings>");
            var service = new ClerkService();
            var result = service.GetModelListWithPaging(request.ToParamQuery());
            return result;
        }

        public dynamic GetSettingList(string id)
        {
            var pQuery = ParamQuery.Instance().AndWhere("UserCode", id);
            var service = new sys_userSettingService();
            return service.GetModelList(pQuery);
        }

        public dynamic GetOrganizeWithUserCheck(string id)
        {
            var service = new ClerkService();
            return service.GetUserOrganize(id);
        }

        public dynamic GetRoleWithUserCheck(string id)
        {
            var service = new ClerkService();
            return service.GetUserRole(id);
        }

        [System.Web.Http.HttpPost]
        public int PostResetPassword(string id)
        {
            var service = new ClerkService();
            return service.ResetUserPassword(id);
        }

        [System.Web.Http.HttpPost]
        public int PostModifyPwd(string oPwd, string nPwd)
        {
            var loginUser = FormsAuth.GetUserData();
            return new ClerkService().ModifyUserPwd(loginUser.UserCode, oPwd, nPwd);
        }

        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>Clerk</table>
    <where>
        <field name='ClerkID' cp='equal'></field>
    </where>
</settings>");
            var service = new ClerkService();
            var result = service.Edit(null, listWrapper, data);
        }

        [System.Web.Http.HttpPost]
        public void EditUserOrganizes(string id, dynamic data)
        {
            var service = new ClerkService();
            service.SaveUserOrganizes(id, data as JToken);
        }

        [System.Web.Http.HttpPost]
        public void EditUserRoles(string id, dynamic data)
        {
            var service = new ClerkService();
            service.SaveUserRoles(id, data as JToken);
        }

        [System.Web.Http.HttpPost]
        public void EditUserSetting(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>sys_userSetting</table>
    <where>
        <field name='ID' cp='equal'></field>
    </where>
</settings>");
            var service = new sys_userSettingService();
            var result = service.Edit(null, listWrapper, data);
        }



    }
}