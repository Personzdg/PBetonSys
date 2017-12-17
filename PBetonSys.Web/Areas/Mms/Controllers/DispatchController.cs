﻿using PBetonSys.Core;
using PBetonSys.Web.Areas.Mms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PBetonSys.Web.Areas.Mms.Controllers
{
    public class DispatchController : Controller
    {
        //
        // GET: /Mms/Dispatch/
        public ActionResult Index()
        {
            var model = new
            {
                dataSource = new 
                {
                    houseItems= new DispatchService().GetHouseList(),
                    carItems=new DispatchService().GetCarList(),
                    driveItems = new DispatchService().GetDriveList(),
                    pumpItems = new DispatchService().GetPumpList()
                }
            };
            return View(model);
        }
	}

    public class DispatchApiController : ApiController
    {
        public dynamic GetDispatchList()
        {
            var result = new DispatchService().GetDispatchList();
            return result;
        }

        public dynamic GetHouseList() 
        {
            return new DispatchService().GetHouseList();
        }

        public dynamic GetDriveList()
        {
            return new DispatchService().GetDriveList();
        }
    }
}