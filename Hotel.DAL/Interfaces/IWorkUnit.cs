using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Interfaces
{
    public interface IWorkUnit
    {
        IRepository<Category> Categories { get; }
        IRepository<Customer> Customers { get; }
        IRepository<PriceCategory> PriceCategories { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Employee> Employees { get; }
        void Save();
    }
}
