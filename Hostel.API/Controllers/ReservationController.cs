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
    public class ReservationController : ApiController
    {
        private IReservationService service;
        private IMapper mapperReservation;
        private IMapper mapperReservationReverse;

        public ReservationController(IReservationService service)
        {
            this.service = service;
            mapperReservation = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationDTO, ReservationModel>()
              ).CreateMapper();
            mapperReservationReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationModel, ReservationDTO>()
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


        [Route("api/GetProfitForMonth/")]
        public decimal GetProfitForMonth([FromUri] DateTime date)
        {
            var profit = service.GetProfitForMonth(date);
           
            return profit;
        }
        

        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] ReservationModel value)
        {
            service.Create(mapperReservationReverse.Map< ReservationModel, ReservationDTO>(value));
            return request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] ReservationModel newReservation)
        {
            ReservationDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Update(mapperReservationReverse.Map<ReservationModel, ReservationDTO>(newReservation), id);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            ReservationDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
