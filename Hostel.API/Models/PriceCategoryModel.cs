using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostel.API.Models
{
    public class PriceCategoryModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CategoryModel Category { get; set; }
        public override bool Equals(object obj)
        {
            if(obj is PriceCategoryModel)
            {
                var objPCM = obj as PriceCategoryModel;
                return this.Price == objPCM.Price
                    && this.StartDate == objPCM.StartDate
                    && this.EndDate == objPCM.EndDate;
            }
            else return base.Equals(obj);
        }
    }
}