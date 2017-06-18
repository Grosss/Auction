using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;
using DAL.Interface.DTO;


namespace DAL.Mappers
{
    public static class BidMapper
    {
        public static DalBid ToDalBid(this Bid bid)
        {
            DalBid dalBid = new DalBid
            {
                Id = bid.BidId,
                Price = bid.Price,
                DateOfBid = bid.DateOfBid,
                LotId = bid.LotId,
                UserId = bid.UserId                                                
            };

            return dalBid;
        }

        public static Bid ToOrmBid(this DalBid bid)
        {
            Bid ormBid = new Bid
            {
                BidId = bid.Id,
                Price = bid.Price,
                DateOfBid = bid.DateOfBid,
                LotId = bid.LotId,
                UserId = bid.UserId
            };

            return ormBid;
        }
    }
}
