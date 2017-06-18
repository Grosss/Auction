using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uow;
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(IUnitOfWork uow, ICategoryRepository repository)
        {
            this.uow = uow;
            this.categoryRepository = repository;
        }

        public IEnumerable<CategoryEntity> GetAllCategories()
        {
            return categoryRepository.GetAll().Select(category => category.ToBllCategory());
        }

        public CategoryEntity GetCategoryById(int id)
        {
            return categoryRepository.GetById(id).ToBllCategory();
        }

        public void CreateCategory(CategoryEntity entity)
        {
            categoryRepository.Create(entity.ToDalCategory());
            uow.Commit();
        }

        public void DeleteCategory(CategoryEntity entity)
        {
            categoryRepository.Delete(entity.ToDalCategory());
            uow.Commit();
        }

        public void UpdateCategory(CategoryEntity entity)
        {
            categoryRepository.Update(entity.ToDalCategory());
            uow.Commit();
        }

        public CategoryEntity GetCategoryByName(string name)
        {
            return categoryRepository.GetCategoryByName(name).ToBllCategory();
        }
    }
}
