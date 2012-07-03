using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JSSerializerSample.Models;

namespace JSSerializerSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var model = new IndexViewModel
            {
                Message = "Here is a message!",
                UserName = "mpaterson",
                ComplexProperty = new ComplexViewModel 
                { 
                    ComplexProperty1 = "Complex Property1 Value"
                }
            };

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
