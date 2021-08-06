using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class PriceCategoryModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is PriceCategoryModel)
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