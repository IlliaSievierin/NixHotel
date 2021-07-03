using AutoMapper;
using Hostel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hostel.API.Controllers
{
    public class CustomerController : ApiController
    {
        private ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }
       
        public IEnumerable<CustomerModel> Get()
        {
            var mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();

            var data = service.GetAll();
            var customers = mapper.Map<IEnumerable<CustomerDTO>, List<CustomerModel>>(data);
            return customers;
        }

  
        [ResponseType(typeof(CustomerModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
             var mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();
           

            CustomerDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var customer = mapper.Map<CustomerDTO, CustomerModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, customer);
        }

     
        public void Post([FromBody] CustomerModel value)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CustomerModel, CustomerDTO>()).CreateMapper();

            service.Create(mapper.Map<CustomerModel, CustomerDTO>(value));
        }

       

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
