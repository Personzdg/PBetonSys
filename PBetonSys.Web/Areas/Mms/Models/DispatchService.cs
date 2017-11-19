using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Dispatch : ModelBase 
    {
        public string 序号 { get; set; }

        public string 合同编号 { get; set; }

        public string 任务单编号 { get; set; }

        public string 配比编号 { get; set; }

        public string 临时工地 { get; set; }

        public string 结构部位 { get; set; }

        public string 强度等级 { get; set; }

        public string 坍落度 { get; set; }

        public string 泵送 { get; set; }

        public string 预定方量 { get; set; }

        public string 累计方量 { get; set; }

        public string 累计车次 { get; set; }

        public DateTime 第一车时间 { get; set; }

        public DateTime 最后一车时间 { get; set; }

        public string 任务供应时间 { get; set; }
    }
    public class DispatchService : ServiceBase<SilotReport>
    {
         public DispatchService()
        {
            base.ModuleName = "Betonsys";
        }

        public List<Dispatch> GetDispatchList() 
        {
            string strSql = "SELECT * FROM AttemperTaskListTab ()";
            return db.Sql(strSql).QueryMany<Dispatch>();
        }
    }
}