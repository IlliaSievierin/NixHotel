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
    public class PriceCategoryTest
    {
        private HttpRequestMessage httpRequest;
        private IMapper mapper;

        public PriceCategoryTest()
        {
            httpRequest = new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            var mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
             mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()
              .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<CategoryDTO, CategoryModel>(s.Category)))
              ).CreateMapper();

        }

        [TestMethod]
        public void PriceCategoryGetAllResultCorrectTest()
        {
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.GetAll()).Returns(new List<PriceCategoryDTO>());

            var expected = mapper.Map<IEnumerable<PriceCategoryDTO>, IEnumerable<PriceCategoryModel>>(mock.Object.GetAll());

            PriceCategoryController controller = new PriceCategoryController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void PriceCategoryGetTest()
        {
            var id = 0;
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.Get(id)).Returns(new PriceCategoryDTO());

            var expected = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(mock.Object.Get(id));

            PriceCategoryController controller = new PriceCategoryController(mock.Object);

            var result = controller.Get(httpRequest, id).Content.ReadAsAsync<PriceCategoryModel>();

            Assert.AreEqual(expected, result.Result);

        }
        [TestMethod]
        public void PriceCategoryPostTest()
        {
            PriceCategoryDTO priceCategoryModel = new PriceCategoryDTO()
            {
               Price = 1000,
               StartDate = new DateTime(2021,1,1),
               EndDate = new DateTime(2021,2,2)
            };
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.Create(priceCategoryModel));

            PriceCategoryController controller = new PriceCategoryController(mock.Object);

            var result = controller.Post(httpRequest, mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryModel)).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void PriceCategoryDeleteTest()
        {
            int id = 1;
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.Delete(id));

            PriceCategoryController controller = new PriceCategoryController(mock.Object);

            var result = controller.Delete(httpRequest, id).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
    
    }
}
