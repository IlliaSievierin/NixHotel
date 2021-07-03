using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public partial class Room
    { 
        public Room()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        public int CategoryId { get; set; }

        public bool Active { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
