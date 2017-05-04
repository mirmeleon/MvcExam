using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.ViewModels.Sugar;
using SugarFactory.Services;


namespace SugarFactor.WebApi.Controllers
{
    [RoutePrefix("api/v01/sugar")]
    public class SugarController : ApiController
    {
        private ISugarService _service;
       
        public SugarController()
        {
           
            this._service = new SugarService();
        }

        /// <summary>
        /// Get all sugar sachets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route]
        [ResponseType(typeof(IEnumerable<AllSachetsViewModel>))]
        public IHttpActionResult SugarSachets()
        {
            if (this._service.GetAllSachet() == null)
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }

            IEnumerable<AllSachetsViewModel> sachetsVm = this._service.GetAllSachet();

           return this.Ok(sachetsVm);
         

        }

        /// <summary>
        /// Creates new sugar sachet
        /// </summary>
        /// <param name="makeSachetBm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createSachet")]
        public IHttpActionResult CreateSachet([FromBody]MakeSachetBm makeSachetBm)
        {
           
            if (!this.ModelState.IsValid)
            {
                
                return this.StatusCode(HttpStatusCode.BadRequest);
            }

            this._service.CreateSachet(makeSachetBm);
            
            return StatusCode(HttpStatusCode.Created);
        }


    }
}