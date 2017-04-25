using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PBetonSys.Web.Areas.Mms
{
    public class MmsBaseApi<TMasterModel, TMasterService> : ApiController
        where TMasterModel : ModelBase, new()
        //where TDetailModel : ModelBase, new()
        where TMasterService : ServiceBase<TMasterModel>, new()
        //where TDetailService : ServiceBase<TDetailModel>, new()
    {
        #region 属性
        private TMasterService _masterService;
        //private TDetailService _detailService;
        public TMasterService masterService
        {
            get
            {
                if (_masterService == null)
                    _masterService = new TMasterService();
                return _masterService;
            }
        }
        //public TDetailService detailService
        //{
        //    get
        //    {
        //        if (_detailService == null)
        //            _detailService = new TDetailService();
        //        return _detailService;
        //    }
        //}
        #endregion

        #region 自动完成
        // GET api/mms/send/getbillno
        public virtual List<dynamic> GetBillNo(string q)
        {
            var pQuery = ParamQuery.Instance().Select("top 10 BillNo").AndWhere("BillNo", q, Cp.StartWith);
            return masterService.GetDynamicList(pQuery);
        }
        #endregion

        #region 采播
        // 取得新的主表Bill GET api/mms/send/getnewbillno
        public virtual string GetNewBillNo()
        {
            return masterService.GetNewKey("BillNo", "dateplus");
        }

        //// 取得新的明细表RowId GET api/mms/send/getnewrowid
        //public virtual string GetNewRowId(int id, string BillNo)
        //{
        //    return detailService.GetNewKey("RowId", "maxplus", id, ParamQuery.Instance().AndWhere("BillNo", BillNo));
        //}
        #endregion

        #region 查询
        // 查询主表数据列表 GET api/mms/send 
        public virtual dynamic Get(RequestWrapper query)
        {
            if (!query.IsLoadedSettings)
                query.LoadSettingXmlString(@"
<settings defaultOrderBy='BillNo'>
    <select>*</select>
    <from>{0}</from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>", typeof(TMasterModel).Name);
            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicListWithPaging(pQuery);
            return result;
        }

        // 取得编辑页面中的主表数据
        public virtual dynamic GetEditMaster(string id)
        {
            return new
            {
                form = masterService.GetModel(ParamQuery.Instance().Select("[SysCont_ID],[Cont_ID],[ProjectName],[ProjectAddr],[Interva],[Clinet_id],[CheckDateTime],[SalseName],[BossName],[SalseRecevice],[LinkPhon],[MobilePhon],[Strong],[Amount],[Price],[FinshFlag],[FinShDateTime],[EndPaymentDatetime],[paymentType],[GatheringRatio],[Remark],[StatDate],[GatheringDate],[EndPaymentMonth],[ContType],[SimpleName],[LinkName],[WXCode],[Password]").AndWhere("SysCont_ID", id))
            };
        }

        // 查询明细表 GET api/mms/send/getdetail
        public virtual dynamic GetDetail(string id)
        {
            var query = RequestWrapper
                .InstanceFromRequest()
                .SetRequestData("BillNo", id)
                .LoadSettingXmlString(@"
<settings defaultOrderBy='MaterialCode'>
    <select>
        A.*, B.MaterialName,B.Model,B.Material
    </select>
    <from>
        {0} A
        left join mms_material B on B.MaterialCode = A.MaterialCode
    </from>
    <where>
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>", "");

            var pQuery = query.ToParamQuery();
            var result = masterService.GetDynamicListWithPaging(pQuery);
            return result;
        }
        #endregion

        #region 删除
        // 删除 DELETE api/mms/send
        public virtual void Delete(string id)
        {
            var result = masterService.Delete(ParamDelete.Instance().AndWhere("BillNo", id));
        }
        #endregion

        #region 保存
        // 保存 POST api/mms/send
        [HttpPost]
        public virtual void Edit(dynamic data)
        {
            var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>{0}</table>
    <where><field name='SysCont_ID' cp='equal'></field></where>
</settings>", typeof(TMasterModel).Name);

            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>{0}</table>
    <where>
        <field name='SysCont_ID' cp='equal'></field>
    </where>
</settings>", "");

            var result = masterService.Edit(formWrapper, listWrapper, data);
        }
        #endregion
    }
}