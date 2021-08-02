using Hotel.BLL.Infrastructure;
using Hotel.WEB.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hotel.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule customerModule = new CustomerModule();
            NinjectModule categoryModule = new CategoryModule();
            NinjectModule priceCaregoryModule = new PriceCategoryModule();
            NinjectModule reservationModule = new ReservationModule();
            NinjectModule roomModule = new RoomModule();
            NinjectModule employeeModule = new EmployeeModule();
            NinjectModule dependencyModule = new DependencyModule("HotelDBModel");
            var kernel = new StandardKernel(categoryModule, customerModule, dependencyModule, priceCaregoryModule, reservationModule, roomModule, employeeModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
