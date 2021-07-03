using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private IWorkUnit Database { get; set; }

        public CustomerService(IWorkUnit database)
        {
            Database = database;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => 
               cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(Database.Customers.GetAll());
        }

        public CustomerDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();

            return mapper.Map<Customer, CustomerDTO>(Database.Customers.Get(id));
        }

        public void Create(CustomerDTO item)
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<CustomerDTO, Customer>()).CreateMapper();

            Database.Customers.Create(mapper.Map<CustomerDTO, Customer>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            var data = Database.Customers.Get(id);
            if (data != null)
            {
                Database.Customers.Delete(id);
                Database.Save();
            }

        }
    }    
}
