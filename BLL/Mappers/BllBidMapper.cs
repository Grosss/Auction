using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllBidMapper
    {
        public static BidEntity ToBllBid(this DalBid bid)
        {
            BidEntity bllBid = new BidEntity
            {
                Id = bid.Id,
                Price = bid.Price,
                DateOfBid = bid.DateOfBid,
                LotId = bid.LotId,
                UserId = bid.UserId
            };

            return bllBid;
        }

        public static DalBid ToDalBid(this BidEntity bid)
        {
            DalBid dalBid = new DalBid
            {
                Id = bid.Id,
                Price = bid.Price,
                DateOfBid = bid.DateOfBid,
                LotId = bid.LotId,
                UserId = bid.UserId
            };

            return dalBid;
        }
    }
}
