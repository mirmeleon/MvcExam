using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using SugarFactory.Models.BindingModels.AdminArea;
using SugarFactory.Models.ViewModels.AdminArea;
using SugarFactory.Services.Contracts;
using SugarFactory.Web.Attributes;

namespace SugarFactory.Web.Areas.Admin.Controllers
{
    [SugarAuthorize(Roles = "Admin, Manager")]
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            this._service = service;
        }

       
        [HttpGet]
        [Route]
        public ActionResult Index(int? page)
        {
            IEnumerable<UsersViewModel> vm = this._service.GetUsersAndRoles();
            IPagedList<UsersViewModel> vmPaged = this._service.MakePagedList(vm, page);
            ViewBag.OnePageOFUsers = vmPaged;
            return View(vmPaged);
        }


       
        [HttpGet]
        [Route("SetRole")]
        public ActionResult SetRole(string id)
        {
            UserAndRoleViewModel mappedUser;
            if(User.IsInRole("Admin"))
            {
                mappedUser = this._service.MakeUserChanges(id);
                return View(mappedUser);
            }
            else
            {
                mappedUser = this._service.MakeUserChangesWithoutAdminRole(id);
                return View(mappedUser);
            }
            
        }

        [HttpPost]
        [Route("SetRole")]
        [ValidateAntiForgeryToken]
        public ActionResult SetRole(EditRoleBm editRoleBm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("SetRole");
            }

            this._service.SetUserRole(editRoleBm);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Route("ViewRoles")]
        public ActionResult ViewRoles(string id)
        {

            UserWithAssignedRoleViewModel assignedRoles = this._service.ViewAssignedRoles(id);

            return this.View(assignedRoles);
        }



        [HttpPost]
        [Route("ViewRoles")]
        [ValidateAntiForgeryToken]
        public ActionResult ViewRoles(DeleteRoleBm deleteRoleBm)
        {

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("ViewRoles"); 

            }

            this._service.DeleteRole(deleteRoleBm);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Route("AssignPrefix")]
        public ActionResult AssignPrefix()
        {
           AllUsersWithAllPrefixesVm usersAndPrefixes = this._service.GetNotAssignedPrefixesAndUsers();
           return this.View(usersAndPrefixes);
        }

        [HttpPost]
        [Route("AssignPrefix")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignPrefix(AssignPrefixBm assignPrefixBm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("AssignPrefix");
            }

          this._service.AssignPrefixes(assignPrefixBm);
          return this.RedirectToAction("AssignPrefix");
           
        }

        [HttpGet]
        [Route("CreatePrefix")]
        public ActionResult CreatePrefix()
        {
            CreatePrefixViewModel vm = new CreatePrefixViewModel();
          
            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreatePrefix")]
        public ActionResult CreatePrefix(CreatePrefixBm createPrefixBm)
        {
            this._service.CreatePrefix(createPrefixBm);
            return this.View();
        }

        public ActionResult Update()
        {
            IEnumerable<CreatePrefixViewModel> prefixes = this._service.GetAllPrefixes();
            return this.PartialView("_NewPrefixPartial", prefixes);

        }

     
    }
}