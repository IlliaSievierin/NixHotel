using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class CustomerRepository : IRepository<Customer>
    {
        private HotelContext db;

        public CustomerRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers;
        }

        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        public void Create(Customer customer)
        {
            db.Customers.Add(customer);
        }

        public void Delete(int id)
        {
            Customer customer = Get(id);
            if (customer != null)
                db.Customers.Remove(customer);
        }

        public void Update(Customer newItem, int id)
        {
            Customer customer = Get(id);
            if (customer != null)
            {
                customer.Id = newItem.Id;
                customer.FirstName = newItem.FirstName;
                customer.MiddleName = newItem.MiddleName;
                customer.LastName = newItem.LastName;
                customer.DateOfBirth = newItem.DateOfBirth;
                customer.Passport = newItem.Passport;

                db.Entry(customer).State = EntityState.Modified;
            }
        }
    }
}
