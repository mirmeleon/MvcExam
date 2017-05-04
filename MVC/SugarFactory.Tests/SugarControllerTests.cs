using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SugarFactory.Data;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Sugar;
using SugarFactory.Services;
using SugarFactory.Services.Contracts;
using SugarFactory.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace SugarFactory.Tests
{
    [TestClass]
    public class SugarControllerTests
    {
        private SugarController _controller;
        private ISugarService _service;

        [TestInitialize]
        public void Init()
        {

            this._service = new SugarService();
            this._controller = new SugarController(_service);
            ConfigureAutoMapper();
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(expression =>
            {
                //bm
                expression.CreateMap<MakeSachetBm, SugarSachet>()
                   .ForMember(dest => dest.ImageUrl,
                       opts => opts.Ignore())
                   .ForMember(dest => dest.PdfUrl,
                       op => op.Ignore())
                          .ForMember(dest => dest.ClientPrefix, ost => ost.Ignore());

                //vm
                expression.CreateMap<ClientPrefix, ClientPrefixViewModel>();
                expression.CreateMap<SugarSachet, AllSachetsViewModel>();
            });
        }

        [TestMethod]
        public void AllWithAdminLogged_ShouldReturn_DefaultViewWithIEnumerableFromAllSachetsViewModel()
        {
            var loggedUserMock = new Mock<IPrincipal>();
            loggedUserMock.Setup(x => x.Identity.Name).Returns("deni@abv.bg");
            loggedUserMock.Setup(x => x.IsInRole("Admin")).Returns(true);


            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(loggedUserMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            this._controller.ControllerContext = controllerContextMock.Object;

            _controller.WithCallTo(c => c.All(1)).ShouldRenderDefaultView()
                .WithModel<IEnumerable<AllSachetsViewModel>>();
        }

        [TestMethod]
        public void AllWithSugarUserLogged_ShouldReturn_DefaultViewWithIEnumerableFromAllSachetsViewModel()
        {
            string email = string.Empty;
            using (var ctx = new SugarFactoryContext())
            {
                var sugarUser = ctx.SugarUsers.FirstOrDefault(u => u.User.Email != "deni@abv.bg");
                email = sugarUser.User.Email;
            }
            var loggedUserMock = new Mock<IPrincipal>();
            loggedUserMock.Setup(x => x.Identity.Name).Returns(email);
            loggedUserMock.Setup(x => x.IsInRole("Admin")).Returns(false);


            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User).Returns(loggedUserMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            this._controller.ControllerContext = controllerContextMock.Object;

            _controller.WithCallTo(c => c.All(1)).ShouldRenderDefaultView()
                .WithModel<IEnumerable<AllSachetsViewModel>>();
        }


        [TestMethod]
        public void MakeSachet_ShouldReturn_DefaultViewWithMakeSachetViewModel()
        {

            _controller.WithCallTo(c => c.MakeSachet()).ShouldRenderDefaultView()
                .WithModel<MakeSachetViewModel>();
        }

        [TestMethod]
        public void MakeSachetWithPostMethodWithModelStateNotValid_ShouldRedirect_ToMakeSachetWithGetMethod()
        {

            _controller.ModelState.AddModelError("ImgFile", "can't be null");
            var mockedBm = new Mock<MakeSachetBm>();
            mockedBm.SetupAllProperties();
            mockedBm.Object.ClientPrefix = "DE";
            mockedBm.Object.FirstColor = "pink";

            _controller.WithCallTo(c => c.MakeSachet(mockedBm.Object))
                .ShouldRedirectTo(r => r.MakeSachet);
        }
    }
}
