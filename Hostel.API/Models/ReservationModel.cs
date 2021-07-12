using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostel.API.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        public CustomerModel Customer { get; set; }

        public RoomModel Room { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public bool CheckIn { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is ReservationModel)
            {
                var objRM = obj as ReservationModel;

                return this.ReservationDate == objRM.ReservationDate
                    && this.ArrivalDate == objRM.ArrivalDate
                    && this.DepartureDate == objRM.DepartureDate
                    && this.CheckIn == objRM.CheckIn
                    && this.Customer == objRM.Customer
                    && this.Room == objRM.Room;
            }
            else return base.Equals(obj);
        }
    }
}