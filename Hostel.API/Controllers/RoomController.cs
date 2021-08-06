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
    public class RoomController : ApiController
    {
        private IRoomService service;
        private IMapper mapperRoom;
        private IMapper mapperRoomReverse;

        public RoomController(IRoomService service)
        {
            this.service = service;

            mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                ).CreateMapper();

            mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomModel, RoomDTO>()
               ).CreateMapper();
        }
        public IEnumerable<RoomModel> Get()
        {
            var data = service.GetAll();
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomModel>>(data);
            return rooms;
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            RoomDTO data = service.Get(id);

            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            var room = mapperRoom.Map<RoomDTO, RoomModel>(data);
            return request.CreateResponse(HttpStatusCode.OK, room);
        }

        [ResponseType(typeof(List<RoomModel>))]
        [Route("api/GetFreeRooms/")]
        public HttpResponseMessage GetFreeRooms(HttpRequestMessage request,[FromUri]DateTime checkStartDate, [FromUri] DateTime checkEndDate)
        {
            var rooms = service.GetFreeRooms(checkStartDate, checkEndDate);
            if (!rooms.Any())
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, mapperRoom.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms));
        }
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] RoomModel value)
        {
            service.Create(mapperRoomReverse.Map<RoomModel, RoomDTO>(value));
            return request.CreateResponse(HttpStatusCode.OK);
        }
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] RoomModel newRoom)
        {
            RoomDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Update(mapperRoomReverse.Map<RoomModel, RoomDTO>(newRoom), id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            RoomDTO data = service.Get(id);
            if (data == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            service.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
