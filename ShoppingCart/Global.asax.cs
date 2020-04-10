using AutoMapper;
using ShoppingCart.Api.v1.Model;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShoppingCart
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public class AutoMapperConfiguration
        {
            [Obsolete]
            public static void Configure()
            {
                //Mapper.Initialize(cfg =>
                //{
                //    cfg.AddProfile<MapProfile>();

                //    cfg.CreateMap<Product, ProductViewModel>()
                //    .ForMember(c => c.CategoryId, d => d.MapFrom(c => c.Category.Id));
                //    cfg.CreateMap<ProductViewModel, Product>()
                //        .ForMember(c => c.Id, d => d.Ignore());

                //    cfg.CreateMap<Category, CategoryViewModel>()
                //    .ForMember(c => c.Id, d => d.MapFrom(c => c.Id));
                //    cfg.CreateMap<CategoryViewModel, Category>()
                //    .ForMember(c => c.Id, d => d.Ignore());

                //    cfg.CreateMap<CartItem, CartItemViewModel>();
                //    cfg.CreateMap<CartItemViewModel, CartItem>()
                //    .ForMember(c => c.Id, d => d.Ignore());

                //    cfg.CreateMap<Order, OrderViewModel>();
                //    cfg.CreateMap<OrderViewModel, Order>()
                //    .ForMember(c => c.Id, d => d.Ignore());

                //    cfg.CreateMap<OrderDetail, OrderDetailViewModel>();
                //    cfg.CreateMap<OrderDetailViewModel, OrderDetail>()
                //    .ForMember(c => c.Id, d => d.Ignore());
                //});


            }
        }

        internal class MapProfile : AutoMapper.Profile
        {

        }
    }
}
