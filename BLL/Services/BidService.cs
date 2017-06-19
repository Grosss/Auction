using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class BidService : IBidService
    {
        private readonly IUnitOfWork uow;
        private readonly IBidRepository bidRepository;

        public BidService(IUnitOfWork uow, IBidRepository repository)
        {
            this.uow = uow;
            this.bidRepository = repository;
        }

        public IEnumerable<BidEntity> GetAllBids()
        {
            return bidRepository.GetAll().Select(bid => bid.ToBllBid());
        }
        
        public BidEntity GetBidById(int id)
        {
            return bidRepository.GetById(id).ToBllBid();
        }

        public void CreateBid(BidEntity entity)
        {
            bidRepository.Create(entity.ToDalBid());
            uow.Commit();
        }

        public void DeleteBid(BidEntity entity)
        {
            bidRepository.Delete(entity.ToDalBid());
            uow.Commit();
        }

        public void UpdateBid(BidEntity entity)
        {
            bidRepository.Update(entity.ToDalBid());
            uow.Commit();
        }

        public IEnumerable<BidEntity> GetAllBidsForLot(int lotId)
        {
            return bidRepository.GetAllBidsForLot(lotId).Select(bid => bid.ToBllBid());
        }

        public BidEntity GetLastBidForLot(int lotId)
        {
            return bidRepository.GetLastBidForLot(lotId).ToBllBid();
        }        
    }
}
