using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SugarFactory.Models.BindingModels.Orders;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.ViewModels.Orders;
using SugarFactory.Services;


namespace SugarFactor.WebApi.Controllers
{
    [RoutePrefix("api/v01/orders")]
    public class OrdersController : ApiController
    {
        private readonly IOrdersService _service;
       

        public OrdersController()
        {
           
            this._service = new OrdersService();
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route]
        [ResponseType(typeof(IEnumerable<OrderViewModel>))]
        public IHttpActionResult Orders()
        {

            if (this._service.GetOrders() == null)
            {
                return this.NotFound();
            }

            IEnumerable<OrderViewModel> orders = this._service.GetOrders();
            return this.Ok(orders);
        }

        /// <summary>
        /// Shows order to be edited by provided Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route] 
        [ResponseType(typeof(OrderViewModel))]
        public IHttpActionResult EditOrder(int orderId)
        {

          
            if (this._service.GetOrderToEdit(orderId) == null)
            {
                return this.NotFound();
            }

            OrderViewModel orderForEdit = this._service.GetOrderToEdit(orderId);
            return Ok(orderForEdit);
        }

        /// <summary>
        /// Edits order by provided id
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="editOrderBm"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("editOrder")]
        public IHttpActionResult EditOrder(int orderId, [FromBody]EditOrderBm editOrderBm)
        {
            if (!this.ModelState.IsValid)
            {
               return this.StatusCode(HttpStatusCode.BadRequest);
            }

            this._service.EditOrder(orderId, editOrderBm);

            return StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="newOrderBm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createOrder")]
        public IHttpActionResult CreateOrder([FromBody] NewOrderBm newOrderBm)
        {
            if (!this.ModelState.IsValid)
            {
              return this.StatusCode(HttpStatusCode.BadRequest);
            }

            this._service.AddNewOrder(newOrderBm);
         
          return StatusCode(HttpStatusCode.Created);
        }

    }
}