using System.Web.Mvc;

namespace SugarFactory.Web.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult NotFound()
        {
          
            this.Response.StatusCode = 500;
            Response.StatusDescription = "Ooops, you just entered in the land of Unicorns!";
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}