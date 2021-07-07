using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperCategory;
        private IMapper mapperCategoryReverse;

        public CategoryService(IWorkUnit database)
        {
            Database = database;
            mapperCategory = new MapperConfiguration(cfg =>
               cfg.CreateMap<Category, CategoryDTO>()
              ).CreateMapper();
            mapperCategoryReverse = new MapperConfiguration(cfg =>
             cfg.CreateMap<CategoryDTO, Category>()
             ).CreateMapper();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return mapperCategory.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
        }

        public CategoryDTO Get(int id)
        {
            return mapperCategory.Map<Category, CategoryDTO>(Database.Categories.Get(id));
        }

        public void Create(CategoryDTO item)
        {
            Database.Categories.Create(mapperCategoryReverse.Map<CategoryDTO, Category>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            var data = Database.Categories.Get(id);
            if (data != null)
            {
                Database.Categories.Delete(id);
                Database.Save();
            }
        }
    }
}
