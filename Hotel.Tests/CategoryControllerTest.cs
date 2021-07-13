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
        private CategoryDTO categoryDTOTest;
        private IMapper mapper;

        public CategoryControllerTest()
        {
            httpRequest = new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
            categoryDTOTest = new CategoryDTO()
            {
                CategoryName = "TestName",
                Bed = 2
            };

        }

        [TestMethod]
        public void CategoryGetAllCorrectTest()
        {
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CategoryDTO>());

            var expected = mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(mock.Object.GetAll());

            CategoryController controller = new CategoryController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void CategoryGetCorrectTest()
        {
            int id = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var expected = mock.Object.Get(id);

            CategoryController controller = new CategoryController(mock.Object);
            var result = controller.Get(httpRequest, id);
            var resultContent = result.Content.ReadAsAsync<CategoryDTO>();

            Assert.AreEqual(expected, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
        [TestMethod]
        public void CategoryPostCorrectTest()
        {

            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.GetAll()).Returns(new List<CategoryDTO>() { categoryDTOTest });
            int lastIdCategory = mock.Object.GetAll().Count();
            mock.Setup(a => a.Get(lastIdCategory)).Returns(categoryDTOTest);

            CategoryController controller = new CategoryController(mock.Object);

            var result = controller.Get(httpRequest, lastIdCategory);
            var resultContent = result.Content.ReadAsAsync<CategoryDTO>();

            Assert.AreEqual(categoryDTOTest, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }
        [TestMethod]
        public void CategoryDeleteCorrectTest()
        {
            int id = 1;
            var mock = new Mock<ICategoryService>();
            mock.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            CategoryController controller = new CategoryController(mock.Object);
            var resultCode = controller.Delete(httpRequest, id).StatusCode;
            var deletedCategory = controller.Get(httpRequest, id).Content.ReadAsAsync<CategoryDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreNotEqual(categoryDTOTest, deletedCategory);
        }

    }
}
