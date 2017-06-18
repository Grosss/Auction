using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllLotMapper
    {
        public static LotEntity ToBllLot(this DalLot lot)
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

        public static DalLot ToDalLot(this LotEntity lot)
        {
            DalLot dalLot = new DalLot
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

            return dalLot;
        }       
    }
}
