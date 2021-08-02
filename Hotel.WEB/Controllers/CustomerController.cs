using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.WEB.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService service;
        private IMapper mapperCustomer;
        private IMapper mapperCustomerReverse;
        private ILogger logger;
   
        public CustomerController(ICustomerService service)
        {
            this.service = service;
            mapperCustomer = new MapperConfiguration(cfg =>
              cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();
            mapperCustomerReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<CustomerModel, CustomerDTO>()).CreateMapper();
            logger = LogManager.GetCurrentClassLogger();
        }
        [Authorize]
        public ActionResult Index()
        {
            var data = mapperCustomer.Map<IEnumerable<CustomerDTO>, IEnumerable<CustomerModel>>(service.GetAll());
            ViewBag.Customers = data;
            return View();
        }
        [Authorize]
        [HttpPost]
        public RedirectResult Add(CustomerModel customer)
        {
            service.Create(mapperCustomerReverse.Map<CustomerModel, CustomerDTO>(customer));
            logger.Info($"{User.Identity.Name} added customer {customer.FirstName} {customer.LastName} {customer.MiddleName}.");
            return Redirect("/Customer/Index");
        }
        [Authorize]
        [HttpGet]
        public RedirectResult Delete(int id)
        {
            CustomerModel customer = mapperCustomer.Map<CustomerDTO, CustomerModel>(service.Get(id));
            service.Delete(id);
            logger.Info($"{User.Identity.Name} deleted customer {customer.FirstName} {customer.LastName} {customer.MiddleName}.");
            return Redirect("/Customer/Index");
        }
    }
}