using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IBidRepository : IRepository<DalBid>
    {
        IEnumerable<DalBid> GetAllBidsForLot(int lotId);
        IEnumerable<DalBid> GetAllBidsForUser(int lotId);
        DalBid GetLastBidForLot(int lotId);
    }
}
