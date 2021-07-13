using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class PriceCategoryDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryDTO Category { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is PriceCategoryDTO)
            {
                var objPCM = obj as PriceCategoryDTO;
                return this.Price == objPCM.Price
                    && this.StartDate == objPCM.StartDate
                    && this.EndDate == objPCM.EndDate;
            }
            else return base.Equals(obj);
        }
    }
}
