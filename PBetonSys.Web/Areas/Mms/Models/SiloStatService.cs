using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace PBetonSys.Web.Areas.Mms.Models
{
    public class SiloStat: ModelBase 
    {
        public string Hous_id { get; set; }
        public string ProvideID { get; set; }
        public string ProvideName { get; set; }
        public string silo_id { get; set; }
        public decimal 昨日库存 { get; set; }
        public decimal 当日入库 { get; set; }
        public decimal 当日出库 { get; set; }
        public decimal 本日库存 { get; set; }
    }
    public class SiloStatService : ServiceBase<SiloStat>
    {
        public SiloStatService()
        {
            base.ModuleName = "Material";
        }


        public List<SiloStat> GetSiloStat(string BegDate)
        {
            //string BegDate = DateTime.Now.ToString("yyyy-mm-dd");
            //string EndDate = DateTime.Now.ToString("yyyy-mm-dd");
            string EndDate = BegDate;
            string EndDatetime =BegDate+" 23:59:59"; //DateTime.Now.ToString();
            
             string checkdatestrSql = string.Format("select max (Checkdate) as checkdate from CheckSilot");
            DateTime startDate = db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(checkdatestrSql).QuerySingle<DateTime>();

            string maxcheckdate = startDate.ToString("yyyy-MM-dd")+ " 23:59:59";
           //string maxcheckdate="2018-1-1";
            var strSql = String.Format(@"
                          select b.Hous_id,b.ProvideID,c.Name as ProvideName , a.silo_id,(a.期初值+(a.累计入库-a.当日入库)-(a.累计出库-a.当日出库)) as 昨日库存, a.当日入库,(a.当日出库) as 当日出库,
                          (a.期初值+a.累计入库-a.累计出库)as 本日库存  from  SiloStat('{0}','{1}','{2}','{3}')
                           as a join silot as b on (a.silo_id=b.siloName) join Provide as c on (b.ProvideID=c.Pr_id)  where  b.State = 1 and b.ShowFlage=1
                         ", BegDate, EndDate, EndDatetime, maxcheckdate);

            return db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(strSql).QueryMany<SiloStat>();
        }


        public dynamic GetTotalSiloStat(string BegDate)
        {

            string EndDate = BegDate;
            string EndDatetime = BegDate + " 23:59:59"; //DateTime.Now.ToString();

            string checkdatestrSql = string.Format("select max (Checkdate) as checkdate from CheckSilot");
            DateTime startDate = db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(checkdatestrSql).QuerySingle<DateTime>();

            string maxcheckdate = startDate.ToString("yyyy-MM-dd") + " 23:59:59";
            //string maxcheckdate="2018-1-1";
            var strSql = String.Format(@"
                          select sum(a.期初值+(a.累计入库-a.当日入库)-(a.累计出库-a.当日出库)) as 昨日库存, sum(a.当日入库) as 当日入库 ,sum(a.当日出库) as 当日出库,
                          sum(a.期初值+a.累计入库-a.累计出库)as 本日库存  from  SiloStat('{0}','{1}','{2}','{3}')
                           as a join silot as b on (a.silo_id=b.siloName) join Provide as c on (b.ProvideID=c.Pr_id)  where  b.State = 1 and b.ShowFlage=1
                         ", BegDate, EndDate, EndDatetime, maxcheckdate);

            return db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(strSql).QueryMany<SiloStat>();

        }


        public dynamic GetSNTotalSiloStat(string BegDate) //水泥库存
        {

            string EndDate = BegDate;
            string EndDatetime = BegDate + " 23:59:59"; //DateTime.Now.ToString();

            string checkdatestrSql = string.Format("select max (Checkdate) as checkdate from CheckSilot");
            DateTime startDate = db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(checkdatestrSql).QuerySingle<DateTime>();

            string maxcheckdate = startDate.ToString("yyyy-MM-dd") + " 23:59:59";
            //string maxcheckdate="2018-1-1";
            var strSql = String.Format(@"
                          select sum(a.期初值+(a.累计入库-a.当日入库)-(a.累计出库-a.当日出库)) as 昨日库存, sum(a.当日入库) as 当日入库 ,sum(a.当日出库) as 当日出库,
                          sum(a.期初值+a.累计入库-a.累计出库)as 本日库存  from  SiloStat('{0}','{1}','{2}','{3}')
                           as a join silot as b on (a.silo_id=b.siloName) join Provide as c on (b.ProvideID=c.Pr_id)  where  b.State = 1 and b.ShowFlage=1 and c.Type='水泥'
                         ", BegDate, EndDate, EndDatetime, maxcheckdate);

            return db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(strSql).QueryMany<SiloStat>();

        }


    }

}