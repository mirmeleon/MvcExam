using System.Net;
using System.Collections.Generic;
using System.Web.Http;
using SugarFactory.Models.ViewModels.Sugar;
using SugarFactory.Services;

namespace SugarFactory.WebApi.Controllers
{
    [RoutePrefix("api/v01/sugar")]
    public class SugarApiController : ApiController
    {
        private ISugarService service;

        public SugarApiController()
        {
            this.service = new SugarService();
        }

        

        [HttpGet]
        [Route("Sachets")]
        public IHttpActionResult Sachets()
        {
            if (this.service.GetAllSachet(User) == null)
            {
                return this.NotFound();
            }

            IEnumerable<AllSachetsViewModel> sachetsVm = this.service.GetAllSachet(User);

            return this.Ok(sachetsVm);
        }
    }
}