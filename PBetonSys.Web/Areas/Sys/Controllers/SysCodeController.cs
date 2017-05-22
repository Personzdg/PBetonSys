using PBetonSys.Web.Areas.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Sys.Controllers
{
    public class SysCodeController : Controller
    {

	}

    public class SysCodeApiController : ApiController
    {
        public dynamic GetPlaceList()
        {
            return new SysCodeService().GetSysCode("Place");
        }
        public dynamic GetStrongList()
        {
            return new SysCodeService().GetSysCode("Strong");
        }
        public dynamic GetFallList()
        {
            return new SysCodeService().GetSysCode("Fall");
        }
        public dynamic GetPumpTypeList()
        {
            return new SysCodeService().GetSysCode("pumpType");
        }
        public dynamic GetHouseList()
        {
            return new SysCodeService().GetSysCode("House_id");
        }
    }
}