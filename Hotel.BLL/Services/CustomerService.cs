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
        private IMapper mapperCustomer;
        private IMapper mapperCustomerReverse;


        public CustomerService(IWorkUnit database)
        {
            Database = database;
            mapperCustomer = new MapperConfiguration(cfg =>
               cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();
            mapperCustomerReverse = new MapperConfiguration(cfg =>
             cfg.CreateMap<CustomerDTO, Customer>()).CreateMapper();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            return mapperCustomer.Map<IEnumerable<Customer>, List<CustomerDTO>>(Database.Customers.GetAll());
        }

        public CustomerDTO Get(int id)
        {
            return mapperCustomer.Map<Customer, CustomerDTO>(Database.Customers.Get(id));
        }

        public void Create(CustomerDTO item)
        {
            Database.Customers.Create(mapperCustomerReverse.Map<CustomerDTO, Customer>(item));
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
