using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PBetonSys.Web.Areas.Mms.Models
{
    public class ConfectService: ServiceBase<Confect>
    {

        public ConfectService() 
        {
            base.ModuleName = "Betonsys";
        }

    }
        public class Confect : ModelBase 
        {
            [PrimaryKey]
            public string Confect_ID { get; set; }
            public string Task_id { get; set; }
            public string Cont_ID { get; set; }
            /// 楼号
            /// </summary>
            public string Hous_id { get; set; }

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
        
            /// </summary>
            public string ViseName { get; set; }

            public string Auditing { get; set; }

            public string Inside_Code { get; set; }

            public bool? State { get; set; }

           
             public DateTime CheckDateTime { get; set; }
             public string Remark { get; set; }

             public int? InsFlag { get; set; }

             public bool? CheckFlag { get; set; }

             public string CheckName { get; set; }

             public string Wjj { get; set; }
             public bool? AuditingFlag { get; set; }
             public DateTime PrintDateTime { get; set; }
             public string FreeCode { get; set; }

             public int? MixTime1 { get; set; }
             public int? MixTime2 { get; set; }
             public int? MixTime3 { get; set; }
             public int? TX4 { get; set; }
             public bool? Inside_ConfectFlag { get; set; }
             public decimal? sumTheory_value { get; set; }
             public DateTime UpDateTime { get; set; }
            
            
            
            
            
            
            
            
            
            
            



        
        }

    }

