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
        private PriceCategoryDTO priceCategoryDTOTest;
        private IMapper mapper;

        public PriceCategoryTest()
        {
            httpRequest = new HttpRequestMessage();
            httpRequest.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

             mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()
              ).CreateMapper();

            priceCategoryDTOTest = new PriceCategoryDTO()
            {
                Price = 1000,
                StartDate = new DateTime(2021,1,1),
                EndDate = new DateTime(2021,2,2)
            };

        }

        [TestMethod]
        public void PriceCategoryGetAllCorrectTest()
        {
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.GetAll()).Returns(new List<PriceCategoryDTO>());

            var expected = mapper.Map<IEnumerable<PriceCategoryDTO>, IEnumerable<PriceCategoryModel>>(mock.Object.GetAll());

            PriceCategoryController controller = new PriceCategoryController(mock.Object);
            var result = controller.Get();

            Assert.AreEqual(expected.Count(), result.Count());
        }
        [TestMethod]
        public void PriceCategoryGetCorrectTest()
        {
            var id = 1;
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.Get(id)).Returns(new PriceCategoryDTO());

            var expected = mock.Object.Get(id);

            PriceCategoryController controller = new PriceCategoryController(mock.Object);
            var result = controller.Get(httpRequest, id);
            var resultContent = result.Content.ReadAsAsync<PriceCategoryDTO>();

            Assert.AreEqual(expected, resultContent.Result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
        [TestMethod]
        public void PriceCategoryPostCorrectTest()
        {
            var mock = new Mock<IPriceCategoryService>();
            int lastIdPriceCategory = 1;
            mock.Setup(a => a.Get(lastIdPriceCategory)).Returns(priceCategoryDTOTest);

            PriceCategoryController controller = new PriceCategoryController(mock.Object);
            var resultCode = controller.Post(httpRequest, mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryDTOTest)).StatusCode;
            var result = controller.Get(httpRequest, lastIdPriceCategory).Content.ReadAsAsync<PriceCategoryDTO>();

            Assert.AreEqual(priceCategoryDTOTest, result.Result);
            Assert.AreEqual(HttpStatusCode.OK, resultCode);

        }
        [TestMethod]
        public void PriceCategoryDeleteCorrectTest()
        {
            int id = 1;
            var mock = new Mock<IPriceCategoryService>();
            mock.Setup(a => a.Get(id)).Returns(new PriceCategoryDTO());

            PriceCategoryController controller = new PriceCategoryController(mock.Object);
            var resultCode = controller.Delete(httpRequest, id).StatusCode;
            var deletedPriceCategory = controller.Get(httpRequest, id).Content.ReadAsAsync<PriceCategoryDTO>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            Assert.AreNotEqual(priceCategoryDTOTest, deletedPriceCategory);

        }
    
    }
}
