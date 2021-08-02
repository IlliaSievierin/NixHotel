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
            mapperReservation = new MapperConfiguration(cfg =>
              cfg.CreateMap<Reservation, ReservationDTO>()
              ).CreateMapper();

            mapperReservationReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<ReservationDTO, Reservation>()
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

        public void Update(ReservationDTO newReservation, int id)
        {
            Database.Reservations.Update(mapperReservationReverse.Map<ReservationDTO, Reservation>(newReservation), id);
            Database.Save();
        }
    }
}
