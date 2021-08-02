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
    class EmployeeRepository : IRepository<Employee>
    {
        private HotelContext db;

        public EmployeeRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        public void Create(Employee employee)
        {
            db.Employees.Add(employee);
        }

        public void Delete(int id)
        {
            Employee employee = Get(id);
            if (employee != null)
                db.Employees.Remove(employee);
        }

        public void Update(Employee newItem, int id)
        {
            Employee employee = Get(id);
            if (employee != null)
            {
                employee.Id = newItem.Id;
                employee.Login = newItem.Login;
                employee.Password = newItem.Password;

                db.Entry(employee).State = EntityState.Modified;
            }
        }
    
    }
}
