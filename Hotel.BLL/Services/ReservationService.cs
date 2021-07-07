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
    public class ReservationService : IReservationService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperReservation;
        private IMapper mapperReservationReverse;

        public ReservationService(IWorkUnit database)
        {
            Database = database;
            mapperReservation = InitMapper();
            mapperReservationReverse = InitMapperReverse();
        }
        private IMapper InitMapper()
        {
            var mapperCustomer = new MapperConfiguration(cfg =>
            cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();

            var mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            var mapperRoom = new MapperConfiguration(cfg =>
               cfg.CreateMap<Room, RoomDTO>()
                /*.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.RoomNumber))
                .ForMember(d => d.Active, o => o.MapFrom(s => s.Active))*/
                .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategory.Map<Category, CategoryDTO>(s.Category)))
               ).CreateMapper();

            return mapperReservation = new MapperConfiguration(cfg =>
              cfg.CreateMap<Reservation, ReservationDTO>()
              //.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
              .ForMember(d => d.Customer, o => o.MapFrom(s => mapperCustomer.Map<Customer, CustomerDTO>(s.Customer)))
              .ForMember(d => d.Room, o => o.MapFrom(s => mapperRoom.Map<Room, RoomDTO>(s.Room)))
              /*.ForMember(d => d.ReservationDate, o => o.MapFrom(s => s.ReservationDate))
              .ForMember(d => d.ArrivalDate, o => o.MapFrom(s => s.ArrivalDate))
              .ForMember(d => d.DepartureDate, o => o.MapFrom(s => s.DepartureDate))
              .ForMember(d => d.CheckIn, o => o.MapFrom(s => s.CheckIn))*/
              ).CreateMapper();
        }
        private IMapper InitMapperReverse()
        {
            var mapperCustomerReverse = new MapperConfiguration(cfg =>
            cfg.CreateMap<CustomerDTO, Customer>()).CreateMapper();

            var mapperCategoryReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();

            var mapperRoomReverse = new MapperConfiguration(cfg =>
               cfg.CreateMap<RoomDTO, Room>()
                .ForMember(d => d.Category, o => o.MapFrom(s => mapperCategoryReverse.Map<CategoryDTO, Category>(s.Category)))
               ).CreateMapper();

            return mapperReservationReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationDTO, Reservation>()
              .ForMember(d => d.Customer, o => o.MapFrom(s => mapperCustomerReverse.Map<CustomerDTO, Customer>(s.Customer)))
              .ForMember(d => d.Room, o => o.MapFrom(s => mapperRoomReverse.Map<RoomDTO, Room>(s.Room)))
              ).CreateMapper();
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            return mapperReservation.Map<IEnumerable<Reservation>, List<ReservationDTO>>(Database.Reservations.GetAll());
        }
        public decimal GetProfitForMonth(DateTime date)
        {
            var reservedRooms = Database.Reservations.GetAll()
                .Where(r => r.DepartureDate.Month == date.Month && r.DepartureDate.Year == date.Year);

            decimal sumProfit = 0;

            foreach (var room in reservedRooms)
            {
                int countDay = room.DepartureDate.Subtract(room.ArrivalDate).Days;
                sumProfit += Database.PriceCategories.GetAll()
                    .Where(pc => pc.CategoryId == room.Room.CategoryId && room.DepartureDate >= pc.StartDate && room.DepartureDate <= pc.EndDate)
                    .Select(p => p.Price).FirstOrDefault() * countDay;
            }
            return sumProfit;
        }
        public ReservationDTO Get(int id)
        {
            return mapperReservation.Map<Reservation, ReservationDTO>(Database.Reservations.Get(id));
        }

        public void Create(ReservationDTO item)
        {
            Database.Reservations.Create(mapperReservationReverse.Map<ReservationDTO, Reservation>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            var data = Database.Reservations.Get(id);
            if (data != null)
            {
                Database.Reservations.Delete(id);
                Database.Save();
            }
        }
    }
}
