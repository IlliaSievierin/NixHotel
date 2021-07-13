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
        private ReservationDTO reservationDTOTest;
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

            reservationDTOTest = new ReservationDTO()
            {
                ArrivalDate = new DateTime(2021, 1, 1),
                DepartureDate = new DateTime(2021, 2, 2),
                ReservationDate = new DateTime(2021, 1, 1),
                CheckIn = true
            };
        }

        [TestMethod]
        public void ReservationGetAllCorrectTest()
        {
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.GetAll()).Returns(new List<ReservationDTO>());

            var expected = mapper.Map<IEnumerable<ReservationDTO>, IEnumerable<ReservationModel>>(mock.Object.GetAll());

            ReservationController controller = new ReservationController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void ReservationGetCorrectTest()
        {
            var id = 1;
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.Get(id)).Returns(new ReservationDTO());

            var expected = mock.Object.Get(id);

            ReservationController controller = new ReservationController(mock.Object);
            var result = controller.Get(httpRequest, id);
            var resultContent = result.Content.ReadAsAsync<ReservationDTO>();

            Assert.AreEqual(expected, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }
        
        [TestMethod]
        public void ReservationPostCorrectTest()
        {
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.GetAll()).Returns(new List<ReservationDTO>() { reservationDTOTest });
            int lastIdReservation = mock.Object.GetAll().Count();
            mock.Setup(a => a.Get(lastIdReservation)).Returns(reservationDTOTest);

            ReservationController controller = new ReservationController(mock.Object);
            var result = controller.Get(httpRequest, lastIdReservation);
            var resultContent = result.Content.ReadAsAsync<ReservationDTO>();

            Assert.AreEqual(reservationDTOTest, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
        
        [TestMethod]
        public void ReservationDeleteCorrectTest()
        {
            int id = 1;
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.Get(id)).Returns(new ReservationDTO());

            ReservationController controller = new ReservationController(mock.Object);
            var resultCode = controller.Delete(httpRequest, id).StatusCode;
            var deletedReservation = controller.Get(httpRequest, id).Content.ReadAsAsync<RoomDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreNotEqual(reservationDTOTest, deletedReservation);
        }

        [TestMethod]
        public void ReservationPutCorrectTest()
        {
            int id = 1; 
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.Get(id)).Returns(reservationDTOTest);
            var reservationModelTest = mapper.Map<ReservationDTO, ReservationModel>(reservationDTOTest);

            ReservationController controller = new ReservationController(mock.Object);
            var resultCode = controller.Put(httpRequest, id, reservationModelTest).StatusCode;
            var updatedReservation = controller.Get(httpRequest, id).Content.ReadAsAsync<ReservationDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreEqual(reservationDTOTest, updatedReservation.Result);
        }

        [TestMethod]
        public void ReservationGetProfitTest()
        {
            var testDate = new DateTime(2021, 1, 1);
            var mock = new Mock<IReservationService>();
            mock.Setup(a => a.GetProfitForMonth(testDate)).Returns(700m);

            var expected = mock.Object.GetProfitForMonth(testDate);

            ReservationController controller = new ReservationController(mock.Object);
            var result = controller.GetProfitForMonth(testDate);

            Assert.AreEqual(expected, result);
        }
    }
}
