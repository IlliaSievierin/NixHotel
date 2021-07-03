using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public  class Customer
    {
        
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
        }

     
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Passport { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
