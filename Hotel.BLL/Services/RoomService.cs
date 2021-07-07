using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperRoom;
        private IMapper mapperRoomReverse;

        public RoomService(IWorkUnit database)
        {
            Database = database;
            mapperRoom = InitMapper();
            mapperRoomReverse = InitMapperReverse();

        }
        private IMapper InitMapper()
        {

            var mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            return mapperRoom = new MapperConfiguration(cfg =>
               cfg.CreateMap<Room, RoomDTO>()
                .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<Category, CategoryDTO>(s.Category)))
               ).CreateMapper();
        }
        private IMapper InitMapperReverse()
        {
            var mapperCategoryReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();

            return mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomDTO, Room>()
                .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategoryReverse.Map<CategoryDTO, Category>(s.Category)))
               ).CreateMapper();
        }

        public IEnumerable<RoomDTO> GetAll()
        {
            return mapperRoom.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }

        public RoomDTO Get(int id)
        {
            return mapperRoom.Map<Room, RoomDTO>(Database.Rooms.Get(id));
        }
        public IEnumerable<RoomDTO> GetFreeRooms(DateTime dateCheck)
        {
            var occupiedRooms = Database.Reservations.GetAll()
                .Where(res=> dateCheck >= res.ArrivalDate && dateCheck <= res.DepartureDate)
                .Select(r=>r.RoomId);

            var freeRooms = Database.Rooms.GetAll()
                .Where(r => !occupiedRooms.Contains(r.Id));

            return mapperRoom.Map<IEnumerable<Room>, List<RoomDTO>>(freeRooms);
        }

        public void Create(RoomDTO item)
        {
            Database.Rooms.Create(mapperRoomReverse.Map<RoomDTO, Room>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            var data = Database.Rooms.Get(id);
            if (data != null)
            {
                Database.Rooms.Delete(id);
                Database.Save();
            }
        }

        public void Update(RoomDTO newRoom, int id)
        {
            Database.Rooms.Update(mapperRoomReverse.Map<RoomDTO, Room>(newRoom), id);
            Database.Save();
        }
    }
}
