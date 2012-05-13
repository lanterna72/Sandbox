using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreConcepts.Models;
using SisoDb;

namespace CoreConcepts.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ISession _dbSession;

        public CustomerController(ISession dbSession)
        {
            _dbSession = dbSession;
        }

        public ActionResult Add()
        {
            var customer = new Customer();

            return View("Add", customer);
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            _dbSession.Insert<Customer>(customer);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var customers = _dbSession
                .Query<Customer>()
                .ToArrayOf(c => new Customer
                {
                    Id = c.Id,
                    Name = c.Name
                });

            return View(customers);
        }
    }
}
