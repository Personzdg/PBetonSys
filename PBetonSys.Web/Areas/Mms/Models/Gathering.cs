using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace PBetonSys.Web.Areas.Mms.Models
{
    public class GatheringService : ServiceBase<Gathering>
    {

        public GatheringService()
        {
            base.ModuleName = "Settlement";
        }

        public void AddDetail(string gathering_ID,string receiveMoney)
        {
            bool isExists=db.Sql(string.Format("select 1 from Gathering_Detail where Gathering_ID = '{0}'",gathering_ID)).QuerySingle<int>()>0?true:false;
            decimal dReceiveMoney = 0;
            Decimal.TryParse(receiveMoney, out dReceiveMoney);
            if (!isExists && dReceiveMoney > 0)
            {
                db.Sql(string.Format("INSERT INTO [dbo].[Gathering_Detail]([Gathering_ID],[CheckDateTime],[ReceiveMoney],[CheckFlag]) VALUES('{0}','{1}',{2},0)", gathering_ID, DateTime.Now, dReceiveMoney)).Execute();
                //db.Insert("Gathering_Detail").Column("Gathering_ID", gathering_ID).Column("CheckDateTime", DateTime.Now).Column("ReceiveMoney", receiveMoney).Column("CheckFlag", 0);
            }
        }

        public void ReSetReceiveMoney(string gathering_ID) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update [dbo].Gathering");
            sb.Append(" set ReceiveMoney=(select sum(ReceiveMoney) from [dbo].Gathering_Detail where Gathering_ID='{0}')");
            sb.AppendFormat(" where Gathering_ID='{0}'", gathering_ID.Trim());
            db.Sql(sb.ToString()).Execute();
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