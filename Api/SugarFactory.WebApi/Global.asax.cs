using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SugarFactory.Models.BindingModels.AdminArea;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.Enums;
using SugarFactory.Models.ViewModels.AdminArea;
using SugarFactory.Models.ViewModels.Orders;
using SugarFactory.Models.ViewModels.Sugar;

namespace SugarFactory.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureAutoMapper();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.Indent = true;

        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                //bm
                config.CreateMap<MakeSachetBm, SugarSachet>()
                     .ForMember(dest => dest.ImageUrl,
                         opts => opts.Ignore())
                     .ForMember(dest => dest.PdfUrl,
                         op => op.Ignore())
                            .ForMember(dest => dest.ClientPrefix, ost => ost.Ignore());

                config.CreateMap<AssignPrefixBm, SugarUser>()
                    .ForMember(src => src.IsActivated,
                        ost => ost.Ignore())
                        .ForMember(des => des.Id, 
                        ost => ost.MapFrom(
                            src => src.Id));



                //vm

                config.CreateMap<SugarSachet, PreviewSachetViewModel>();

                config.CreateMap<Order, OrderViewModel>()
                    .ForMember(dest => dest.OrderDate,
                        opt => opt.MapFrom(
                            src => src.OrderDate.Date.ToString("dd/MM/yyyy")));

                config.CreateMap<ApplicationUser, UsersViewModel>();

                config.CreateMap<SugarSachet, AllSachetsViewModel>()
                .ForMember(dest => dest.ClientPrefix,
                opt => opt.MapFrom(
                    src => src.ClientPrefix.PrefixName));


                config.CreateMap<SugarSachet, NewOrderFromExistingSachetViewModel>()
                .ForMember(dest => dest.ClientPrefix, opt => opt.MapFrom(
                      src => src.ClientPrefix.PrefixName))
                    .ForMember(dest => dest.OrderDate,
                    opt => opt.MapFrom(
                        src => DateTime.Today))
                        .ForMember(dest => dest.OrderStatus,
                        opt => opt.MapFrom(
                            src => OrderStatus.Ordered));
                config.CreateMap<SugarUser, AllUsersPrefixViewModel>()
                .ForMember(dest => dest.Email,
                ost => ost.MapFrom(
                    src => src.User.Email));

                config.CreateMap<ClientPrefix, ClientPrefixViewModel>();
                config.CreateMap<ClientPrefix, CreatePrefixViewModel>();

            });
        }
    }
}
