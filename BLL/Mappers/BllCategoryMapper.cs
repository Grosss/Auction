using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllCategoryMapper
    {
        public static CategoryEntity ToBllCategory(this DalCategory category)
        {
            if (category == null)
                return null;

            CategoryEntity bllCategory = new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name
            };

            return bllCategory;
        }

        public static DalCategory ToDalCategory(this CategoryEntity category)
        {
            if (category == null)
                return null;

            DalCategory dalCategory = new DalCategory
            {
                Id = category.Id,
                Name = category.Name
            };

            return dalCategory;
        }        
    }
}
