using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostel.API.Utils
{
    public class PriceCategoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPriceCategoryService>().To<PriceCategoryService>();
        }
    }
}