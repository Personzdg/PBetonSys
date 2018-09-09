using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace PBetonSys.Web.Areas.Mms.Models
{
    public class DayReport : ModelBase 
    {
        public string 合同编号 { get; set; }
        public string 工程名称 { get; set; }
        public string 施工单位 { get; set; }
        public decimal 预订方量 { get; set; }
        public decimal 生产方量 { get; set; }
        public decimal 签收方量 { get; set; }
        public decimal 砂浆 { get; set; }
        public string 泵号 { get; set; }   
        public string 施工部位 { get; set; }
        public string 强度等级 { get; set; }
        public string 坍落度 { get; set; }
        public string 任务单号 { get; set; }
        public string 合同单位 { get; set; }
        public string 临时工地 { get; set; }
        public string 备注 { get; set; }
        public DateTime 开始时间 { get; set; }
        public DateTime 结束时间 { get; set; }
        public int 累计车次 { get; set; }
        public string 销售员 { get; set; }
    }
    ////
    public class DayReportService : ServiceBase<DayReport>
    {
        public DayReportService()
        {
            base.ModuleName = "Betonsys";
        }


        public List<DayReport> GetDayReportData(string BegDayDate, string EndDatetime)
        {
            var strSql = String.Format(@"
                         select 合同编号,工程名称,施工单位,预订方量,生产方量,签收方量,砂浆,泵号,施工部位,强度等级,坍落度,任务单号,合同单位,临时工地,备注,开始时间,结束时间,累计车次,销售员  from Betonsys..DayReportStarTab('{0}','{1}')  
                         ", BegDayDate, EndDatetime+" 23:59:59");

            return db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql).QueryMany<DayReport>();
        }


        public dynamic GetTotalDayReport(string BegDayDate, string EndDatetime)
        {

            var strSql = String.Format(@"
                         select sum(预订方量) as 预订方量,sum(生产方量) as 生产方量 ,sum(签收方量) as 签收方量,sum(累计车次) as 累计车次 from DayReportStarTab('{0}','{1}')  
                         ", BegDayDate, EndDatetime + " 23:59:59");

            return db.ConnectionStringName("Betonsys", new SqlServerProvider()).Sql(strSql).QueryMany<DayReport>();
          
        }

    }

}