using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Tool_Shop_Web_App.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderListing()
        {
            return View();
        }
        public ActionResult OrderDetails()
        { 
            return View();
        }

    }
}