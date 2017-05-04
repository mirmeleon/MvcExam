using System.Collections.Generic;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Orders;

namespace SugarFactory.Services
{
    public interface IOrdersService
    {
        IEnumerable<OrderViewModel> GetOrders();
        OrderViewModel GetOrderToEdit(int orderId);
        void EditOrder(int orderId, EditOrderBm editOrderBm);
        Order GetOrder(int orderId);
   
        void AddNewOrder(NewOrderBm newOrderBm);
     
    }
}