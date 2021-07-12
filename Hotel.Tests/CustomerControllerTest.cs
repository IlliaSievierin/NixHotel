using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Hotel.BLL.Interfaces;
using Hostel.API.Controllers;
using Hotel.BLL.DTO;
using System.Net.Http;
using AutoMapper;
using Hostel.API.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        private HttpRequestMessage httpRequest;
        private IMapper mapper;

        public CustomerControllerTest()
        {
            httpRequest= new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<CustomerDTO, CustomerModel>()
               ).CreateMapper();

        }

        [TestMethod]
        public void CustomerGetAllResultCorrectTest()
        {
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CustomerDTO>());

            var expected = mapper.Map<IEnumerable<CustomerDTO>, IEnumerable<CustomerModel>>(mock.Object.GetAll());

            CustomerController controller = new CustomerController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void CustomerGetTest()
        {
            var id = 0;
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.Get(id)).Returns(new CustomerDTO());
           
            var expected = mapper.Map<CustomerDTO, CustomerModel>(mock.Object.Get(id));

            CustomerController controller = new CustomerController(mock.Object);
            
            var result = controller.Get(httpRequest, id).Content.ReadAsAsync<CustomerModel>();
           
            Assert.AreEqual(expected, result.Result);
           
        }
        [TestMethod]
        public void CustomerPostTest()
        {
            CustomerDTO customerModel = new CustomerDTO()
            {
                LastName = "Test",
                FirstName = "Test",
                MiddleName = "Test",
                DateOfBirth = new DateTime(1999, 2, 2),
                Passport = "193849374"
            };
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.Create(customerModel));

            CustomerController controller = new CustomerController(mock.Object);

            var result = controller.Post(httpRequest, mapper.Map<CustomerDTO, CustomerModel>(customerModel)).StatusCode;
            
            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void CustomerDeleteTest()
        {
            int id = 1;
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.Delete(id));

            CustomerController controller = new CustomerController(mock.Object);

            var result = controller.Delete(httpRequest, id).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void CustomerGetCountTest()
        {
            
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.GetAll());

            var expected = mock.Object.GetAll().Count();

            CustomerController controller = new CustomerController(mock.Object);

            var result = controller.GetAllCount();

            Assert.AreEqual(expected, result);

        }

    }
}
