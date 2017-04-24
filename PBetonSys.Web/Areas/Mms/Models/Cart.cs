using PBetonSys.Core;
using PBetonSys.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Cart : ModelBase
    {
        public string CartID { get; set; }

        public string licenseID { get; set; }

        public string CartType { get; set; }

        public string Company { get; set; }

        public string department { get; set; }

        public string brand { get; set; }

        public string Tare { get; set; }

        public decimal Cart_bulk { get; set; }

        public string Remark { get; set; }

        public Int16 State { get; set; }

        public DateTime BuyDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public decimal UseTime { get; set; }

        public string Pump_Type { get; set; }

        public int OrderID { get; set; }

        public int Flag { get; set; }

        public DateTime UpdataDatetime { get; set; }
        public string CardId { get; set; }
        public double Average_Oil_Consume { get; set; }

        public int ID { get; set; }

        public string Hous_id { get; set; }

        public string ProduceHous { get; set; }
    }

    public class CartService : ServiceBase<Cart>
    {
       public CartService()
       {
           base.ModuleName = "Betonsys";
       }
        public List<Cart> GetCartList()
        {
            string strSql = "SELECT [Clinet_id] ,[Name],[CheckDateTime],[State],[Remark],[SimpleName],[ClerkID],[LinkName],[LinkPhon],[WXCode],[Password] FROM [dbo].[Clinet]";
            return db.ConnectionStringName(APP.DB_Betonsys, new SqlServerProvider()).Sql(strSql).QueryMany<Cart>();
        } 
    }
}