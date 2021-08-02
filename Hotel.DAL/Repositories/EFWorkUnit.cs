using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    public class EFWorkUnit : IWorkUnit
    {
        private HotelContext db;
        private CategoryRepository categoryRepository;
        private CustomerRepository customerRepository;
        private PriceCategoryRepository priceCategoryRepository;
        private ReservationRepository reservationRepository;
        private RoomRepository roomRepository;
        private EmployeeRepository employeeRepository;

        public EFWorkUnit(string connectionString)
        {
            db = new HotelContext(connectionString);
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }
                return categoryRepository;
            }
        }
        public IRepository<Customer> Customers
        {
            get 
            {
                if(customerRepository == null)
                {
                    customerRepository = new CustomerRepository(db);
                }
                return customerRepository;
            }
        }
        public IRepository<PriceCategory> PriceCategories
        {
            get
            {
                if (priceCategoryRepository == null)
                {
                    priceCategoryRepository = new PriceCategoryRepository(db);
                }
                return priceCategoryRepository;
            }
        }
        public IRepository<Reservation> Reservations
        {
            get
            {
                if (reservationRepository == null)
                {
                    reservationRepository = new ReservationRepository(db);
                }
                return reservationRepository;
            }
        }
        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(db);
                }
                return roomRepository;
            }
        }
        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository(db);
                }
                return employeeRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
