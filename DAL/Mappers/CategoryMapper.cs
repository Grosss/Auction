using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    public static class CategoryMapper
    {
        public static DalCategory ToDalCategory(this Category category)
        {
            DalCategory dalCategory = new DalCategory
            {
                Id = category.CategoryId,
                Name = category.Name
            };

            return dalCategory;
        }

        public static Category ToOrmCategory(this DalCategory category)
        {
            Category ormCategory = new Category
            {
                CategoryId = category.Id,
                Name = category.Name
            };

            return ormCategory;
        }
    }
}
