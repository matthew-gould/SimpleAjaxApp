using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SimpleAjaxApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [OutputCache(Duration = 30)]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [OutputCache(Duration = 90)]
        public PartialViewResult Index(string parameter)
        {
            return PartialView("_IndexReturnViewPartial");
        }

        [HttpGet]
        [OutputCache(Duration = 30)]
        public PartialViewResult Search()
        {
            return PartialView("_SearchUsersViewPartial");
        }

        [HttpPost]
        [OutputCache(Duration = 90)]
        public PartialViewResult Search(string name)
        {
            var customerResultsViewModel = new Models.CustomerResultsViewModel();

            CustomersDatabaseDataContext db = new CustomersDatabaseDataContext();

            if (name == "")
            {
                var customers = from a in db.Customers
                                select a;

                var parsingSearchResults = customers.Select(customer =>
                    new Models.CustomerViewModel
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email
                    });

                var customerSearchResults = new List<Models.CustomerViewModel> { };

                foreach (var customer in parsingSearchResults)
                {
                    customerSearchResults.Add(customer);
                }

                customerResultsViewModel.CustomerSearchResults = customerSearchResults;
            }
            else
            {

                var customers = from a in db.Customers.
                            Where(p => p.FirstName == name)
                                select a;

                var parsingSearchResults = customers.Select(customer =>
                    new Models.CustomerViewModel
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email
                    });

                var customerSearchResults = new List<Models.CustomerViewModel> { };

                foreach (var customer in parsingSearchResults)
                {
                    customerSearchResults.Add(customer);
                }

                customerResultsViewModel.CustomerSearchResults = customerSearchResults;  
            }
            return PartialView("_UserResultsViewPartial", customerResultsViewModel);
        }
    }
}