using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PBetonSys.Web.Areas.Mms.Models
{
    public class GatheringService : ServiceBase<Gathering>
    {

        public GatheringService()
        {
            base.ModuleName = "Settlement";
        }

        public string GetNewCode()
        {
            var produce = db.StoredProcedure("GetGatheringID");
            produce.ParameterOut("Number", DataTypes.String, 13);
            produce.Execute();
            return produce.ParameterValue<string>("Number");
        }


    }



    public class Gathering : ModelBase 
    {
        [PrimaryKey]
        public string Gathering_ID { get; set; }

        public string SysCont_id { get; set; }
        public DateTime CheckDateTime { get; set; }
        public decimal? ReceiveMoney { get; set; }
        public decimal? Other { get; set; }
        public string ReceiveType { get; set; }
        public bool? AffirmFlag { get; set; }
        public string ClerkName { get; set; }
        public string Remark { get; set; }
        public string SalseName { get; set; }
        public DateTime AffirmDateTime { get; set; }
        public string AffirmName { get; set; }
        public string GatheringPerson { get; set; }
        public string Bank { get; set; }
        public string Accounts { get; set; }
        public string AcCode { get; set; }
        public int? delay { get; set; }
    }
}