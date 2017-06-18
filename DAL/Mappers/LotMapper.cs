using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    public static class LotMapper
    {
        public static DalLot ToDalLot(this Lot lot)
        {            
            DalLot dalLot = new DalLot
            {
                Id = lot.LotId,
                Name = lot.Name,
                Price = lot. Price,
                Image = lot.Image,
                ExpirationTime = lot.ExpirationTime,
                Description = lot.Description,
                UserId = lot.UserId,
                CategoryId = lot.CategoryId
            };

            return dalLot;
        }

        public static Lot ToOrmLot(this DalLot lot)
        {
            Lot ormLot = new Lot
            {
                LotId = lot.Id,
                Name = lot.Name,
                Price = lot.Price,
                Image = lot.Image,
                ExpirationTime = lot.ExpirationTime,
                Description = lot.Description,
                UserId = lot.UserId,
                CategoryId = lot.CategoryId
            };

            return ormLot;
        }
    }
}
