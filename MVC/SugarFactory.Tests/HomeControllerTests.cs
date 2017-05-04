using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SugarFactory.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace SugarFactory.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void Init()
        {
            this._controller = new HomeController();

        }

        [TestMethod]
        public void Index_ShouldReturn_DefaultView()
        {
            _controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void About_ShouldReturn_DefaultView()
        {

            _controller.WithCallTo(c => c.About()).ShouldRenderDefaultView();
        }



        [TestMethod]
        public void Contact_ShouldReturn_DefaultView()
        {

            _controller.WithCallTo(c => c.Contact()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Contact_ShouldReturn_ViewBagMessage()
        {
            string message = "More information you can receive by phone or email.";
            var result = _controller.Contact() as ViewResult;
            Assert.AreEqual(message, _controller.ViewBag.Message);
        }

    }
}
