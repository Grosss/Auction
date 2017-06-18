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
    public class LotService : ILotService
    {
        private readonly IUnitOfWork uow;
        private readonly ILotRepository lotRepository;

        public LotService(IUnitOfWork uow, ILotRepository repository)
        {
            this.uow = uow;
            this.lotRepository = repository;
        }

        public IEnumerable<LotEntity> GetAllLots()
        {
            return lotRepository.GetAll().Select(lot => lot.ToBllLot());
        }

        public LotEntity GetLotById(int id)
        {
            return lotRepository.GetById(id).ToBllLot();
        }

        public void CreateLot(LotEntity entity)
        {
            lotRepository.Create(entity.ToDalLot());
            uow.Commit();
        }

        public void DeleteLot(LotEntity entity)
        {
            lotRepository.Delete(entity.ToDalLot());
            uow.Commit();
        }

        public void UpdateLot(LotEntity entity)
        {
            lotRepository.Update(entity.ToDalLot());
            uow.Commit();
        }

        public IEnumerable<LotEntity> GetAllLotsForCategory(int categoryId)
        {
            return lotRepository.GetAllLotsForCategory(categoryId).Select(lot => lot.ToBllLot());
        }

        public IEnumerable<LotEntity> GetAllLotsForUser(int userId)
        {
            return lotRepository.GetAllLotsForCategory(userId).Select(lot => lot.ToBllLot());
        }
    }
}
