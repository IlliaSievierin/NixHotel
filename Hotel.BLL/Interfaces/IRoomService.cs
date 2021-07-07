using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAll();
        RoomDTO Get(int id);
        void Create(RoomDTO item);
        void Delete(int id);

        void Update(RoomDTO newRoom, int id);
        IEnumerable<RoomDTO> GetFreeRooms(DateTime dateCheck);
    }
}
