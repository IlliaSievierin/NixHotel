using AutoMapper;
using Hostel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hostel.API.Controllers
{
    public class PriceCategoryController : ApiController
    {
        private IPriceCategoryService service;
        private IMapper mapperPriceCategory;
        private IMapper mapperPriceCategoryReverse;

        public PriceCategoryController(IPriceCategoryService service)
        {
            this.service = service;

            mapperPriceCategory = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()
              ).CreateMapper();

            mapperPriceCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>()
              ).CreateMapper();
        }
        public IEnumerable<PriceCategoryModel> Get()
        {

            var data = service.GetAll();
            var priceCategories = mapperPriceCategory.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(data);
            return priceCategories;
        }


        [ResponseType(typeof(PriceCategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            PriceCategoryDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var priceCategory = mapperPriceCategory.Map<PriceCategoryDTO, PriceCategoryModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, priceCategory);
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] PriceCategoryModel value)
        {
            service.Create(mapperPriceCategoryReverse.Map<PriceCategoryModel, PriceCategoryDTO>(value));
            return request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            PriceCategoryDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

