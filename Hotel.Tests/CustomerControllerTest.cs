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
        private CustomerDTO customerDTOTest;
        private IMapper mapper;

        public CustomerControllerTest()
        {
            httpRequest= new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<CustomerDTO, CustomerModel>()
               ).CreateMapper();
            customerDTOTest = new CustomerDTO()
            {
                LastName = "Test",
                FirstName = "Test",
                MiddleName = "Test",
                DateOfBirth = new DateTime(1999, 2, 2),
                Passport = "193849374"
            };
        }

        [TestMethod]
        public void CustomerGetAllCorrectTest()
        {
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CustomerDTO>());

            var expected = mapper.Map<IEnumerable<CustomerDTO>, IEnumerable<CustomerModel>>(mock.Object.GetAll());

            CustomerController controller = new CustomerController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }

        [TestMethod]
        public void CustomerGetCorrectTest()
        {
            var id = 1;
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.Get(id)).Returns(new CustomerDTO());
           
            var expected = mock.Object.Get(id);

            CustomerController controller = new CustomerController(mock.Object);
            var result = controller.Get(httpRequest, id);
            var resultContent = result.Content.ReadAsAsync<CustomerDTO>();

            Assert.AreEqual(expected, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
       
        [TestMethod]
        public void CustomerPostCorrectTest()
        {
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CustomerDTO>() { customerDTOTest });
            int lastIdCustomer = mock.Object.GetAll().Count();
            mock.Setup(a => a.Get(lastIdCustomer)).Returns(customerDTOTest);

            CustomerController controller = new CustomerController(mock.Object);
            var result = controller.Get(httpRequest, lastIdCustomer);
            var resultContent = result.Content.ReadAsAsync<CustomerDTO>();

            Assert.AreEqual(customerDTOTest, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void CustomerDeleteTest()
        {
            int id = 1;
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.Get(id)).Returns(new CustomerDTO());

            CustomerController controller = new CustomerController(mock.Object);
            var resultCode = controller.Delete(httpRequest, id).StatusCode;
            var deletedCustomer = controller.Get(httpRequest, id).Content.ReadAsAsync<CustomerDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreNotEqual(customerDTOTest, deletedCustomer);
        }

        [TestMethod]
        public void CustomerGetCountTest()
        {
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CustomerDTO>());

            var expected = mock.Object.GetAll().Count();

            CustomerController controller = new CustomerController(mock.Object);
            var result = controller.GetAllCount();

            Assert.AreEqual(expected, result);
        }

    }
}
