using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.ViewModels.Sugar;
using SugarFactory.Services.Contracts;
using SugarFactory.Web.Attributes;

namespace SugarFactory.Web.Controllers
{
    [RoutePrefix("sugar")]
    [SugarAuthorize(Roles = "SugarUser, Admin, Manager")]
    [HandleError(ExceptionType = typeof(ArgumentNullException), View = "CustomError")]
    public class SugarController : Controller
    {
        private readonly ISugarService _service;

        public SugarController(ISugarService service)
        {
            this._service = service;
        }

       
        
        [HttpGet]
        [Route]
        public ActionResult All(int? page)
        {
         
            IEnumerable<AllSachetsViewModel> sachetsVm = this._service.GetAllSachet(User);
            IPagedList<AllSachetsViewModel> paged = this._service.MakePagedList(sachetsVm, page);
            ViewBag.paged = paged;
            return this.View(paged);
        }

        [HttpGet]
        [Route("makeSachet")]
        [SugarAuthorize(Roles = "Admin, Manager")]
        public ActionResult MakeSachet()
        {
            MakeSachetViewModel makeSachetVm = this._service.GenereteSacheteForm();
            return View(makeSachetVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("makeSachet")]
        [SugarAuthorize(Roles = "Admin, Manager")]
        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "CustomError")]
       
        public ActionResult MakeSachet(MakeSachetBm makeSachetBm)
        {
            if (makeSachetBm.FirstColor == null)
            {
                throw new ArgumentNullException();
            }   

           if (this.ModelState.IsValid)
            {
                this._service.CreateSachet(makeSachetBm);

                return this.RedirectToAction("All", "Sugar");
            }

            return this.RedirectToAction("MakeSachet", "Sugar");
        }

       
    }
}