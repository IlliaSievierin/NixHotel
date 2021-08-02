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
    public class RoomController : Controller
    {
        private IRoomService serviceRoom;
        private ICategoryService serviceCategory;
        private IMapper mapperRoom;
        private IMapper mapperCategory;
        private IMapper mapperRoomReverse;
        private ILogger logger;

        public RoomController(IRoomService serviceRoom, ICategoryService serviceCaregory)
        {
            this.serviceRoom = serviceRoom;
            this.serviceCategory = serviceCaregory;

            mapperCategory = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()
                ).CreateMapper();

            mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                ).CreateMapper();

            mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomModel, RoomDTO>()
               ).CreateMapper();

            logger = LogManager.GetCurrentClassLogger();

        }
        [Authorize]
        public ActionResult Index()
        {
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(serviceRoom.GetAll());
            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(serviceCategory.GetAll());
            ViewBag.Rooms = rooms;
            ViewBag.CategoriesSelectList = new SelectList(categories.ToList(), "Id", "CategoryName");
                
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var room = mapperRoom.Map<RoomDTO,RoomModel>(serviceRoom.Get(id));
            var category = mapperCategory.Map<CategoryDTO, CategoryModel>(serviceCategory.Get(room.CategoryId));
            var price = serviceRoom.GetPrice(room.CategoryId);
            ViewBag.Category = category;
            if (price == 0)
            {
                ViewBag.Price = "Price not specified";
            }
            else
            {
                ViewBag.Price = price;
            }


            return View(room);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add(RoomModel room)
        {
            serviceRoom.Create(mapperRoomReverse.Map<RoomModel,RoomDTO>(room));
            logger.Info($"{User.Identity.Name} added room {room.RoomNumber}.");
            return Redirect("/Room/Index");
        }

    }
}