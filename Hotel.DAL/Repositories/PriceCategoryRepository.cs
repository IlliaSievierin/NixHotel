using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class PriceCategoryRepository : IRepository<PriceCategory>
    {
        private HotelContext db;

        public PriceCategoryRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<PriceCategory> GetAll()
        {
            return db.PriceCategories;
        }

        public PriceCategory Get(int id)
        {
            return db.PriceCategories.Find(id);
        }

        public void Create(PriceCategory priceCategory)
        {
            db.PriceCategories.Add(priceCategory);
        }

        public void Delete(int id)
        {
            PriceCategory priceCategory = Get(id);
            if (priceCategory != null)
                db.PriceCategories.Remove(priceCategory);
        }

        public void Update(PriceCategory newItem, int id)
        {
            PriceCategory priceCategory = Get(id);
            if (priceCategory != null)
            {
                priceCategory.Id = newItem.Id;
                priceCategory.Price = newItem.Price;
                priceCategory.StartDate = newItem.StartDate;
                priceCategory.EndDate = newItem.EndDate;
                priceCategory.CategoryId = newItem.CategoryId;

                db.Entry(priceCategory).State = EntityState.Modified;
            }
        }
    }
}
