using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IPriceCategoryService
    {
        IEnumerable<PriceCategoryDTO> GetAll();
        PriceCategoryDTO Get(int id);
        void Create(PriceCategoryDTO item);
        void Delete(int id);
    }
}
