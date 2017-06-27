using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORM.Entities;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext context;

        public CategoryRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalCategory> GetAll()
        {
            return context.Set<Category>().ToList().Select(category => category.ToDalCategory());
        }

        public DalCategory GetById(int id)
        {
            return context.Set<Category>().FirstOrDefault(c => c.CategoryId == id)?.ToDalCategory();
        }

        public void Create(DalCategory entity)
        {
            context.Set<Category>().Add(entity.ToOrmCategory());
        }

        public void Delete(DalCategory entity)
        {
            var category = context.Set<Category>().FirstOrDefault(c => c.CategoryId == entity.Id);
            context.Set<Category>().Remove(category);
        }

        public void Update(DalCategory entity)
        {
            var category = context.Set<Category>().FirstOrDefault(c => c.CategoryId == entity.Id);
            if (category != null)
            {
                category.Name = entity.Name;
            }
        }

        public DalCategory GetCategoryByName(string name)
        {
            return context.Set<Category>().FirstOrDefault(c => c.Name == name)?.ToDalCategory();
        }
    }
}
