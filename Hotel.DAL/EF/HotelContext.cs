using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.EF
{
    public partial class HotelContext : DbContext
    {
        public HotelContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<HotelContext>(new HotelInitializer());
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PriceCategory> PriceCategories { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

    }
}
