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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public UserEntity GetUserById(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public void CreateUser(UserEntity entity)
        {
            userRepository.Create(entity.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(UserEntity entity)
        {
            userRepository.Delete(entity.ToDalUser());
            uow.Commit();
        }

        public void UpdateUser(UserEntity entity)
        {
            userRepository.Update(entity.ToDalUser());
            uow.Commit();
        }

        public void DepositMoneyToUser(decimal amount, int userId)
        {
            userRepository.DepositMoney(amount, userId);
            uow.Commit();
        }

        public void WithDrawMoneyFromUser(decimal amount, int userId)
        {
            userRepository.WithDrawMoney(amount, userId);
            uow.Commit();
        }          

        public UserEntity GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email).ToBllUser();
        }        

        public UserEntity GetUserByLogin(string login)
        {
            return userRepository.GetUserByLogin(login).ToBllUser();
        }        
    }
}
