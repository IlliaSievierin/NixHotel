using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<ReservationDTO> GetAll();
        ReservationDTO Get(int id);
        void Create(ReservationDTO item);
        void Delete(int id);

        void Update(ReservationDTO newReservation, int id);
        decimal GetProfitForMonth(DateTime date);
    }
}
