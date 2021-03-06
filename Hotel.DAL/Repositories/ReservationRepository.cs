using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class ReservationRepository : IRepository<Reservation>
    {
        private HotelContext db;

        public ReservationRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return db.Reservations;
        }

        public Reservation Get(int id)
        {
            return db.Reservations.Find(id);
        }

        public void Create(Reservation reservation)
        {
            db.Reservations.Add(reservation);
        }

        public void Delete(int id)
        {
            Reservation reservation = Get(id);
            if (reservation != null)
                db.Reservations.Remove(reservation);
        }

        public void Update(Reservation newItem, int id)
        {
            Reservation reservation = Get(id);
            if (reservation != null)
            {
                reservation.Id = newItem.Id;
                reservation.CustomerId = newItem.CustomerId;
                reservation.RoomId = newItem.RoomId;
                reservation.ReservationDate = newItem.ReservationDate;
                reservation.ArrivalDate = newItem.ArrivalDate;
                reservation.DepartureDate = newItem.DepartureDate;
                reservation.CheckIn = newItem.CheckIn;

                db.Entry(reservation).State = EntityState.Modified;
            }
        }
    }
}
