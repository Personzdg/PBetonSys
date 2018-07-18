using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Gathering_DetailService : ServiceBase<Gathering_Detail>
    {

        public Gathering_DetailService()
        {
            base.ModuleName = "Settlement";
        }

    }

    public class Gathering_Detail : ModelBase
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Gathering_ID { get; set; }
        public DateTime CheckDateTime { get; set; }
        public decimal? ReceiveMoney { get; set; }
        public string Remark { get; set; }
        public bool? CheckFlag { get; set; }

    }
}