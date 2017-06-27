using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORM.Entities;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class BidRepository : IBidRepository
    {
        private readonly DbContext context;

        public BidRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalBid> GetAll()
        {
            return context.Set<Bid>().ToList().Select(bid => bid.ToDalBid());
        }

        public DalBid GetById(int id)
        {
            return context.Set<Bid>().FirstOrDefault(b => b.BidId == id)?.ToDalBid();
        }

        public void Create(DalBid entity)
        {
            context.Set<Bid>().Add(entity.ToOrmBid());
        }

        public void Delete(DalBid entity)
        {
            var category = context.Set<Bid>().FirstOrDefault(b => b.BidId == entity.Id);
            context.Set<Bid>().Remove(category);
        }

        public void Update(DalBid entity)
        {
            var bid = context.Set<Bid>().FirstOrDefault(b => b.BidId == entity.Id);
            if (bid != null)
            {
                bid.Price = entity.Price;
                bid.DateOfBid = entity.DateOfBid;
                bid.LotId = entity.LotId;
                bid.UserId = entity.UserId;
            }
        }

        public IEnumerable<DalBid> GetAllBidsForLot(int lotId)
        {
            return context.Set<Bid>().ToList().Where(bid => bid.LotId == lotId).Select(bid => bid.ToDalBid());
        }

        public IEnumerable<DalBid> GetAllBidsForUser(int userId)
        {
            return context.Set<Bid>().ToList().Where(bid => bid.UserId == userId).Select(bid => bid.ToDalBid());
        }

        public DalBid GetLastBidForLot(int lotId)
        {
            return context.Set<Bid>().ToList().Where(bid => bid.LotId == lotId).OrderByDescending(x => x.Price).FirstOrDefault()?.ToDalBid();
        }
    }
}
