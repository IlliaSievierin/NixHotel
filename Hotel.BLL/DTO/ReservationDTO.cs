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
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool CheckIn { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is ReservationDTO)
            {
                var objRM = obj as ReservationDTO;

                return this.ReservationDate == objRM.ReservationDate
                    && this.ArrivalDate == objRM.ArrivalDate
                    && this.DepartureDate == objRM.DepartureDate
                    && this.CheckIn == objRM.CheckIn
                    && this.CustomerId == objRM.CustomerId
                    && this.RoomId == objRM.RoomId;
            }
            else return base.Equals(obj);
        }

    }
}
