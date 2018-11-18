using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class TransSurpl: ModelBase 
    {
        public string outTransp_id { get; set; }
        public string outCont_id { get; set; }
        public string 转出工程名称 { get; set; }
        public DateTime outCheckDatetime { get; set; }
        public string vehicle_id { get; set; }
        public string Driver { get; set; }
        public decimal trans_out_value { get; set; }
        public decimal Trans_in_value { get; set; }
        public string inCont_id { get; set; }
        public string 转入工程名称 { get; set; }
        public string inCheckDatetime { get; set; }
        public string inTransp_id { get; set; }
        public string type { get; set; }
   

    }
    public class TransSurplService : ServiceBase<TransSurpl>
    {
        public TransSurplService()
        {
            base.ModuleName = "Betonsys";
        }

        public List<TransSurpl> GetTransSurplData(string BegDayDate, string EndDatetime)
        {
            var strSql = String.Format(@"
                         select outTransp_id,outCont_id,转出工程名称,outCheckDatetime,vehicle_id,Driver,trans_out_value,
                          Trans_in_value,inCont_id,转入工程名称,inCheckDatetime,inTransp_id,type   from Betonsys..TransSurpl('{0}','{1}')  
                         ", BegDayDate, EndDatetime + " 23:59:59");

            return db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql).QueryMany<TransSurpl>();
        }


        public dynamic GetTotalTransSurplData(string BegDayDate, string EndDatetime)
        {

            var strSql = String.Format(@"
                         select sum(isnull(trans_out_value,0)) as trans_out_value, sum(isnull(Trans_in_value,0)) as Trans_in_value  from Betonsys..TransSurpl('{0}','{1}')  
                         ", BegDayDate, EndDatetime + " 23:59:59");

            return db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql).QueryMany<TransSurpl>();

        }



    }

}