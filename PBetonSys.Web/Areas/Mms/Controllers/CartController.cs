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
    public class CartController : Controller
    {
        //
        // GET: /Mms/Car/
        public ActionResult Index()
        {
            return View();
        }
	}

    public class CartApiController : ApiController 
    {
        public dynamic GetCartList(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
                <settings defaultOrderBy='BuyDateTime'>
                    <select>
                      CartID,CartType,licenseID,Company,department,brand,Tare,Cart_bulk,UseTime,BuyDateTime,StartDateTime,State,Average_Oil_Consume,CardId,Remark
                    </select>
                    <from>
                        Cart
                    </from>
                </settings>");
            var pQuery = query.ToParamQuery();
            var result = new CartService().GetDynamicListWithPaging(pQuery);
            return result;
        }

        public string GetNewCode()
        {
            var service = new CartService();
            return service.GetNewKey("CartID", "maxplus").PadLeft(3, '0');
        }

        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {
            var listWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
            <settings>
                <table>
                    Cart
                </table>
                <where>
                    <field name='CartID' cp='equal' variable='CartID'></field>
                </where>
            </settings>");
            var service = new CartService();
            var result = service.Edit(null, listWrapper, data);
        }
    }
}