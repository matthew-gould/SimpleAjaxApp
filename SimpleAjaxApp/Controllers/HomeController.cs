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
        //[OutputCache(Duration = 90)]
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
        //[OutputCache(Duration = 90)]
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
                        ID = customer.Id,
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
                        ID = customer.Id,
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

        [HttpGet]
        [OutputCache(Duration = 30)]
        public PartialViewResult Add()
        {
            return PartialView("_AddUserViewPartial");
        }

        [HttpPost]
        //[OutputCache(Duration = 90)]
        [ActionName("Add")]
        public PartialViewResult AddCustomer(string firstName, string lastName, string email)
        {
            using (CustomersDatabaseDataContext db = new CustomersDatabaseDataContext())
            {
                Customer customer = new Customer();
                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.Email = email;

                db.Customers.InsertOnSubmit(customer);
                db.SubmitChanges();
            };

                return PartialView("_AddUserResultsViewPartial");
        }

        [HttpPost]
        [ActionName("Delete")]
        public ViewResult DeleteCustomer(string customer_id)
        {
            int person_id = int.Parse(customer_id);
            using (CustomersDatabaseDataContext db = new CustomersDatabaseDataContext())
            {
                var customer = from a in db.Customers.
                               Where(p => p.Id == person_id)
                               select a;

                foreach (var person in customer)
                {
                    db.Customers.DeleteOnSubmit(person);
                }

                db.SubmitChanges();
            };

            return View("Index");
        }
    }
}