using Hostel.API.Utils;
using Hotel.BLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Web.WebApi.Filter;

namespace Hostel.API
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

            NinjectModule customerModule = new CustomerModule();
            NinjectModule categoryModule = new CategoryModule();
            NinjectModule priceCaregoryModule = new PriceCategoryModule();
            NinjectModule reservationModule = new ReservationModule();
            NinjectModule roomModule = new RoomModule();
            NinjectModule dependencyModule = new DependencyModule("HotelDBModel");
            var kernel = new StandardKernel(categoryModule, customerModule, dependencyModule, priceCaregoryModule, reservationModule, roomModule);
            kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));
            GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
        }
    }
}
