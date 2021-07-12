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
    public class ReservationControllerTest
    {
        private HttpRequestMessage httpRequest;
        private IMapper mapper;

        public ReservationControllerTest()
        {
            httpRequest = new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            var mapperCustomer = new MapperConfiguration(cfg =>
                cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();

            var mapperCategory = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

            var mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                 .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<CategoryDTO, CategoryModel>(s.Category)))
                ).CreateMapper();

            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationDTO, ReservationModel>()
              .ForMember(d => d.Customer, o => o.MapFrom(s => mapperCustomer.Map<CustomerDTO, CustomerModel>(s.Customer)))
              .ForMember(d => d.Room, o => o.MapFrom(s => mapperRoom.Map<RoomDTO, RoomModel>(s.Room)))
              ).CreateMapper();

        }

        [TestMethod]
        public void ReservationGetAllResultCorrectTest()
        {
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.GetAll()).Returns(new List<ReservationDTO>());

            var expected = mapper.Map<IEnumerable<ReservationDTO>, IEnumerable<ReservationModel>>(mock.Object.GetAll());

            ReservationController controller = new ReservationController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void ReservationGetTest()
        {
            var id = 0;
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.Get(id)).Returns(new ReservationDTO());

            var expected = mapper.Map<ReservationDTO, ReservationModel>(mock.Object.Get(id));

            ReservationController controller = new ReservationController(mock.Object);

            var result = controller.Get(httpRequest, id).Content.ReadAsAsync<ReservationModel>();

            Assert.AreEqual(expected, result.Result);

        }
        [TestMethod]
        public void ReservationPostTest()
        {
            ReservationDTO reservationModel = new ReservationDTO()
            {
                ArrivalDate = new DateTime(2021,1,1),
                DepartureDate  = new DateTime(2021,2,2),
                ReservationDate = new DateTime(2021,1,1),
                CheckIn = true
            };
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.Create(reservationModel));

            ReservationController controller = new ReservationController(mock.Object);

            var result = controller.Post(httpRequest, mapper.Map<ReservationDTO, ReservationModel>(reservationModel)).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void ReservationDeleteTest()
        {
            int id = 1;
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.Delete(id));

            ReservationController controller = new ReservationController(mock.Object);

            var result = controller.Delete(httpRequest, id).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
      
    }
}
