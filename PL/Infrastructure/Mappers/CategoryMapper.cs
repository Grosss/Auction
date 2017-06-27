using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using PL.Models;

namespace PL.Infrastructure.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryViewModel ToMvcCategory(this CategoryEntity category)
        {
            CategoryViewModel vmCategory = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return vmCategory;
        }

        public static CategoryEntity ToBllCategory(this CategoryViewModel category)
        {
            CategoryEntity bllCategory = new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name
            };

            return bllCategory;
        }
    }
}
