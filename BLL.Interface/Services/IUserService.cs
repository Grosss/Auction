using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        IEnumerable<UserEntity> GetAllUsers();
        UserEntity GetUserById(int id);
        void CreateUser(UserEntity entity);
        void DeleteUser(UserEntity entity);
        void UpdateUser(UserEntity entity);
        void DepositMoneyToUser(decimal amount, int userId);
        void WithDrawMoneyFromUser(decimal amount, int userId);
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByLogin(string login);
    }
}
