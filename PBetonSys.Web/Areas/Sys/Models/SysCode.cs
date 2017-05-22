using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Sys.Models
{
    [Module("Betonsys")]
    public class SysCodeService : ServiceBase<SysCode>
    {
        public dynamic GetSysCode(string codeType)
        {
            var sql = string.Format(@"select * from SysCode where CodeType='{0}'", codeType);
            var result = db.Sql(sql).QueryMany<dynamic>();
            return result;
        }
    }

    public class SysCode:ModelBase
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }
}