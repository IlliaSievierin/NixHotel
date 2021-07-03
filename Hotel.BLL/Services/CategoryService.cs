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

        public CategoryService(IWorkUnit database)
        {
            Database = database;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg =>
              cfg.CreateMap<Category, CategoryDTO>()
              ).CreateMapper();

            return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
        }

        public CategoryDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
             cfg.CreateMap<Category, CategoryDTO>()
             ).CreateMapper();

            return mapper.Map<Category, CategoryDTO>(Database.Categories.Get(id));
        }

        public void Create(CategoryDTO item)
        {
            var mapper = new MapperConfiguration(cfg =>
             cfg.CreateMap<CategoryDTO, Category>()
             ).CreateMapper(); 

            Database.Categories.Create(mapper.Map<CategoryDTO, Category>(item));
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
