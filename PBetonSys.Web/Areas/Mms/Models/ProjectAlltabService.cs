using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace PBetonSys.Web.Areas.Mms.Models
{
    public class ProjectAlltab : ModelBase 
    {
        public string name { get; set; }
        public string ProjectName { get; set; }
        public string SalseName { get; set; }
        public string 合同编号 { get; set; }
        public decimal 本日销售方量 { get; set; }
        public decimal 本日销售金额 { get; set; }
        public decimal 本月销售方量 { get; set; }
        public decimal 本月销售金额 { get; set; }
        public decimal 本月实际收款 { get; set; }
        public decimal 本月累计收款 { get; set; }

        public decimal 累计销售方量 { get; set; }
        public decimal 累计销售金额 { get; set; }
        public decimal 其他扣除 { get; set; }
        public decimal 累计欠款 { get; set; }
        public decimal 收款率 { get; set; } 
    }

    public class ProjectAlltabService : ServiceBase<ProjectAlltab>
    {
        public ProjectAlltabService()
        {
            base.ModuleName = "Settlement";
        }


        public List<ProjectAlltab> GetProjectAlltabData(string BegDayDate,string EndDatetime)
        {
             //string BegDayDate = DateTime.Now.ToString("yyyy-mm-dd");
             string BegMonthDate = DateTime.Now.ToString("yyyy-MM-01");
             string  BegYearDate = "2017-01-01";
             //string EndDatetime = DateTime.Now.ToString();
             string  ContID = "";
  

            var strSql = String.Format(@"
                        select e.name, c.ProjectName, c.SalseName, b.合同编号,b.本日销售方量,b.本日销售金额,b.本月销售方量,b.本月销售金额,
                         b.累计销售方量,b.累计销售金额, isnull(a.本月实际收款,0) as 本月实际收款 ,isnull(a.本月累计收款,0) as 本月累计收款,
                         isnull(其他扣除,0) as 其他扣除,(isnull(b.累计销售金额,0)-isnull(a.本月累计收款,0)) as 累计欠款 ,
                         ((a.本月累计收款-a.其他扣除)/b.累计销售金额)*100 as 收款率 from contract as c LEFT OUTER JOIN  
                         ProjectGatheringGatherTab('{0}','{1}','{2}','{3}') as a  on (c.SysCont_id=a.合同编号) LEFT OUTER JOIN 
                         ProjectGatherStatTab('{4}','{5}','{6}','{7}','{8}')  as b  on( c.SysCont_id=b.合同编号)  join Clinet as E on (c.Clinet_id=e.Clinet_id) 
                         ", BegMonthDate, BegYearDate, EndDatetime, ContID, BegDayDate, BegMonthDate, BegYearDate, EndDatetime, ContID);            
            
            return db.ConnectionStringName(APP.DB_Settlement, new SqlServerProvider()).Sql(strSql).QueryMany<ProjectAlltab>();
        }
    }

}