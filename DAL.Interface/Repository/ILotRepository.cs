using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface ILotRepository : IRepository<DalLot>
    {
        IEnumerable<DalLot> GetAllLotsForCategory(int categoryId);
        IEnumerable<DalLot> GetAllLotsForUser(int userId);
    }
}
