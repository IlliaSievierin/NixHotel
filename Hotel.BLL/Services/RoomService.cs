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
            mapperRoom = new MapperConfiguration(cfg =>
               cfg.CreateMap<Room, RoomDTO>()
               ).CreateMapper();

            mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomDTO, Room>()
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
        public IEnumerable<RoomDTO> GetFreeRooms(DateTime dateStartCheck, DateTime dateEndCheck)
        {
            var occupiedRooms = Database.Reservations.GetAll()
                .Where(res=> (dateStartCheck<=res.ArrivalDate && dateEndCheck>=res.ArrivalDate)||
                             (dateStartCheck<=res.DepartureDate && dateEndCheck >= res.DepartureDate)||
                             (dateStartCheck >= res.ArrivalDate && dateEndCheck <= res.DepartureDate))
                .Select(r=>r.RoomId);

            var freeRooms = Database.Rooms.GetAll()
                .Where(r => !occupiedRooms.Contains(r.Id));

            return mapperRoom.Map<IEnumerable<Room>, List<RoomDTO>>(freeRooms);
        }
        public bool CheckRoomAvailability(DateTime dateStartCheck, DateTime dateEndCheck,int roomId)
        {
            var room = GetFreeRooms(dateStartCheck, dateEndCheck).Where(r=>r.Id == roomId).FirstOrDefault();
            if (room != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal GetPrice(int id)
        {
            var priceRoom = Database.PriceCategories.GetAll()
                .Where(pc => pc.CategoryId == id && DateTime.Today >= pc.StartDate && DateTime.Today <= pc.EndDate)
                .FirstOrDefault();
            if(priceRoom!=null)
                return priceRoom.Price;
            return 0m;
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
