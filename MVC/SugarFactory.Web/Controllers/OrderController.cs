using System;
using System.Web.Mvc;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.ViewModels.Orders;
using SugarFactory.Web.Attributes;
using PagedList;
using SugarFactory.Services.Contracts;


namespace SugarFactory.Web.Controllers
{
    [SugarAuthorize(Roles = "SugarUser, Admin, Manager")]
    [RoutePrefix("order")]
    [HandleError(ExceptionType = typeof(ArgumentNullException), View = "CustomError")]
    public class OrderController : Controller
    {
        private readonly IOrdersService _service;

        public OrderController(IOrdersService service)
        {
            this._service = service;
        }

      
        [HttpGet]
        [Route]
        public ActionResult Orders(int? page)
        {
            IPagedList<OrderViewModel> pagedOrders = this._service.MakePagedList(page, this.ControllerContext);
            ViewBag.OnePageOfOrders = pagedOrders;

            return View(pagedOrders);
        }

        [HttpGet]
        [SugarAuthorize(Roles = "Admin, Manager")]
        [Route("EditOrder/{orderId:int}")] 
        public ActionResult EditOrder(int orderId)
        {

            OrderViewModel orderForEdit = this._service.GetOrderToEdit(orderId);

            return View(orderForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SugarAuthorize(Roles = "Admin, Manager")]
        [Route("EditOrder/{OrderId:int}")] 
        public ActionResult EditOrder(int orderId, EditOrderBm editOrderBm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction($"EditOrder");
            }

            this._service.EditOrder(orderId, editOrderBm);

            return this.RedirectToAction("Orders");
        }

        [HttpGet]
        [Route("NewOrder")]
        public ActionResult NewOrder(int id)
        {
            NewOrderFromExistingSachetViewModel order = this._service.GetOrderSachet(id);
            return this.View(order);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("NewOrder")]
        public ActionResult NewOrder(NewOrderBm newOrderBm)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("NewOrder");
            }

            this._service.AddNewOrder(newOrderBm);
            return this.RedirectToAction("Orders");
        }
    }
}