using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IBidService
    {
        IEnumerable<BidEntity> GetAllBids();
        BidEntity GetBidById(int id);
        void CreateBid(BidEntity entity);
        void DeleteBid(BidEntity entity);
        void UpdateBid(BidEntity entity);
        IEnumerable<BidEntity> GetAllBidsForLot(int lotId);
        BidEntity GetLastBidForLot(int lotId);
    }
}
