using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Models;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace PL.Infrastructure.Mappers
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
                CategoryId = lot.CategoryId.ToString()
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
                CategoryId = int.Parse(lot.CategoryId)
            };

            return bllLot;
        }
    }
}
