using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostel.API.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public CategoryModel Category { get; set; }
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is RoomModel)
            {
                var objRM = obj as RoomModel;
                return this.RoomNumber == objRM.RoomNumber
                    && this.Active == objRM.Active
                    && this.Category == objRM.Category;
            }
            else return base.Equals(obj);
        }
    }
}