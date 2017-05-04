using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using SugarFactory.Models.EntityModels;
using AutoMapper;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.Enums;
using SugarFactory.Models.ViewModels.Orders;


namespace SugarFactory.Services
{
   public class OrdersService : Service, IOrdersService
   {
       
        
        public IEnumerable<OrderViewModel> GetOrders()
        {
            IEnumerable<Order> orders;
            orders = this.Context.Orders;
            
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

   
        public void AddNewOrder(NewOrderBm newOrderBm)
        {
            SugarSachet sachet = this.Context.SugarSachets.Find(newOrderBm.Id);
            Order order = new Order();
          
            order.OrderDate = DateTime.Today;
            order.PaperKg = newOrderBm.PaperKg;
            order.OrderStatus = OrderStatus.Ordered;
            order.SachetUniqueNumber = sachet.UniqueNumber;
            order.ClientPrefix = sachet.ClientPrefix.PrefixName;

            this.Context.Orders.Add(order);
            this.Context.SaveChanges();
        }
      
   }
}
