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

        [HttpPost]
        [Route]
        [OutputCache(Duration = 90)]
        public PartialViewResult Search(string name)
        {
            var customerResultsViewModel = new Models.CustomerResultsViewModel();

            if (name != "")
            {
                var customerSearchResults = new List<Models.CustomerViewModel>
                {
                    new Models.CustomerViewModel
                    {
                        FirstName = "Tony",
                        LastName = "Romo",
                        Email = "cowboys@gmail.com"
                    },

                    new Models.CustomerViewModel
                    {
                        FirstName = "Matthew",
                        LastName = "Ryan",
                        Email = "falcons@gmail.com"
                    },

                    new Models.CustomerViewModel
                    {
                        FirstName = "Aaron",
                        LastName = "Rodgers",
                        Email = "packers@gmail.com"
                    }
                };
                customerResultsViewModel.CustomerSearchResults = customerSearchResults;
            }
            else
            {
                customerResultsViewModel.CustomerSearchResults = new List<Models.CustomerViewModel>();
            }

            


            //CustomersDatabaseDataContext db = new CustomersDatabaseDataContext();

            //var customers = from p in db.Customers
            //                select p;



            return PartialView("_UserResultsViewPartial", customerResultsViewModel);
        }
    }
}