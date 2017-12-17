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

        public string ProjectName { get; set; }

        public int CheckFlag { get; set; }

        public string Telephon { get; set; }

        public string House_id { get; set; }

        public int ShowFlag { get; set; }

        public string CheckAdder { get; set; }

        public string wjj { get; set; }


        public string interva { get; set; }

        public string ClientName { get; set; }
    }
    public class DispatchService : ServiceBase<SilotReport>
    {
         public DispatchService()
        {
            base.ModuleName = "Betonsys";
        }

        public List<Dispatch> GetDispatchList() 
        {
            string strSql = "select e.CheckFlag, b.Telephon,  b.House_id, b.ShowFlag,c.CheckAdder,b.wjj,c.interva,a.*,c.ProjectName,f.Name ClientName   from AttemperTaskListTab() as a  join  Task as b on(a.任务单编号=b.Task_id) join contract as c on(a.合同编号=c.Cont_id) join Confect as e  on (a.配比编号=E.Confect_ID) left join dbo.Client as f on c.Clinet_id=f.Cl_ID order by e.CheckFlag";
            return db.Sql(strSql).QueryMany<Dispatch>();
        }

        public dynamic GetHouseList()
        {
            var pQuery = ParamQuery.Instance()
                .Select("Code as value,DESCRIPTION as text")
                .From("SysCode")
                .AndWhere("CodeType", "House_id");

            return base.GetDynamicList(pQuery);
        }

        public dynamic GetCarList()
        {
            var pQuery = ParamQuery.Instance()
                .Select("Code as value,DESCRIPTION as text")
                .From("SysCode")
                .AndWhere("CodeType", "拌车");

            return base.GetDynamicList(pQuery);
        }

        public dynamic GetDriveList()
        {
            var pQuery = ParamQuery.Instance()
                .Select("DriveID as value,Name as text")
                .From("Drive");
            return base.GetDynamicList(pQuery);
        }

        public dynamic GetPumpList()
        {
            var pQuery = ParamQuery.Instance()
                .Select("Code as value,DESCRIPTION as text")
                .From("SysCode")
                .AndWhere("CodeType", "pumpType");

            return base.GetDynamicList(pQuery);
        }
    }
}