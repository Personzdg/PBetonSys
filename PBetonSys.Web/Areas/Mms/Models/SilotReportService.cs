using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class SilotReport : ModelBase 
    {
        public string Hous_id { get; set; }

        public decimal MaxVal { get; set; }

        public string MaxValDesc { get { return string.Format("{0}吨",MaxVal); } }

        public string MinVal { get; set; }

        public string Name { get; set; }

        public decimal SVal { get; set; }

        public decimal EVal { get; set; }

        public decimal IVal { get; set; }

        public decimal UVal { get; set; }

        public decimal PVal { get; set; }

        public string PValDesc { get { return string.Format("{0}吨", PVal); } }

        public string UPercentDesc 
        {
            get { return string.Format("{0}%",UPercent); }
        }

        public string UnPercentDesc
        {
            get { return string.Format("{0}%", UnPercent); }
        }

        public string IsShow 
        {
            get { return UPercent > 50 ? "block" :"none"; }
        }
        public int UPercent 
        {
            get {
                if(MaxVal==0)
                {
                    return 0;
                }
                var pResult = int.Parse(((PVal / MaxVal) * 100).ToString("F0"));
                return pResult>100?100:pResult<0?0:pResult;
            }
        }

        public int UnPercent
        {
            get { return 100 - UPercent; }
        }
    }
    public class SilotReportService : ServiceBase<SilotReport>
    {
        public SilotReportService()
        {
            base.ModuleName = "Material";
        }


        public List<SilotReport> GetSilotReportData()
        {

            // string startDate = "2017-10-16";   //select max (isnull(Checkdate,getdate())) as checkdate from CheckSilot
            string endDate = DateTime.Now.ToString();
            string checkdatestrSql = string.Format("select max (Checkdate) as checkdate from CheckSilot");
            DateTime startDate = db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(checkdatestrSql).QuerySingle<DateTime>();


            string strSql = string.Format("Select b.Hous_id,b.MaxVal,b.MinVal,a.筒仓名称 Name,a.期初值 SVal,a.盘点值 PVal,a.入库值 IVal,a.消耗值 UVal from CheckSilotfunction('{0}','{1}') as a join silot as b on (a.筒仓名称=b.siloName) where  b.State = 1 and b.ShowFlage=1 order by Hous_id ", startDate.ToString(), endDate);
            return db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(strSql).QueryMany<SilotReport>();
        }
    }
}
//        public List<SilotReport> GetSilotReportData()
//        {
//            string startDate = "2017-10-16";
//            string endDate = "2017-10-21";
//            string strSql = string.Format("Select b.Hous_id,b.MaxVal,b.MinVal,a.筒仓名称 Name,a.期初值 SVal,a.盘点值 PVal,a.入库值 IVal,a.消耗值 UVal from CheckSilotfunction('{0}','{1}') as a join silot as b on (a.筒仓名称=b.siloName) where  b.State = 1 and b.ShowFlage=1", startDate, endDate) ;
//            return db.ConnectionStringName(APP.DB_Materials, new SqlServerProvider()).Sql(strSql).QueryMany<SilotReport>();
//        } 
//    }
//}