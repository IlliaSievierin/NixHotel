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
    public class CategoryController : ApiController
    {
        private ICategoryService service;
        private IMapper mapperCategory;
        private IMapper mapperCategoryReverse;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
            mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
            mapperCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
        }

        public IEnumerable<CategoryModel> Get()
        {
            var data = service.GetAll();
            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(data);
            return categories;
        }


        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            CategoryDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var category = mapperCategory.Map<CategoryDTO, CategoryModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, category);
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] CategoryModel value)
        {
            service.Create(mapperCategoryReverse.Map<CategoryModel, CategoryDTO>(value));
            return request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            CategoryDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

