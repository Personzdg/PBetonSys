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
    public class ProductController : Controller
    {
        //
        // GET: /Mms/Product/
        public ActionResult Index()
        {
            var model = new
            {
                form = new
                {
                    checkdateTime = DateTime.Now.ToString("yyyy-MM-dd hh24:mi:ss"),
                    Hous_ID = ""
                }
            };
            return View(model);
        }
	}

    public class ProductApiController : ApiController 
    {
        public dynamic GetProductList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='a.CheckDateTime'>
                    <select>
                       a.Transp_id,a.Cont_id,a.Vehicle_id,a.Driver,a.amount,a.Number,a.CheckDateTime,a.ProduceFalg,a.Task_id , b.strong,c.ProjectName
                    </select>
                    <from>
                         transport as a
                         join task as b on (a.task_id=b.task_id)
                         left join Contract as c on a.Cont_ID=c.Cont_ID
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new ProductService().GetDynamicListWithPaging(pQuery);
            return result;
        }
    }
}