using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Models;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace PL.Infrastructure.Mappers
{
    public static class BidMapper
    {
        public static BidViewModel ToMvcBid(this BidEntity bid, IUserService userService, ILotService lotService)
        {
            BidViewModel vmBid = new BidViewModel
            {
                Id = bid.Id,
                LotId = bid.LotId,
                LotTitle = lotService.GetLotById(bid.LotId).Name,
                Price = bid.Price,
                DateOfBid = bid.DateOfBid,
                UserName = userService.GetUserById(bid.UserId).Login                                        
            };

            return vmBid;
        }

        public static BidEntity ToBllBid(this BidViewModel bid, IUserService userService)
        {
            BidEntity bllBid = new BidEntity
            {
                Id = bid.Id,
                Price = bid.Price,
                DateOfBid = bid.DateOfBid,
                LotId = bid.LotId,
                UserId = userService.GetUserByLogin(bid.UserName).Id
            };

            return bllBid;
        }
    }
}