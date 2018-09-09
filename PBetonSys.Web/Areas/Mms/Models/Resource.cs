using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Resource : ModelBase
    {
    }

    public class ResourceService : ServiceBase<Resource>
    {
        public ResourceService()
        {
            base.ModuleName = "Material";
        }
    }
}