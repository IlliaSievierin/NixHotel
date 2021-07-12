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
    public class CategoryControllerTest
    {
        private HttpRequestMessage httpRequest;
        private IMapper mapper;

        public CategoryControllerTest()
        {
            httpRequest = new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

        }

        [TestMethod]
        public void CategoryGetAllResultCorrectTest()
        {
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CategoryDTO>());

            var expected = mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(mock.Object.GetAll());

            CategoryController controller = new CategoryController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void CategoryGetTest()
        {
            int id = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var expected = mapper.Map<CategoryDTO, CategoryModel>(mock.Object.Get(id));

            CategoryController controller = new CategoryController(mock.Object);

            var result = controller.Get(httpRequest, id).Content.ReadAsAsync<CategoryModel>();

            Assert.AreEqual(expected, result.Result);

        }
        [TestMethod]
        public void CategoryPostTest()
        {
            CategoryDTO categoryModel = new CategoryDTO()
            {
               CategoryName = "Test",
               Bed = 2
            };
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Create(categoryModel));

            CategoryController controller = new CategoryController(mock.Object);

            var result = controller.Post(httpRequest, mapper.Map<CategoryDTO, CategoryModel>(categoryModel)).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }
        [TestMethod]
        public void CategoryDeleteTest()
        {
            int id = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Delete(id));

            CategoryController controller = new CategoryController(mock.Object);

            var result = controller.Delete(httpRequest, id).StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);

        }

    }
}
