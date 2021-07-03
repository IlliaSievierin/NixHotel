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
    }
}