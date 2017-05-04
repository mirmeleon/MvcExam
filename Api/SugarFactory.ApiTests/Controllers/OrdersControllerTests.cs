using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SugarFactor.WebApi.Controllers;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Orders;


namespace SugarFactory.ApiTests
{
    [TestClass]
   public class OrdersControllerTests
    {
        private OrdersController _controller;
        private IEnumerable<Order> _orders;
        private HttpConfiguration _config;

        [TestInitialize]
        public void Init()
        {
            this._controller = new OrdersController();
            _config = new HttpConfiguration();
            this._controller.Configuration = _config;
            ConfigureAutoMapper();
        }

        [TestMethod]
        public void Orders_ShouldReturn_OkWithIEnumerableFromOrderViewModel()
        {
            var result = _controller.Orders()  as OkNegotiatedContentResult<IEnumerable<OrderViewModel>>;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditOrder_ShouldReturn_BadRequestWhenModelIsNotValid()
        {
            var editOrderBm = new Mock<EditOrderBm>();
            editOrderBm.SetupAllProperties();
            editOrderBm.Object.PaperKg = 9;
            editOrderBm.Object.Id = 15;
            editOrderBm.Object.OrderStatus = 0;
            editOrderBm.Object.OrderDate = DateTime.Today;
            
             this._controller.Validate(editOrderBm.Object);

            var result = _controller.EditOrder(15, editOrderBm.Object) as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void EditOrder_ShouldReturn_CreatedWhenModelIsValid()
        {
            var editOrderBm = new Mock<EditOrderBm>();
            editOrderBm.SetupAllProperties();
            editOrderBm.Object.PaperKg = 12;
            editOrderBm.Object.Id = 15;
            editOrderBm.Object.OrderStatus = 0;
            editOrderBm.Object.OrderDate = DateTime.Today;

            
            var result = _controller.EditOrder(15, editOrderBm.Object) as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public void CreateOrder_ShouldReturn_BadRequestWhenNewOrderModelIsInvalid()
        {
            var newOrderBm = new Mock<NewOrderBm>();
            newOrderBm.Object.PaperKg = 9;

            this._controller.Validate(newOrderBm.Object);

            var result = _controller.CreateOrder(newOrderBm.Object) as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void CreateOrder_ShouldReturn_CreatedWhenNewOrderModelIsValid()
        {
            var newOrderBm = new Mock<NewOrderBm>();
            newOrderBm.Object.PaperKg = 10;
            newOrderBm.Object.Id = 25;
           this._controller.Validate(newOrderBm.Object);

            var result = _controller.CreateOrder(newOrderBm.Object) as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }


        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Order, OrderViewModel>()
                   .ForMember(dest => dest.OrderDate,
                       opt => opt.MapFrom(
                           src => src.OrderDate.Date.ToString("dd/MM/yyyy")));


            });
        }
    }
}
