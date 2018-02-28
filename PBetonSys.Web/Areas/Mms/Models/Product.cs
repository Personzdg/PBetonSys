using PBetonSys.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBetonSys.Web.Areas.Mms.Models
{
    public class Product : ModelBase
    {
    }

    public class ProductService : ServiceBase<Product>
    {
        public ProductService()
        {
            base.ModuleName = "Betonsys";
        }
    }
}