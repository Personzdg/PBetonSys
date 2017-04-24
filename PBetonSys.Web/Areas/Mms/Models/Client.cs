  using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{

    public class ClientService : ServiceBase<Clinet>
    {
        public List<Clinet> GetClientList()
        {
            string strSql = "SELECT [Clinet_id] ,[Name],[CheckDateTime],[State],[Remark],[SimpleName],[ClerkID],[LinkName],[LinkPhon],[WXCode],[Password] FROM [dbo].[Clinet]";
            return db.Sql(strSql).QueryMany<Clinet>();
        }
    }
    public class Clinet : ModelBase
    {
        [PrimaryKey]
        public string Clinet_id { get; set; }

        public string Name { get; set; }

        public DateTime CheckDateTime { get; set; }

        public Int16 State { get; set; }

        public string Remark { get; set; }

        public string SimpleName { get; set; }

        public string ClerkID { get; set; }

        public string LinkName { get; set; }

        public string LinkPhon { get; set; }

        public string WXCode { get; set; }

        public string Password { get; set; }
    }
}