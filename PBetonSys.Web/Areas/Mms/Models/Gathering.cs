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

        public void AddDetail(string gathering_ID,decimal receiveMoney)
        {
            bool isExists=db.Sql(string.Format("select 1 from Gathering_Detail where Gathering_ID = '{0}'",gathering_ID)).QuerySingle<int>()>0?true:false;
            //Decimal.TryParse(receiveMoney, out dReceiveMoney);
            if (!isExists&& receiveMoney > 0)
            {
                db.Sql(string.Format("INSERT INTO [dbo].[Gathering_Detail]([Gathering_ID],[CheckDateTime],[ReceiveMoney],[CheckFlag]) VALUES('{0}','{1}',{2},0)", gathering_ID, DateTime.Now, receiveMoney)).Execute();
                //db.Insert("Gathering_Detail").Column("Gathering_ID", gathering_ID).Column("CheckDateTime", DateTime.Now).Column("ReceiveMoney", receiveMoney).Column("CheckFlag", 0);
            }
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