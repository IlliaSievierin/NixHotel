using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public CustomerDTO Customer { get; set; }

        public RoomDTO Room { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public bool CheckIn { get; set; }

    }
}
