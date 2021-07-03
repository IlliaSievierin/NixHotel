using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Category
    {
        public Category()
        {
            PriceCategory = new HashSet<PriceCategory>();
            Room = new HashSet<Room>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string CategoryName { get; set; }

        public int Bed { get; set; }

        public virtual ICollection<PriceCategory> PriceCategory { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
