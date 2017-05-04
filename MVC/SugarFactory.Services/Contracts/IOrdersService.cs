using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Orders;

namespace SugarFactory.Services.Contracts
{
    public interface IOrdersService
    {
        IEnumerable<OrderViewModel> GetOrders(ControllerContext controllerContext);
        OrderViewModel GetOrderToEdit(int orderId);
        void EditOrder(int orderId, EditOrderBm editOrderBm);
        Order GetOrder(int orderId);
        NewOrderFromExistingSachetViewModel GetOrderSachet(int sachetId);
        void AddNewOrder(NewOrderBm newOrderBm);
        IPagedList<OrderViewModel> MakePagedList(int? page, ControllerContext controllerContext);
    }
}