using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        
        public string CategoryName { get; set; }
        public int Bed { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is CategoryModel)
            {
                var objCM = obj as CategoryModel;
                return this.CategoryName == objCM.CategoryName
                    && this.Bed == objCM.Bed;
            }
            else return base.Equals(obj);
        }
    }
}