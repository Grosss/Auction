using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryEntity> GetAllCategories();
        CategoryEntity GetCategoryById(int id);
        void CreateCategory(CategoryEntity entity);
        void DeleteCategory(CategoryEntity entity);
        void UpdateCategory(CategoryEntity entity);
        CategoryEntity GetCategoryByName(string name);
    }
}
