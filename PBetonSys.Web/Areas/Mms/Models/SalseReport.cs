using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class SalseReport : ModelBase
    {
    }
    public class SalseReportService : ServiceBase<SalseReport>
    {
        public SalseReportService()
        {
            base.ModuleName = "Settlement";
        }
    }
}