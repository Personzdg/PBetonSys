using PBetonSys.Core;
using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class ConfectController : Controller
    {
        // GET: /Mms/confect/
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    Confect_ID = "",
                    ProjectName = "",
                    CheckDateTime = ""
                }
            };
            return View(model);
        }

        /// <summary>
        /// 配比审核
        /// </summary>
        public ActionResult Audit() 
        {
            var model = new
            {
                form = new
                {
                    Task_id = "",
                    cont_id = "",
                    projectname="",
                    Name = "",
                    Place = "",
                    Provide_DateTime = "",
                    Stong="",
                    Fall=""
                }
            };
            return View(model);
        }

        public ActionResult AuditDetail(string id = "")
        {
            var model = new
            {
                form = new 
                { Confect_ID = id 
                }
            };
            return View(model);
        }
        public ActionResult ConfectList()
        {
            return View();
        }



    }

    public class ConfectApiController : MmsBaseApi<Confect, ConfectService>
    {

        public dynamic GetConfectList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                        a.Confect_ID,a.Cont_ID,a.Hous_id,a.Amount,a.Place,a.Strong,a.Fall,a.Pump,a.Inside_Code,a.CheckDateTime,
                        a.AuditingFlag,a.sumTheory_value,a.UpDateTime,c.Pump_vehicle,c.LinkName,c.Telephon,c.Provide_DateTime ,b.ProjectName,e.Name
                    </select>
                    <from>
                        Confect as a
                       left join Task as c on(a.Task_ID=c.Task_ID)   left join Contract as b on a.Cont_ID=b.Cont_ID  left join   Client  as e on (b.Clinet_id=e.Cl_ID ) and (e.flag=0)
                    </from>
  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
    <field name='a.Confect_ID'       cp='startwith'  ></field>
    <field name='b.ProjectName'       cp='like'   ></field>
    <field name='c.Provide_DateTime'          cp='daterange'  ></field>
  </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new TaskService().GetDynamicListWithPaging(pQuery);
            return result;
        }

        public dynamic GetAuditConfectList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='CheckDateTime'>
                    <select>
                        task.Provide_DateTime, task.House_id as tHous,task.Pump_vehicle, task.ContractUnit,Task.TempProject, Confect.*,Contract.projectname,SysCode.DESCRIPTION,Client.Name
                    </select>
                    <from>
                        Confect
                        join  Contract on Confect.Cont_ID=Contract.cont_id and Contract.State=1
                        join Task on Task.Task_id=Confect.Task_id
                        left join Client on Contract.Clinet_id=Client.Cl_ID and Client.flag=0
                        left join SysCode on  Confect.Hous_id=SysCode.Code and SysCode.CodeType='House_id'
                    </from>
                  <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
                    <field name='Confect.Task_id'       cp='equal'  ></field>
                    <field name='Confect.Cont_ID'       cp='equal'   ></field>
                  <field name='Contract.projectname'       cp='like'   ></field>
                  <field name='Client.Name'       cp='like'   ></field>
                  <field name='Client.Place'       cp='like'   ></field>
                    <field name='Task.Provide_DateTime'          cp='daterange'  ></field>
                </where>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new TaskService().GetDynamicListWithPaging(pQuery);
            return result;
        }



        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                        <table>
                            Confect
                        </table>
                        <where>
                            <field name='Confect_ID' cp='equal' variable='Confect_ID'></field>
                        </where>
                    </settings>");

            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                        <table>
                            Confect_Detail2
                        </table>
                        <where>
                            <field name='Confect_ID' cp='equal' variable='Confect_ID'></field>
                        </where>
                    </settings>");

            var service = new TaskService();
            var fromId = data.form.Confect_ID.Value;
            ParamDelete pd = ParamDelete.Instance().From("Confect_Detail2").AndWhere("Confect_ID",fromId); //删除之前的记录
            var result = service.Edit(formWrapper, listWrapper, data);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Confect_ID"></param>
        /// <returns></returns>
        public dynamic GetAjustList(string Confect_ID)
        {
            var query = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                          <select>
                            Confect_Detail_ID,Hous_ID
                        </select>
                        <from>
                            Confect_Detail2
                       </from>
                    </settings>");
            var pQuery = query.ToParamQuery();
            pQuery.AndWhere("Confect_ID", Confect_ID);
            return new TaskService().GetDynamicList(pQuery);
        }

        public dynamic GetConfectDetaiList(string Confect_Detail_ID) 
        {
            var query = RequestWrapper.Instance().LoadSettingXmlString(@"
                    <settings>
                          <select>
                            Inside_ID,Hous1Name,Hous2Name,Hous3Name,Hous4Name,MT_Size,Theory_Value,Tol,T1,MTType,MTSize,Provide_ID,MTCode,ZBCode,MT_Value ,Sequence,Range_To,range_From,NewMin,NewMax,Theory_Value,MT_Value,Ratio
                        </select>
                        <from>
                            Confect_Detail2
                       </from>
                    </settings>");
            var pQuery = query.ToParamQuery();
            pQuery.AndWhere("Confect_Detail_ID", Confect_Detail_ID);
            return new TaskService().GetDynamicList(pQuery);
        }
     }


}