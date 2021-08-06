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
    public class PriceCategoryController : Controller
    {

        private IPriceCategoryService servicePriceCategory;
        private ICategoryService serviceCategory;

        private IMapper mapperPriceCategory;
        private IMapper mapperPriceCategoryReverse;
        private IMapper mapperCategory;

        private ILogger logger;

        public PriceCategoryController(IPriceCategoryService servicePriceCategory, ICategoryService serviceCategory)
        {
            this.servicePriceCategory = servicePriceCategory;
            this.serviceCategory = serviceCategory;

            mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, CategoryModel>()
               ).CreateMapper();
            mapperPriceCategory = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>()
              ).CreateMapper();
            mapperPriceCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>()
              ).CreateMapper();
            logger = LogManager.GetCurrentClassLogger();

        }
        [Authorize]
        public ActionResult Index()
        {
            var priceCategories = mapperPriceCategory.Map<IEnumerable<PriceCategoryDTO>, IEnumerable<PriceCategoryModel>>(servicePriceCategory.GetAll());
            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(serviceCategory.GetAll());
            ViewBag.Categories = categories;
            ViewBag.CategoriesSelectList = new SelectList(categories.ToList(), "Id", "CategoryName");
            ViewBag.PriceCategories = priceCategories;
            return View();
        }
        [Authorize]
        [HttpPost]
        public RedirectResult Add(PriceCategoryModel priceCategory)
        {
            servicePriceCategory.Create(mapperPriceCategoryReverse.Map<PriceCategoryModel, PriceCategoryDTO>(priceCategory));
            logger.Info($"{User.Identity.Name} added price category: category id - {priceCategory.CategoryId}, price - {priceCategory.Price},start day - {priceCategory.StartDate}, end day - {priceCategory.EndDate} ");
            return Redirect("/PriceCategory/Index");
        }

        [Authorize]
        [HttpGet]
        public RedirectResult Delete(int id)
        {
            PriceCategoryModel priceCategory = mapperPriceCategory.Map<PriceCategoryDTO, PriceCategoryModel>(servicePriceCategory.Get(id));
            servicePriceCategory.Delete(id);
            logger.Info($"{User.Identity.Name} deleted price category: category id - {priceCategory.CategoryId}, price - {priceCategory.Price}, date-{priceCategory.StartDate}/{priceCategory.EndDate}");
            return Redirect("/PriceCategory/Index");
        }
    }
}