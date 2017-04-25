using Newtonsoft.Json.Linq;
using PBetonSys.Core;
using PBetonSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Sys.Models
{
    public class ClerkService : ServiceBase<Clerk>
    {
        public object Login(JObject request)
        {
            var Name = request.Value<string>("usercode");
            var Password = request.Value<string>("password");

            //用户名密码检查
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Password))
                return new { status = "error", message = "用户名或密码不能为空！" };

            //用户名密码验证
            var result = this.GetModel(ParamQuery.Instance()
                            .AndWhere("Name", Name)
                            .AndWhere("Password", Password)
                            .AndWhere("State", true));

            if (result == null || String.IsNullOrEmpty(result.ClerkID))
                return new { status = "error", message = "用户名或密码不正确！" };

            //调用框架中的登陆机制
            var loginer = new LoginerBase { UserCode = result.ClerkID, UserName = result.Name };

           // var effectiveHours = ZConfig.GetConfigInt("LoginEffectiveHours");
            FormsAuth.SignIn(loginer.UserCode, loginer, 60 * 24);


            //返回登陆成功
            return new { status = "success", message = "登陆成功！" };
        }
    

        public bool AuthorizeUserMenu(List<string> urls)
        {
            var UserCode = FormsAuth.GetUserData().UserCode;
            var result = db.Sql(string.Format(@"
select 1
from sys_roleMenuMap A
left join sys_userRoleMap B on B.RoleCode = A.RoleCode
left join sys_menu C on C.MenuCode = A.MenuCode
where B.UserCode = '{1}'
and C.URL in ('{0}')", string.Join("','", urls), UserCode)).QueryMany<int>();

            return result.Count > 0;
        }


        public Dictionary<string, object> GetDefaultUserSetttins()
        {
            var defaults = new Dictionary<string, object>();
            defaults.Add("theme", "default");
            defaults.Add("navigation", "accordion");
            defaults.Add("gridrows", "20");
            return defaults;
        }

        public Dictionary<string, object> GetCurrentUserSettings()
        {
            var result = new Dictionary<string, object>();
            var UserCode = FormsAuth.GetUserData<LoginerBase>().UserCode;
            //var config = db.Sql("select ConfigJSON from sys_user where UserCode=@0", UserCode).QuerySingle<string>();
            var settings = db.Sql("select * from sys_userSetting where UserCode=@0", UserCode).QueryMany<sys_userSetting>();

            foreach (var item in settings)
                result.Add(item.SettingCode, item.SettingValue);

            var defaults = GetDefaultUserSetttins();

            foreach (var item in defaults)
                if (!result.ContainsKey(item.Key)) result.Add(item.Key, item.Value);

            return result;
        }

        public void SaveCurrentUserSettings(JObject settings)
        {
            var UserCode = FormsAuth.GetUserData<LoginerBase>().UserCode;
            foreach (JProperty item in settings.Children())
            {
                var result = db.Update("sys_userSetting")
                    .Column("SettingValue", item.Value.ToString())
                    .Where("UserCode", UserCode)
                    .Where("SettingCode", item.Name)
                    .Execute();

                if (result <= 0)
                {
                    var model = new sys_userSetting();
                    model.UserCode = UserCode;
                    model.SettingCode = item.Name;
                    model.SettingValue = item.Value.ToString();
                    db.Insert<sys_userSetting>("sys_userSetting", model).AutoMap(x => x.ID).Execute();
                }
            }
        }

        public List<dynamic> GetRoleMembers(string role)
        {
            var result = db.Sql(String.Format(@"
 select A1.Name as MemberName ,A1.ClerkID as MemberCode,'user' as MemberType
  from Clerk A1
 where A1.ClerkID in (select B1.UserCode from sys_userRoleMap B1 where B1.RoleCode = '{0}')
union
 select A2.OrganizeName as MemberName ,A2.OrganizeCode as MemberCode,'organize' as MemberType
   from sys_organize A2
  where A2.OrganizeCode in (select B2.OrganizeCode from sys_organizeRoleMap B2 where B2.RoleCode = '{0}')", role)).QueryMany<dynamic>();
            return result;
        }

        public dynamic GetUserOrganize(string user)
        {
            var sql = String.Format(@"
select distinct A.OrganizeCode,A.OrganizeName,A.ParentCode
,(case when B.UserCode is null then 'false' else 'true' end) as Checked
from sys_organize A
left join sys_userOrganizeMap B on B.OrganizeCode = A.OrganizeCode and B.UserCode = '{0}'", user);
            return db.Sql(sql).QueryMany<dynamic>();
        }

        public void SaveUserOrganizes(string ClerkID, JToken OrganizeList)
        {
            db.UseTransaction(true);
            Logger("设置用户机构", () =>
            {
                db.Delete("sys_userOrganizeMap").Where("UserCode", ClerkID).Execute();
                foreach (JToken item in OrganizeList.Children())
                {
                    var OrganizeCode = item["OrganizeCode"].ToString();
                    db.Insert("sys_userOrganizeMap").Column("UserCode", ClerkID).Column("OrganizeCode", OrganizeCode).Execute();
                }
                db.Commit();
            }, e => db.Rollback());
        }

        public dynamic GetUserRole(string user)
        {
            var sql = String.Format(@"
select distinct A.RoleCode,A.RoleName
,(case when B.RoleCode is null then 'false' else 'true' end) as Checked
from sys_role A
left join sys_userRoleMap B on B.RoleCode = A.RoleCode and B.UserCode = '{0}'", user);
            return db.Sql(sql).QueryMany<dynamic>();
        }

        public void SaveUserRoles(string ClerkID, JToken RoleList)
        {
            db.UseTransaction(true);
            Logger("设置用户角色", () =>
            {
                db.Delete("sys_userRoleMap").Where("UserCode", ClerkID).Execute();
                foreach (JToken item in RoleList.Children())
                {
                    var RoleCode = item["RoleCode"].ToString();
                    db.Insert("sys_userRoleMap").Column("UserCode", ClerkID).Column("RoleCode", RoleCode).Execute();
                }
                db.Commit();
            }, e => db.Rollback());
        }

        public int ResetUserPassword(string ClerkID)
        {
            var defaultPassword = "1234";
            var result = db.Update("Clerk")
                .Column("Password", defaultPassword)
                .Where("ClerkID", ClerkID)
                .Execute();
            return result;
        }

        protected override void OnAfterEditDetail(EditEventArgs arg)
        {
            if (arg.type == OptType.Add)
            {
                ResetUserPassword(arg.row["ClerkID"].ToString());
            }
            base.OnAfterEditDetail(arg);
        }
    }

    public class Clerk : ModelBase
    {

        [PrimaryKey]
        public string ClerkID
        {
            get;
            set;
        }

        public string GroupID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool? State
        {
            get;
            set;
        }

        public string department
        {
            get;
            set;
        }

        public string ConfigJSON
        {
            get;
            set;
        }
        public int OverTimeNumber
        {
            get;
            set;
        }
        public bool? Net
        {
            get;
            set;
        }

        public Int16 UserType
        {
            get;
            set;
        }


        public string SysCont_ID
        {
            get;
            set;
        }

        public string LinkPhon
        {
            get;
            set;
        }

    }
}