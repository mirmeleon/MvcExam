using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SugarFactory.Web.Controllers;

namespace SugarFactory.Tests
{
    [TestClass]
   public class ErrorControllerTests
    {
        [TestMethod]
        public void NotFound_ShouldReturn_500()
        {
             // // arrange    
            ErrorController controller = new ErrorController();
            var request = new HttpRequest("", "http://localhost", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            // act
            var actual = controller.NotFound();

            // assert
            Assert.AreEqual(500, response.StatusCode);
           
         }

        [TestMethod]
        public void NotFound_ShouldReturn_StatusDescription()
        {
            // arrange    
            ErrorController controller = new ErrorController();
            var request = new HttpRequest("", "http://localhost", "");
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);

            // act
            var actual = controller.NotFound();
            //assert
            Assert.AreEqual("Ooops, you just entered in the land of Unicorns!", response.StatusDescription);
        }

        //todo da go opravia ako mi ostane vreme, dava mi null response-a
        //[TestMethod]    
        //public void NotFound_ShouldReturn_DefaultView()
        //{
        //    ErrorController controller = new ErrorController();
        //    var result = controller.NotFound() as ViewResult;
        //    Assert.AreEqual("NotFound", result.ViewName);
        //}
    }
}
