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
        private IMapper mapperCustomer;
        private IMapper mapperCustomerReverse;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
            mapperCustomer = new MapperConfiguration(cfg =>
               cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();
            mapperCustomerReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<CustomerModel, CustomerDTO>()).CreateMapper();
        }
       
        public IEnumerable<CustomerModel> Get()
        {
            var data = service.GetAll();
            var customers = mapperCustomer.Map<IEnumerable<CustomerDTO>, List<CustomerModel>>(data);
            return customers;
        }

        [Route("api/GetAllCount")]
        public long GetAllCount()
        {
            var count = Get().Count();

            return count;
        }

        [ResponseType(typeof(CustomerModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            CustomerDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var customer = mapperCustomer.Map<CustomerDTO, CustomerModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, customer);
        }

        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] CustomerModel value)
        {
            service.Create(mapperCustomerReverse.Map<CustomerModel, CustomerDTO>(value));
            return request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            CustomerDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
