using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Mvc;

namespace SugarFactory.WebApi.Controllers
{
    
    [AllowAnonymous]
  
    public class HomeController : Controller
    {
        [HttpGet]
        [Route]
         public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
}
