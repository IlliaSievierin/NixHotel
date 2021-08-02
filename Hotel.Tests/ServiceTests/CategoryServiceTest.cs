using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Services;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Tests.ServiceTests
{
    [TestClass]
    public class CategoryServiceTest
    {
        private Category categoryTest;
        private IMapper mapper;

        public CategoryServiceTest()
        {
            mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            categoryTest = new Category()
            {
                CategoryName = "TestName",
                Bed = 2
            };
        }

        [TestMethod]
        public void CategoryGetAllCorrectTest()
        {
            var mock = new Mock<IWorkUnit>();
            mock.Setup(a => a.Categories.GetAll()).Returns(new List<Category>());

            var expected = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(mock.Object.Categories.GetAll());

            CategoryService service = new CategoryService(mock.Object);
            var result = service.GetAll();

            Assert.AreEqual(expected.Count(), result.Count());
        }

        [TestMethod]
        public void CategoryGetCorrectTest()
        {
            int id = 1;
            var mock = new Mock<IWorkUnit>();
            mock.Setup(a => a.Categories.Get(id)).Returns(new Category());

            var expected = mapper.Map<Category,CategoryDTO>(mock.Object.Categories.Get(id));

            CategoryService service = new CategoryService(mock.Object);
            var result = service.Get(id);

            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void CategoryPostCorrectTest()
        {
            var mock = new Mock<IWorkUnit>();
            int lastIdCategory = 1;
            mock.Setup(a => a.Categories.Get(lastIdCategory)).Returns(categoryTest);

            var expected = mapper.Map<Category, CategoryDTO>(categoryTest);

            CategoryService service = new CategoryService(mock.Object);
            var result = service.Get(lastIdCategory);

            Assert.AreEqual(expected, result);
        }
        /*
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
        */
    }
}
