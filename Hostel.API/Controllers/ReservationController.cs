﻿using AutoMapper;
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
    public class ReservationController : ApiController
    {
        private IReservationService service;
        private IMapper mapperReservation;
        private IMapper mapperReservationReverse;

        public ReservationController(IReservationService service)
        {
            this.service = service;
            mapperReservation = InitMapper();
            mapperReservationReverse = InitMapperReverse();
        }

        private IMapper InitMapper()
        {
            var mapperCustomer = new MapperConfiguration(cfg =>
               cfg.CreateMap<CustomerDTO, CustomerModel>()).CreateMapper();

            var mapperCategory = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

            var mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                 .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.RoomNumber))
                 .ForMember(d => d.Active, o => o.MapFrom(s => s.Active))
                 .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<CategoryDTO, CategoryModel>(s.Category)))
                ).CreateMapper();

            return mapperReservation = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationDTO, ReservationModel>()
              .ForMember(d => d.ReservationDate, o => o.MapFrom(s => s.ReservationDate))
              .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
              .ForMember(d => d.ArrivalDate, o => o.MapFrom(s => s.ArrivalDate))
              .ForMember(d => d.CheckIn, o => o.MapFrom(s => s.CheckIn))
              .ForMember(d => d.DepartureDate, o => o.MapFrom(s => s.DepartureDate))
              .ForMember(d => d.Customer, o => o.MapFrom(s => mapperCustomer.Map<CustomerDTO, CustomerModel>(s.Customer)))
              .ForMember(d => d.Room, o => o.MapFrom(s => mapperRoom.Map<RoomDTO, RoomModel>(s.Room)))
              ).CreateMapper();
        }
        private IMapper InitMapperReverse()
        {
            var mapperCustomerReverse = new MapperConfiguration(cfg =>
            cfg.CreateMap<CustomerModel, CustomerDTO>()).CreateMapper();

            var mapperCategoryReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();

            var mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomModel, RoomDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.RoomNumber))
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active))
                .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategoryReverse.Map<CategoryModel, CategoryDTO>(s.Category)))
               ).CreateMapper();

            return mapperReservationReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationModel, ReservationDTO>()
              .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
              .ForMember(d => d.Customer, o => o.MapFrom(s => mapperCustomerReverse.Map<CustomerModel, CustomerDTO>(s.Customer)))
              .ForMember(d => d.Room, o => o.MapFrom(s => mapperRoomReverse.Map<RoomModel, RoomDTO>(s.Room)))
              .ForMember(d => d.ReservationDate, o => o.MapFrom(s => s.ReservationDate))
              .ForMember(d => d.ArrivalDate, o => o.MapFrom(s => s.ArrivalDate))
              .ForMember(d => d.DepartureDate, o => o.MapFrom(s => s.DepartureDate))
              .ForMember(d => d.CheckIn, o => o.MapFrom(s => s.CheckIn))
              ).CreateMapper();
        }
        public IEnumerable<ReservationModel> Get()
        {
          
            var data = service.GetAll();
            var reservations = mapperReservation.Map<IEnumerable<ReservationDTO>, List<ReservationModel>>(data);
            return reservations;
        }

        [ResponseType(typeof(ReservationModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            ReservationDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var reservation = mapperReservation.Map<ReservationDTO, ReservationModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, reservation);
        }


        [Route("api/GetProfitForMonth/{date}")]
        public decimal GetProfitForMonth([FromUri] DateTime date)
        {
            var profit = service.GetProfitForMonth(date);
           
            return profit;
        }

        public void Post([FromBody] ReservationModel value)
        {
            service.Create(mapperReservationReverse.Map< ReservationModel, ReservationDTO>(value));
        }



        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}