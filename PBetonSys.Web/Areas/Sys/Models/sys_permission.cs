using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Sys.Models
{
    public class sys_permissionService : ServiceBase<sys_permission>
    {

    }

    public class sys_permission : ModelBase
    {

        [PrimaryKey]
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string ParentCode { get; set; }
        public string CreatePerson { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdatePerson { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}