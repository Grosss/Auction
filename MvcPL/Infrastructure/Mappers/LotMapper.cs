using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models;
using BLL.Interface.Entities;

namespace MvcPL.Infrastructure.Mappers
{
    public static class LotMapper
    {
        public static LotViewModel ToMvcLot(this LotEntity lot)
        {
            LotViewModel vmLot = new LotViewModel
            {
                Id = lot.Id,
                Name = lot.Name,
                Price = lot.Price,
                Image = lot.Image,
                ExpirationTime = lot.ExpirationTime,
                Description = lot.Description,
                UserId = lot.UserId,
                CategoryId = lot.CategoryId
            };

            return vmLot;
        }

        public static LotEntity ToBllLot(this LotViewModel lot)
        {
            LotEntity bllLot = new LotEntity
            {
                Id = lot.Id,
                Name = lot.Name,
                Price = lot.Price,
                Image = lot.Image,
                ExpirationTime = lot.ExpirationTime,
                Description = lot.Description,
                UserId = lot.UserId,
                CategoryId = lot.CategoryId
            };

            return bllLot;
        }
    }
}
