using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    
           public class S_ConfectService: ServiceBase<S_Confect>
    {

        public S_ConfectService() 
        {
            base.ModuleName = "Betonsys";
        }

    }

           public class S_Confect : ModelBase
           {
               [PrimaryKey]
               public int ID { get; set; }
               public string Inside_ID { get; set; }
               public string Type { get; set; }
               public string MT_Size { get; set; }
               public decimal? Theory_Value { get; set; }
               public string Provide_ID { get; set; }
               public bool? State { get; set; }
               public string Strong { get; set; }
               public string Fall { get; set; }
               public string silo { get; set; }
               public decimal? Tol { get; set; }
               public decimal? T1 { get; set; }
               public string MTType { get; set; }
               public string Place { get; set; }
               public string Hous1Name { get; set; }
               public string Hous2Name { get; set; }
               public string Hous3Name { get; set; }
               public string Hous4Name { get; set; }
               public string ZBCode { get; set; }
               public string Pump { get; set; }
               public decimal? jbtime { get; set; }
               public decimal? MT_Value { get; set; }
               public int Sequence { get; set; }
               public decimal? Range_To { get; set; }
               public decimal? Range_From { get; set; }
               public decimal? NewMin { get; set; }
               public decimal? NewMax { get; set; }
               public decimal? MixTime2 { get; set; }
               public decimal? MixTime3 { get; set; }
              




           }
  
}