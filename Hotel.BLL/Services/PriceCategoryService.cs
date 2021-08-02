using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class PriceCategoryService : IPriceCategoryService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperPriceCategory;
        private IMapper mapperPriceCategoryReverse;
        public PriceCategoryService(IWorkUnit database)
        {
            Database = database;
            mapperPriceCategory = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategory, PriceCategoryDTO>()
              ).CreateMapper();

            mapperPriceCategoryReverse = new MapperConfiguration(cfg =>
              cfg.CreateMap<PriceCategoryDTO, PriceCategory>()
              ).CreateMapper();
        }
      
        public IEnumerable<PriceCategoryDTO> GetAll()
        {

            return mapperPriceCategory.Map<IEnumerable<PriceCategory>, List<PriceCategoryDTO>>(Database.PriceCategories.GetAll());
        }

        public PriceCategoryDTO Get(int id)
        {
            return mapperPriceCategory.Map<PriceCategory, PriceCategoryDTO>(Database.PriceCategories.Get(id));
        }

        public void Create(PriceCategoryDTO item)
        {
          

            Database.PriceCategories.Create(mapperPriceCategoryReverse.Map<PriceCategoryDTO, PriceCategory>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            var data = Database.PriceCategories.Get(id);
            if (data != null)
            {
                Database.PriceCategories.Delete(id);
                Database.Save();
            }
        }
    }
}
