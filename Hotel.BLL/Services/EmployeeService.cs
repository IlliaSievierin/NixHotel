using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperEmployee;
        private IMapper mapperEmployeeReverse;

        public EmployeeService(IWorkUnit database)
        {
            Database = database;
            mapperEmployee = new MapperConfiguration(cfg =>
               cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            mapperEmployeeReverse = new MapperConfiguration(cfg =>
             cfg.CreateMap<EmployeeDTO, Employee>()).CreateMapper();

        }
        public IEnumerable<EmployeeDTO> GetAll()
        {
            return mapperEmployee.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAll());
        }
        public EmployeeDTO Get(int id)
        {
            return mapperEmployee.Map<Employee, EmployeeDTO>(Database.Employees.Get(id));
        }

        public void Create(EmployeeDTO item)
        {
            Database.Employees.Create(mapperEmployeeReverse.Map<EmployeeDTO, Employee>(item));
            Database.Save();
        }
    }
}
