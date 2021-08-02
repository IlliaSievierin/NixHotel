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
    public class ReservationController : Controller
    {
        IReservationService serviceReservation;
        IRoomService serviceRoom;
        ICustomerService serviceCustomer;

        IMapper mapperCustomer;
        IMapper mapperRoom;
        IMapper mapperReservation;
        IMapper mapperReservationReverse;

        ILogger logger;
        public ReservationController(IReservationService serviceReservation, IRoomService serviceRoom, ICustomerService customerService)
        {
            this.serviceReservation = serviceReservation;
            this.serviceRoom = serviceRoom;
            this.serviceCustomer = customerService;

             mapperCustomer = new MapperConfiguration(cfg =>
              cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();

             mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                ).CreateMapper();

             mapperReservation = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationDTO, ReservationModel>()
              ).CreateMapper();

             mapperReservationReverse = new MapperConfiguration(cfg =>
             cfg.CreateMap<ReservationModel, ReservationDTO>()
             ).CreateMapper();

            logger = LogManager.GetCurrentClassLogger();
        }
        [Authorize]
        public ActionResult Index()
        {
            var reservations = mapperReservation.Map<IEnumerable<ReservationDTO>, IEnumerable<ReservationModel>>(serviceReservation.GetAll());
            var customers = mapperCustomer.Map<IEnumerable<CustomerDTO>, IEnumerable<CustomerModel>>(serviceCustomer.GetAll());
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(serviceRoom.GetAll());
            ViewBag.Reservation = reservations;
            ViewBag.CustomersSelectList = new SelectList(customers.ToList(), "Id", "Passport");
            ViewBag.RoomsSelectList = new SelectList(rooms.ToList(), "Id", "RoomNumber"); ;

            return View();
        }
        [Authorize]
        [HttpPost]
        public RedirectResult Add(ReservationModel reservation)
        {
            serviceReservation.Create(mapperReservationReverse.Map<ReservationModel, ReservationDTO>(reservation));
            logger.Info($"{User.Identity.Name} added reservation сustomer id - {reservation.CustomerId}, room id - {reservation.RoomId}, arrival date- {reservation.ArrivalDate}, departure date - {reservation.DepartureDate}.");
            return Redirect("/Reservation/Index");
        }
    }
}