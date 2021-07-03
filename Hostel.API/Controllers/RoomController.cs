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

            mapperRoom = InitMapper();
            mapperRoomReverse = InitMapperReverse();
        }
        private IMapper InitMapper()
        {
            var mapperCategory = new MapperConfiguration(cfg =>
                  cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();

            return mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()
                 .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.RoomNumber))
                 .ForMember(d => d.Active, o => o.MapFrom(s => s.Active))
                 .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<CategoryDTO, CategoryModel>(s.Category)))
                ).CreateMapper();
        }
        private IMapper InitMapperReverse()
        {
            var mapperCategoryReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();

            return mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomModel, RoomDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.RoomNumber))
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active))
                .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategoryReverse.Map<CategoryModel, CategoryDTO>(s.Category)))
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
        [Route("api/GetFreeRooms/{checkDate}")]
        public HttpResponseMessage GetFreeRooms(HttpRequestMessage request,[FromUri]DateTime checkDate)
        {
            var rooms = service.GetFreeRooms(checkDate);
            if (!rooms.Any())
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, mapperRoom.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms));
        }
        public void Post([FromBody] RoomModel value)
        {
            service.Create(mapperRoomReverse.Map<RoomModel, RoomDTO>(value));
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
