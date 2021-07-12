using AutoMapper;
using Hostel.API.Controllers;
using Hostel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RoomGetAllResultCorrectTest()
        {
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Get(1)).Returns(new RoomDTO());
            IMapper mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<RoomDTO, RoomModel>()
               ).CreateMapper();
            var expected = mapper.Map<RoomDTO, RoomModel>(mock.Object.Get(1));

            RoomController controller = new RoomController(mock.Object);     
            var result = controller.Get(new HttpRequestMessage(), 1);

            Assert.AreEqual(result.Id, "1");
        }
        [Test]
        public void CustomerGetAllResultCorrectTest()
        {
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CustomerDTO>());
            IMapper mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<CustomerDTO, CustomerModel>()
               ).CreateMapper();
            var expected = mock.Object.GetAll();

            CustomerController controller = new CustomerController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), 1);
        }
        [Test]
        public void GetReturnsProduct()
        {
            int id = 1;
            var mock = new Mock<ICustomerService>();
            mock.Setup(a => a.Get(id)).Returns(new CustomerDTO());
            IMapper mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<CustomerDTO, CustomerModel>()
               ).CreateMapper();
            var expected = mapper.Map<CustomerDTO, CustomerModel> (mock.Object.Get(id));

            CustomerController controller = new CustomerController(mock.Object);
            controller.Request.SetConfiguration(new HttpConfiguration());
            controller.Request = new HttpRequestMessage();
            var result = controller.Get(new HttpRequestMessage(),id);

            Assert.AreEqual(result.Content, expected);
        }
        [Test]
        public void GetReturnsProduct1()
        {
            int id = 1;
            var mock = new Mock<IWorkUnit>();
            mock.Setup(a => a.Customers.Get(id)).Returns(new Customer());
            IMapper mapper = new MapperConfiguration(
               cfg => cfg.CreateMap<Customer, CustomerDTO>()
               ).CreateMapper();
            var expected = mapper.Map<Customer, CustomerDTO>(mock.Object.Customers.Get(id));

            CustomerService controller = new CustomerService(mock.Object);
            var result = controller.Get(id);

            Assert.AreEqual(result.Id, 1);
        }
    }
}