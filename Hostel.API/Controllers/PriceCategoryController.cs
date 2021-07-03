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

            mapperPriceCategory = InitMapper();
            mapperPriceCategoryReverse = InitMapperReverse();
        }
        private IMapper InitMapper()
        {
           var mapperCategory = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();


            return mapperPriceCategory = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()
              .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
              .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
              .ForMember(d => d.StartDate, o => o.MapFrom(s => s.StartDate))
              .ForMember(d => d.EndDate, o => o.MapFrom(s => s.EndDate))
              .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<CategoryDTO, CategoryModel>(s.Category)))
              ).CreateMapper();
        }
        private IMapper InitMapperReverse()
        {
            var mapperCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();

            return mapperPriceCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>()
              .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
              .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
              .ForMember(d => d.StartDate, o => o.MapFrom(s => s.StartDate))
              .ForMember(d => d.EndDate, o => o.MapFrom(s => s.EndDate))
              .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategoryReverse.Map<CategoryModel, CategoryDTO>(s.Category)))
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


        public void Post([FromBody] PriceCategoryModel value)
        {
            service.Create(mapperPriceCategoryReverse.Map<PriceCategoryModel, PriceCategoryDTO>(value));
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}

