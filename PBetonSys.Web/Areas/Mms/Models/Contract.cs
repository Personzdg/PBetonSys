using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PBetonSys.Web.Areas.Mms.Models
{

    public class ContractService: ServiceBase<Contract>
    {
        //public List<Contract> GetContractList()
        //{
        //    string strSql = "SELECT [Clinet_id] ,[Name],[CheckDateTime],[State],[Remark],[SimpleName],[ClerkID],[LinkName],[LinkPhon],[WXCode],[Password] FROM [dbo].[Clinet]";
        //    return masterService.GetModelList
        //}

        //public virtual dynamic GetModel(string id) 
        //{
        //    return .GetModel(ParamQuery.Instance().AndWhere("BillNo", id));
        //}
    }

    public class Contract: ModelBase
    {
        [PrimaryKey]
        public string SysCont_ID { get; set; }

        public string Cont_ID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectAddr { get; set; }

        public decimal? Interva { get; set; }

        public string Clinet_id { get; set; }

        public DateTime CheckDateTime { get; set; }

        public string SalseName { get; set; }

        public string BossName { get; set; }

        public string SalseRecevice { get; set; }

        public string LinkPhon { get; set; }

        public string MobilePhon { get; set; }

        public string Strong { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Price { get; set; }

        public Int16 FinshFlag { get; set; }

        public DateTime FinShDateTime { get; set; }

        public DateTime EndPaymentDatetime { get; set; }

        public string paymentType { get; set; }

        public decimal? GatheringRatio { get; set; }

        public string Remark { get; set; }

        public string StatDate { get; set; }

        public string GatheringDate { get; set; }

        public int? EndPaymentMonth { get; set; }

        public string ContType { get; set; }

        public string SimpleName { get; set; }

        public string LinkName { get; set; }

        public string WXCode { get; set; }

        public string Password { get; set; }
    }
}