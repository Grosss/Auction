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
    public class LotRepositpry : ILotRepository
    {
        private readonly DbContext context;

        public LotRepositpry(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalLot> GetAll()
        {
            return context.Set<Lot>().ToList().Select(lot => lot.ToDalLot());
        }

        public DalLot GetById(int id)
        {
            return context.Set<Lot>().FirstOrDefault(l => l.LotId == id)?.ToDalLot();
        }

        public void Create(DalLot entity)
        {
            context.Set<Lot>().Add(entity.ToOrmLot());
        }

        public void Delete(DalLot entity)
        {
            var lot = context.Set<Lot>().FirstOrDefault(l => l.LotId == entity.Id);
            context.Set<Lot>().Remove(lot);
        }

        public void Update(DalLot entity)
        {
            var lot = context.Set<Lot>().FirstOrDefault(l => l.LotId == entity.Id);
            if (lot != null)
            {
                lot.LotId = entity.Id;
                lot.Name = entity.Name;
                lot.Price = entity.Price;
                lot.Image = entity.Image;
                lot.ExpirationTime = entity.ExpirationTime;
                lot.Description = entity.Description;
                lot.UserId = entity.UserId;
                lot.CategoryId = entity.CategoryId;
            }
        }

        public IEnumerable<DalLot> GetAllLotsForCategory(int categoryId)
        {
            return context.Set<Lot>().ToList().Where(lot => lot.CategoryId == categoryId).Select(lot => lot.ToDalLot());
        }

        public IEnumerable<DalLot> GetAllLotsForUser(int userId)
        {
            return context.Set<Lot>().ToList().Where(lot => lot.UserId == userId).Select(lot => lot.ToDalLot());
        }
    }
}
