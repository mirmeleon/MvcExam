using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;
using SugarFactory.Data;
using SugarFactory.Models.BindingModels.AdminArea;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.AdminArea;
using SugarFactory.Services;
using SugarFactory.Services.Contracts;
using SugarFactory.Web.Areas.Admin.Controllers;
using TestStack.FluentMVCTesting;

namespace SugarFactory.Tests
{
    
    [TestClass]
    public class AdminControllerTests
    {
        private AdminController _controller;
        private List<UsersViewModel> _users;
        private SugarFactoryContext _context;
        private IEnumerable<IdentityRole> _adminRole;
        private string _adminRoleId;
        private IdentityRole _admins;
        private IEnumerable<IdentityUserRole> _oneAdmin;
        private string _adminMail;
        private string _adminId;

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<ApplicationUser, UsersViewModel>();
                expression.CreateMap<ApplicationUser, UserAndRoleViewModel>();
                expression.CreateMap<ClientPrefix, CreatePrefixViewModel>();
            });
        }

        [TestInitialize]
        public void Init()
        {
            this.ConfigureAutoMapper();


            this._controller = new AdminController(new AdminService());
            this._context = new SugarFactoryContext();
            this._users = CreateListOfUSers();
            this._adminRole = this._context.Roles.Where(r => r.Name == "Admin");
            this._adminRoleId = this._context.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
            this._admins = this._context.Roles.FirstOrDefault(g => g.Id == _adminRoleId);
            this._oneAdmin = _admins.Users.Take(1);

            this._adminId = string.Empty;

            foreach (var id in _oneAdmin)
            {
                _adminId = id.UserId;
            }

            this._adminMail = this._context.Users.FirstOrDefault(m => m.Id == _adminId).Email;

        }




        [TestMethod]
        public void Index_ShouldReturn_DefaultViewWithVm()
        {

            _controller.WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView()
                .WithModel<IPagedList<UsersViewModel>>();
        }

        [TestMethod]
        public void SetRoleWithLoggedInAdmin_ShouldReturn_ViewWithUserAndRoleViewModel()
        {

            var logedUserMock = new Mock<IPrincipal>();
            logedUserMock.Setup(p => p.IsInRole("Admin")).Returns(true);


            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                .Returns(logedUserMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            this._controller.ControllerContext = controllerContextMock.Object;


            _controller.WithCallTo(c => c.SetRole(_adminId))
                .ShouldRenderDefaultView().WithModel<UserAndRoleViewModel>();

        }

        [TestMethod]
        public void SetRoleWithLoggedInNotAdmin_ShouldReturn_ViewWithUserAndRoleViewModel()
        {
            var notAdminRoleId = this._context.Roles.FirstOrDefault(r => r.Name == "SugarUser").Id;
            var sugarUsers = this._context.Roles.FirstOrDefault(g => g.Id == notAdminRoleId);
            var oneUser = sugarUsers.Users.Take(1);

            var notAdmId = string.Empty;

            foreach (var id in oneUser)
            {
                notAdmId = id.UserId;
            }



            var loggedUserMock = new Mock<IPrincipal>();
            loggedUserMock.Setup(p => p.IsInRole("Admin")).Returns(false);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(loggedUserMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            this._controller.ControllerContext = controllerContextMock.Object;

            _controller.WithCallTo(c => c.SetRole(notAdmId))
                .ShouldRenderDefaultView().WithModel<UserAndRoleViewModel>();


        }

        [TestMethod]
        public void SetRoleWithLoggedInAdmin_ShouldReturn_ViewWithSpecificUserAndRoleViewModel()
        {
            var loggedUserMock = new Mock<IPrincipal>();
            loggedUserMock.Setup(p => p.IsInRole("Admin")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(loggedUserMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            this._controller.ControllerContext = controllerContextMock.Object;


            var model = new Mock<UserAndRoleViewModel>();
            model.Object.UserId = this._adminId;
            model.Object.Roles = this._adminRole;
            model.Object.Email = this._adminMail;

            var mockedService = new Mock<IAdminService>();
            mockedService.SetupAllProperties();
            mockedService.Setup(s => s.MakeUserChanges(_adminId)).Returns(model.Object);
            var userVm = mockedService.Object.MakeUserChanges(_adminId);

            _controller.WithCallTo(c => c.SetRole(_adminId)).ShouldRenderDefaultView()
                .WithModel<UserAndRoleViewModel>(m =>
                {
                    m.UserId = userVm.UserId;
                    m.Email = userVm.Email;
                    m.Roles = userVm.Roles;
                });


        }

        [TestMethod]
        public void SetRoleWithPostWithoutErrors_Should_RedirectToIndex()
        {
            var loggedUserMock = new Mock<IPrincipal>();
            loggedUserMock.Setup(p => p.IsInRole("Admin")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(loggedUserMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            this._controller.ControllerContext = controllerContextMock.Object;



            var editRoleBm = new Mock<EditRoleBm>();
            editRoleBm.SetupAllProperties();
            editRoleBm.Object.UserId = _adminId;
            editRoleBm.Object.RoleId = _adminRoleId;

            _controller.WithCallTo(c => c.SetRole(editRoleBm.Object))
                .ShouldRedirectTo(c => c.Index(0));


        }

        [TestMethod]
        public void SetRoleWithBmErrors_Should_RedirectToSetRoleWithStringId()
        {
            _controller.ModelState.AddModelError("UserId", "invalid id");

            var editRoleBm = new Mock<EditRoleBm>();

            _controller.WithCallTo(c => c.SetRole(editRoleBm.Object))
                .ShouldRedirectTo(c => c.SetRole("2232"));

        }

        [TestMethod]
        public void ViewRoles_Should_ReturnDefaultViewWithUserWithAssignedRoleViewModel()
        {

            var id = this._context.Users.FirstOrDefault(u => u.Id != _adminId).Id;
            _controller.WithCallTo(c => c.ViewRoles(id))
                .ShouldRenderDefaultView().WithModel<UserWithAssignedRoleViewModel>();
        }

        [TestMethod]
        public void ViewRoles_Should_RedirectToActionIndex()
        {
            var user = this._context.SugarUsers.FirstOrDefault(u => u.User.Email != "deni@abv.bg");
            var mockedBm = new Mock<DeleteRoleBm>();
            mockedBm.SetupAllProperties();
            mockedBm.Object.UserEmail = user.User.Email;
            mockedBm.Object.RolesName = "SugarUser";

            _controller.WithCallTo(c => c.ViewRoles(mockedBm.Object))
                .ShouldRedirectTo(c => c.Index(0));
        }

        [TestMethod]
        public void LoggedAdmin_Role_ShouldReturn_LoggedUserInRoleAdmin()
        {

            var logedUserMock = new Mock<IPrincipal>();


            logedUserMock.Object.IsInRole("Admin");


            logedUserMock.Verify(p => p.IsInRole("Admin"), Times.Once);
        }


        [TestMethod]
        public void AssignPrefix_Should_ReturnViewWithAllUsersWithAllPrefixesVm()
        {

            _controller.WithCallTo(c => c.AssignPrefix())
                .ShouldRenderDefaultView()
                .WithModel<AllUsersWithAllPrefixesVm>();
        }

        [TestMethod]
        public void AssignPrefixWithPostMethod_Should_RedirectToAssignPrefixWithGetParameter()
        {
            var mockedBm = new Mock<AssignPrefixBm>();
            mockedBm.SetupAllProperties();
            mockedBm.Object.ClientPrefix = "DD";
            mockedBm.Object.Id = 2;

            _controller.WithCallTo(c => c.AssignPrefix(mockedBm.Object))
                .ShouldRedirectTo(c => c.AssignPrefix);
        }

        [TestMethod]
        public void CreatePrefix_Should_ReturnDefaultViewWithViewModel()
        {
            var mockedVm = new Mock<CreatePrefixViewModel>();
            _controller.WithCallTo(c => c.CreatePrefix())
                .ShouldRenderDefaultView()
                .WithModel<CreatePrefixViewModel>();
        }

        [TestMethod]
        public void CreatePrefixWithPost_Should_ReturnDefaultView()
        {

            var mockedBm = new Mock<CreatePrefixBm>();
            mockedBm.SetupAllProperties();
            mockedBm.Object.PrefixName = "KO";

            _controller.WithCallTo(c => c.CreatePrefix(mockedBm.Object))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Update_Should_ReturnPartialView()
        {

            _controller.WithCallTo(c => c.Update())
                .ShouldRenderPartialView("_NewPrefixPartial")
                .WithModel<IEnumerable<CreatePrefixViewModel>>();

        }

        private List<UsersViewModel> CreateListOfUSers()
        {
            this._users = new List<UsersViewModel>()
            {
                new UsersViewModel()
                {
                    Id = "1",
                    Email = "deni@abv.bg"
                },
                new UsersViewModel()
                {
                    Id = "2",
                    Email = "kori@abv.bg"
                }
            };
            return _users;
        }
    }
}