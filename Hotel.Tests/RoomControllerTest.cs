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
        public void RoomGetTest()
        {
            int id = 1;
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Get(id)).Returns(new RoomDTO());

            var expected = mapper.Map<RoomDTO, RoomModel>(mock.Object.Get(id));

            RoomController controller = new RoomController(mock.Object);

            var result = controller.Get(httpRequest, id).Content.ReadAsAsync<RoomModel>();

            Assert.AreEqual(expected, result.Result);

        }
        [TestMethod]
        public void RoomPostTest()
        {
            RoomDTO roomModel = new RoomDTO()
            {
                Active = true,
                RoomNumber = "Test",   
            };
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Create(roomModel));

            RoomController controller = new RoomController(mock.Object);

            var result = controller.Post(httpRequest, mapper.Map<RoomDTO, RoomModel>(roomModel)).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void RoomPutTest()
        {
            int id = 1;
            RoomDTO roomModel = new RoomDTO()
            {
                Active = true,
                RoomNumber = "Test",
            };
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Update(roomModel,id));

            RoomController controller = new RoomController(mock.Object);

            var result = controller.Put(httpRequest,id, mapper.Map<RoomDTO, RoomModel>(roomModel)).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void RoomDeleteTest()
        {
            int id = 1;
            var mock = new Mock<IRoomService>();
            mock.Setup(a => a.Delete(id));

            RoomController controller = new RoomController(mock.Object);

            var result = controller.Delete(httpRequest, id).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
       
    }
}
