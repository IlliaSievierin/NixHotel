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
    public class RoomControllerTest
    {
        private HttpRequestMessage httpRequest;
        private RoomDTO roomDTOTest;
        private IMapper mapper;

        public RoomControllerTest()
        {
            httpRequest = new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            var mapperCategory = new MapperConfiguration(cfg =>
                  cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                 .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<CategoryDTO, CategoryModel>(s.Category)))
                ).CreateMapper();

            roomDTOTest = new RoomDTO()
            {
                Active = true,
                RoomNumber = "Test",
            };

        }

        [TestMethod]
        public void RoomGetAllResultCorrectTest()
        {
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.GetAll()).Returns(new List<RoomDTO>());

            var expected = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(mock.Object.GetAll());

            RoomController controller = new RoomController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }

        [TestMethod]
        public void RoomGetCorrectTest()
        {
            int id = 1;
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Get(id)).Returns(new RoomDTO());

            var expected = mock.Object.Get(id);

            RoomController controller = new RoomController(mock.Object);
            var result = controller.Get(httpRequest, id);
            var resultContent = result.Content.ReadAsAsync<RoomDTO>();

            Assert.AreEqual(expected, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void RoomPostCorrectTest()
        {
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.GetAll()).Returns(new List<RoomDTO>() { roomDTOTest });
            int lastIdRoom = mock.Object.GetAll().Count();
            mock.Setup(a => a.Get(lastIdRoom)).Returns(roomDTOTest);

            RoomController controller = new RoomController(mock.Object);
            var result = controller.Get(httpRequest, lastIdRoom);
            var resultContent = result.Content.ReadAsAsync<RoomDTO>();

            Assert.AreEqual(roomDTOTest, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void RoomPutCorrectTest()
        {
            int id = 1;
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Get(id)).Returns(roomDTOTest);
            var roomModelTest = mapper.Map<RoomDTO, RoomModel>(roomDTOTest);

            RoomController controller = new RoomController(mock.Object);
            var resultCode = controller.Put(httpRequest, id, roomModelTest).StatusCode;
            var updatedRoom = controller.Get(httpRequest, id).Content.ReadAsAsync<RoomDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreEqual(roomDTOTest, updatedRoom.Result);
        }

        [TestMethod]
        public void RoomDeleteCorrectTest()
        {
            int id = 1;
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Get(id)).Returns(new RoomDTO());

            RoomController controller = new RoomController(mock.Object);
            var resultCode = controller.Delete(httpRequest, id).StatusCode;
            var deletedRoom = controller.Get(httpRequest, id).Content.ReadAsAsync<RoomDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreNotEqual(roomDTOTest, deletedRoom);
        }

        [TestMethod]
        public void RoomGetFreeRoomsCorrectTest()
        {
            var testDate = new DateTime(2021, 1, 1);
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.GetFreeRooms(testDate)).Returns(new List<RoomDTO>() { roomDTOTest});

            var expected = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(mock.Object.GetFreeRooms(testDate));

            RoomController controller = new RoomController(mock.Object);
            var result = controller.GetFreeRooms(httpRequest,testDate);
            var resultContent = result.Content.ReadAsAsync<List<RoomDTO>>();

            Assert.AreEqual(expected.Count(), resultContent.Result.Count());
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

    }
}
