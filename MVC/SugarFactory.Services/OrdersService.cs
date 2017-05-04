using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using SugarFactory.Models.EntityModels;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.Enums;
using SugarFactory.Models.ViewModels.Orders;
using SugarFactory.Services.Contracts;


namespace SugarFactory.Services
{
    [HandleError(ExceptionType = typeof(ArgumentNullException), View = "Error")]
    public class OrdersService : Service, IOrdersService
   {
        public IEnumerable<OrderViewModel> GetOrders(ControllerContext contContext)
        {
            var userId = contContext.HttpContext.User.Identity.GetUserId();
           
            SugarUser activeSugarUser = this.Context.SugarUsers.FirstOrDefault(u => u.User.Id == userId);
            var cont = contContext.HttpContext.User;
            IEnumerable<Order> orders;
          
            

            if (cont.IsInRole("Admin") || cont.IsInRole("Manager"))
            {
                orders = this.Context.Orders;
            }
            else
            {
                 orders = this.Context.Orders.Where(c => c.ClientPrefix == activeSugarUser.ClientPrefix);
            
            }

            IEnumerable<OrderViewModel> mapped = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);

            return mapped;
           
        }

       
        public OrderViewModel GetOrderToEdit(int orderId)
        {
            Order order = GetOrder(orderId);
            OrderViewModel vm = Mapper.Map<Order, OrderViewModel>(order);

            return vm;
        }

        public void EditOrder(int orderId, EditOrderBm editOrderBm)
        {
            Order order = GetOrder(orderId);
     
            order.OrderDate = editOrderBm.OrderDate;
            order.PaperKg = editOrderBm.PaperKg;
            order.OrderStatus = editOrderBm.OrderStatus;

            this.Context.Orders.AddOrUpdate(order);
            this.Context.SaveChanges();
        }

        public Order GetOrder(int orderId)
        {
            Order order = this.Context.Orders.Find(orderId);
            return order;
        }


        public NewOrderFromExistingSachetViewModel GetOrderSachet(int sachetId)
        {
           SugarSachet sachet = this.Context.SugarSachets.Find(sachetId);
            NewOrderFromExistingSachetViewModel sachetVm =
                Mapper.Map<SugarSachet, NewOrderFromExistingSachetViewModel>(sachet);

            return sachetVm;
        }

        public void AddNewOrder(NewOrderBm newOrderBm)
        {
            SugarSachet sachet = this.Context.SugarSachets.Find(newOrderBm.Id);
            Order order = new Order();
          
            order.OrderDate = DateTime.Today;
            order.PaperKg= newOrderBm.PaperKg;
            order.OrderStatus = OrderStatus.Ordered;
            order.SachetUniqueNumber = sachet.UniqueNumber;
            order.ClientPrefix = sachet.ClientPrefix.PrefixName;

            this.Context.Orders.Add(order);
            this.Context.SaveChanges();
        }
     
        public IPagedList<OrderViewModel> MakePagedList(int? page, ControllerContext contContext )
       {
           IEnumerable<OrderViewModel> orders = GetOrders(contContext);
            var pageNumber = page ?? 1;
            var onePageOfOrders = orders.ToPagedList(pageNumber, 5);
            return onePageOfOrders;
        }
   }
}
