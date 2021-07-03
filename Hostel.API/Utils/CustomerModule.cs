using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject;
using Ninject.Modules;

namespace Hostel.API.Utils
{
    public class CustomerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerService>().To<CustomerService>();
        }
       
    }
}