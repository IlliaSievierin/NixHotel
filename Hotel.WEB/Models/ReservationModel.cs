using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class ReservationModel
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
            if (obj is ReservationModel)
            {
                var objRM = obj as ReservationModel;

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