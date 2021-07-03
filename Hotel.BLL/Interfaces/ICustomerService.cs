using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO Get(int id);
        void Create(CustomerDTO item);
        void Delete(int id);
    }
}
