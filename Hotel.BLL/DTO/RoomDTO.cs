using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int CategoryId { get; set; }
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is RoomDTO)
            {
                var objRM = obj as RoomDTO;
                return this.RoomNumber == objRM.RoomNumber
                    && this.Active == objRM.Active
                    && this.CategoryId == objRM.CategoryId;
            }
            else return base.Equals(obj);
        }
    }
}
