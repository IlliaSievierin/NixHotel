using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int CustomerId { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public bool CheckIn { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Room Room { get; set; }
    }
}
