using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class TaskService : ServiceBase<Task>
    {
        public TaskService() 
        {
            base.db.ConnectionStringName(APP.DB_Betonsys, new SqlServerProvider());
        }
        //public List<Task> GetTaskList()
        //{

        //    string strSql = "SELECT [Task_id],[House_id],[Cont_ID],[Clin_ID],[Place],[Strong],[Fall],[Infiltrate] ,[Amount] ,[Pump],[KanZhe],[Pump_vehicle],[LinkName],[Telephon],[ViseName],[Auditing],[CheckDateTime],[Provide_DateTime] ,[Remark],[State],[Wjj],[ShowFlag],[ContractUnit] ,[TempProject],[Task_inside_code],[AuditingFlag],[NetID],[Type] FROM [dbo].[Task]";
        //    return db.Sql(strSql).QueryMany<Task>();
        //}
    }
    public class Task : ModelBase
    {

        /// <summary>
        /// 单号任务，单号由存储过程GetTaskNO得到
        /// </summary>
        [PrimaryKey]
        public string Task_id { get; set; }

        /// <summary>
        /// 楼号
        /// </summary>
        public string House_id { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string Cont_ID { get; set; }

        /// <summary>
        /// 施工单位编号
        /// </summary>
        public string Clin_ID { get; set; }

        /// <summary>
        /// 结构部位
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// 强度
        /// </summary>
        public string Strong { get; set; }

        /// <summary>
        /// 坍落度
        /// </summary>
        public string Fall { get; set; }

        /// <summary>
        /// 暂时不需要
        /// </summary>
        public string Infiltrate { get; set; }

        /// <summary>
        /// 预定方量
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// 是否泵送
        /// </summary>
        public bool? Pump { get; set; }

        /// <summary>
        /// 暂时不需要
        /// </summary>
        public string KanZhe { get; set; }

        /// <summary>
        /// 泵车
        /// </summary>
        public string Pump_vehicle { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephon { get; set; }

        /// <summary>
        /// 签发（新单登录用户）
        /// </summary>
        public string ViseName { get; set; }


        /// <summary>
        /// 审核（审核登录用户）
        /// </summary>
        public string Auditing { get; set; }

        /// <summary>
        /// 新单时间
        /// </summary>
        public DateTime CheckDateTime { get; set; }

        /// <summary>
        /// 供应时间
        /// </summary>
        public DateTime? Provide_DateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 暂时不需要
        /// </summary>
        public bool? State { get; set; }

        /// <summary>
        /// 外加剂
        /// </summary>
        public string Wjj { get; set; }
        /// <summary>
        /// 暂时不需要
        /// </summary>
        public bool? ShowFlag { get; set; }

        /// <summary>
        /// 暂时不需要
        /// </summary>
        public string ContractUnit { get; set; }

        /// <summary>
        /// 暂时不需要
        /// </summary>
        public string TempProject { get; set; }

        /// <summary>
        /// 暂时不需要
        /// </summary>
        public string Task_inside_code { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public bool? AuditingFlag { get; set; }

        /// <summary>
        /// 手机端导入过来的编号（可以不用管)
        /// </summary>
        public int? NetID { get; set; }

        /// <summary>
        /// 类型（"net"手机下单）
        /// </summary>
        public string Type { get; set; }
    }
}