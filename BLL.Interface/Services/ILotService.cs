using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface ILotService
    {
        IEnumerable<LotEntity> GetAllLots();
        LotEntity GetLotById(int id);
        void CreateLot(LotEntity entity);
        void DeleteLot(LotEntity entity);
        void UpdateLot(LotEntity entity);
        IEnumerable<LotEntity> GetAllLotsForCategory(int categoryId);
        IEnumerable<LotEntity> GetAllLotsForUser(int userId);
    }
}
