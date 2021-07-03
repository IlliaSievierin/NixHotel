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

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        public IEnumerable<CategoryModel> Get()
        {
            var mapper = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

            var data = service.GetAll();
            var categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(data);
            return categories;
        }


        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

            CategoryDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var category = mapper.Map<CategoryDTO, CategoryModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, category);
        }


        public void Post([FromBody] CategoryModel value)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();

            service.Create(mapper.Map<CategoryModel, CategoryDTO>(value));
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}

