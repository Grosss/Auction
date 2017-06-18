using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        void DepositMoney(decimal amount, int userId);
        void WithDrawMoney(decimal amount, int userId);
        DalUser GetUserByEmail(string email);
        DalUser GetUserByLogin(string login);
    }
}
