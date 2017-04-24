using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Sys.Models
{
    public class sys_parameterService : ServiceBase<sys_parameter>
    {

    }

    public class sys_parameter : ModelBase
    {

        [PrimaryKey]
        public string ParamCode
        {
            get;
            set;
        }

        public string ParamValue
        {
            get;
            set;
        }

        public bool? IsUserEditable
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string CreatePerson
        {
            get;
            set;
        }

        public DateTime? CreateDate
        {
            get;
            set;
        }

        public string UpdatePerson
        {
            get;
            set;
        }

        public DateTime? UpdateDate
        {
            get;
            set;
        }

    }
}