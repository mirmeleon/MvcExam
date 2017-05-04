using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SugarFactor.WebApi.Controllers;
using SugarFactory.Data;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.Enums;
using SugarFactory.Models.ViewModels.Orders;
using SugarFactory.Services;

namespace SugarFactory.ApiTests.Services
{
    [TestClass]
    public class OrdersServiceTests
    {
        private IOrdersService _service;
        private IEnumerable<Order> _orders;
        private HttpConfiguration _config;
        private SugarFactoryContext _context;

        [TestInitialize]
        public void Init()
        {
            this._service = new OrdersService();
            ConfigureAutoMapper();
            this._context = new SugarFactoryContext();
        }

        [TestMethod]
        public void GetOrders_ShouldReturn_EnumerationWithTheSameCount()
        {
            IEnumerable<Order> orders = this._context.Orders;

            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<SugarFactoryContext>();
            mockContext.Setup(m => m.Orders).Returns(mockSet.Object);

            var serviceMock = new Mock<IOrdersService>();
            IEnumerable<OrderViewModel> mappedOrders =
                Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

            serviceMock.Setup(s => s.GetOrders()).Returns(mappedOrders);

            var result = this._service.GetOrders();

            Assert.AreEqual(mappedOrders.Count(), result.Count());

        }

        [TestMethod]
        public void GetOrderToEdit_ShouldReturn_OrderWithGivvenId()
        {
            var mockedOrder = CreateFakeOrder();
            mockedOrder.Object.Id = 18;

            OrderViewModel vm = Mapper.Map<Order, OrderViewModel>(mockedOrder.Object);

            var result = _service.GetOrder(18);

            Assert.AreEqual(vm.Id, result.Id);
        }

        [TestMethod]
        public void EditOrder_ShouldReturn_EditedOrder()
        {
            var mockedOrder = CreateFakeOrder();
            mockedOrder.Object.Id = 18;

            var changedOrder = new Mock<EditOrderBm>();
            changedOrder.Object.OrderDate = DateTime.Today + TimeSpan.FromDays(2);
            changedOrder.Object.OrderStatus = OrderStatus.Ordered;
            changedOrder.Object.PaperKg = 123;

            _service.EditOrder(mockedOrder.Object.Id, changedOrder.Object);

            Assert.AreNotEqual(mockedOrder.Object.PaperKg, changedOrder.Object.PaperKg);
            Assert.AreNotEqual(mockedOrder.Object.OrderDate, changedOrder.Object.OrderDate);
            Assert.AreEqual(mockedOrder.Object.OrderStatus, changedOrder.Object.OrderStatus);

        }

        [TestMethod]
        public void AddNewOrder_ShouldReturn_OrderContextWithOneMoreOrder()
        {
            int originalNumber = this._context.Orders.Count();
            int orderId = this._context.SugarSachets.FirstOrDefault().Id;


            var newOrderBm = new Mock<NewOrderBm>();
            newOrderBm.Object.PaperKg = 234;
            newOrderBm.Object.Id = orderId;
            
            _service.AddNewOrder(newOrderBm.Object);

            int ordersAfterInsert = _context.Orders.Count();

            Assert.AreEqual(originalNumber + 1, ordersAfterInsert);


        }

        private Mock<Order> CreateFakeOrder()
        {
            var mockedOrder = new Mock<Order>();

            mockedOrder.Object.ClientPrefix = "TR";
            mockedOrder.Object.OrderDate = DateTime.Today;
            mockedOrder.Object.OrderStatus = OrderStatus.Ordered;
            mockedOrder.Object.PaperKg = 11;
            mockedOrder.Object.SachetUniqueNumber = "TR/2/22";
            return mockedOrder;
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