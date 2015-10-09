using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleAjaxApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [OutputCache(Duration = 30)]
        public PartialViewResult Index()
        {
            return PartialView("_IndexViewPartial");
        }

        [HttpPost]
        [OutputCache(Duration = 90)]
        public PartialViewResult Index(string parameter)
        {
            return PartialView("_IndexReturnViewPartial");
        }
    }
}