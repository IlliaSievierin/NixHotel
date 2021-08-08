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
            ViewBag.Customers = customers;
            ViewBag.Rooms = rooms;
            ViewBag.CustomersSelectList = new SelectList(customers.ToList(), "Id", "Passport");
            ViewBag.RoomsSelectList = new SelectList(rooms.ToList().Where(r=>r.Active).OrderBy(r=>r.RoomNumber), "Id", "RoomNumber"); 

            return View();
        }
        [Authorize]
        [HttpPost]
        public RedirectResult Add(ReservationModel reservation)
        {
            if (serviceRoom.CheckRoomAvailability(reservation.ArrivalDate, reservation.DepartureDate, reservation.RoomId))
            {
                serviceReservation.Create(mapperReservationReverse.Map<ReservationModel, ReservationDTO>(reservation));
                logger.Info($"{User.Identity.Name} added reservation: сustomer id - {reservation.CustomerId}, room id - {reservation.RoomId}, arrival date- {reservation.ArrivalDate}, departure date - {reservation.DepartureDate}.");
                return Redirect("/Reservation/Index");
            }
            else
            {
                return Redirect("/Reservation/RoomBusy");
            }
        }
        [Authorize]
        [HttpGet]
        public RedirectResult Delete(int id)
        {
            ReservationModel reservation = mapperReservation.Map<ReservationDTO, ReservationModel>(serviceReservation.Get(id));
            serviceReservation.Delete(id);
            logger.Info($"{User.Identity.Name} deleted reservation: сustomer id - {reservation.CustomerId}, room id - {reservation.RoomId}, arrival date- {reservation.ArrivalDate}, departure date - {reservation.DepartureDate}.");
            return Redirect("/Reservation/Index");
        }

        [Authorize]
        [HttpGet]
        public RedirectResult Edit(int id)
        {
            ReservationModel reservation = mapperReservation.Map<ReservationDTO, ReservationModel>(serviceReservation.Get(id));
            reservation.CheckIn = true;
            serviceReservation.Update(mapperReservationReverse.Map <ReservationModel,ReservationDTO>(reservation), id);
            logger.Info($"{User.Identity.Name} update status reservation (now {reservation.CheckIn}.");
            return Redirect("/Reservation/Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfitForMonth(DateTime dateCheck)
        {
            var profitForSelectedMonth = serviceReservation.GetProfitForMonth(dateCheck);
            ViewBag.Profit = profitForSelectedMonth;
            ViewBag.CheckDate = dateCheck;
            return View();
        }

        public ActionResult RoomBusy()
        {
            return View();
        }

    }
}