using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
//using ClientPrefix = SugarFactory.Models.EntityModels.ClientPrefix;

namespace SugarFactory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureAutoMapper();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
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
                           .ForMember(dest=>dest.ClientPrefix, ost=>ost.Ignore());

                expression.CreateMap<AssignPrefixBm, SugarUser>()
                    .ForMember(src => src.IsActivated,
                        ost => ost.Ignore())
                        .ForMember(des=>des.Id,
                        ost=>ost.MapFrom(
                            src=>src.Id));


                //vm 

                expression.CreateMap<SugarSachet, PreviewSachetViewModel>();

                expression.CreateMap<Order, OrderViewModel>()
                    .ForMember(dest => dest.OrderDate,
                        opt => opt.MapFrom(
                            src => src.OrderDate.Date.ToString("dd/MM/yyyy"))); 

                expression.CreateMap<ApplicationUser, UsersViewModel>();
            
                expression.CreateMap<SugarSachet, AllSachetsViewModel>()
                .ForMember(dest=>dest.ClientPrefix,
                opt=>opt.MapFrom(
                    src=>src.ClientPrefix.PrefixName));

             
                expression.CreateMap<SugarSachet, NewOrderFromExistingSachetViewModel>()
                .ForMember(dest=>dest.ClientPrefix, opt=>opt.MapFrom(
                    src=>src.ClientPrefix.PrefixName))
                    .ForMember(dest=>dest.OrderDate,
                    opt=>opt.MapFrom(
                        src=> DateTime.Today))
                        .ForMember(dest=>dest.OrderStatus, 
                        opt=>opt.MapFrom(
                            src=>OrderStatus.Ordered));
                expression.CreateMap<SugarUser, AllUsersPrefixViewModel>()
                .ForMember(dest=>dest.Email, 
                ost=>ost.MapFrom(
                    src=>src.User.Email));

                expression.CreateMap<ClientPrefix, ClientPrefixViewModel>();
                expression.CreateMap<ClientPrefix, CreatePrefixViewModel>();

            });

        }
    }
}
