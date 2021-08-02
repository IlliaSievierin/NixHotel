using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService service;
        private IMapper mapperCategory;
        private IMapper mapperCategoryReverse;
        private ILogger logger;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
            mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
            mapperCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
            logger = LogManager.GetCurrentClassLogger();
        }
        [Authorize]
        public ActionResult Index()
        {
            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(service.GetAll());
            ViewBag.Categories = categories;
            return View();
        }

        [Authorize]
        [HttpPost]
        public RedirectResult Add(CategoryModel category)
        {
            service.Create(mapperCategoryReverse.Map<CategoryModel, CategoryDTO>(category));
            logger.Info($"{User.Identity.Name} added сategory {category.CategoryName}, bed - {category.Bed}.");
            return Redirect("/Category/Index");
        }
    }
}