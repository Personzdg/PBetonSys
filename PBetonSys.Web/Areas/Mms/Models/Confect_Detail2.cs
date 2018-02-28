using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Confect_Detail2Service : ServiceBase<Confect_Detail2>
    {

        public Confect_Detail2Service()
        {
            base.ModuleName = "Betonsys";
        }

    }

     public class Confect_Detail2 : ModelBase 
        {
          public string Confect_ID { get; set; }
          public string Inside_ID { get; set; }
          public string Confect_Detail_ID { get; set; }
          public string Type { get; set; }
          public string MT_Size { get; set; }
          public decimal? Ratio { get; set; }
          public decimal? MT_Value { get; set; }
          public decimal? Theory_value { get; set; }

          public string Provide_ID { get; set; }
          public DateTime CheckDateTime { get; set; }
          public bool? State { get; set; }
          public bool? Flag { get; set; }
          public string silo { get; set; }

          public decimal? Tol { get; set; }
          public decimal? T1 { get; set; }

          public string MtType { get; set; }
          public string MTSize { get; set; }
          public string MTCode { get; set; }
          public string Hous1Name { get; set; }
          public string Hous2Name { get; set; }
          public string Hous3Name { get; set; }
          public string Hous4Name { get; set; }
          public string Hous_ID { get; set; }
          public string ZBCode { get; set; }
          public string Clerk { get; set; }
          public int? Sequence { get; set; }
          public decimal? Range_To { get; set; }
          public decimal? range_From { get; set; }
          public decimal? NewMin { get; set; }
          public decimal? NewMax { get; set; }
          public decimal? NewS_Value { get; set; }
          public decimal? OldS_Value { get; set; }




     }


}