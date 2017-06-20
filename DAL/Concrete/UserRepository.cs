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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalUser> GetAll()
        {
           return context.Set<User>().ToList().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int id)
        {
            return context.Set<User>().FirstOrDefault(u => u.UserId == id)?.ToDalUser();
        }

        public void Create(DalUser entity)
        {
            var role = context.Set<Role>().FirstOrDefault(r => r.Name == "user");
            entity.ToOrmUser().Roles.Add(role);
            context.Set<User>().Add(entity.ToOrmUser());
        }

        public void Delete(DalUser entity)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
            context.Set<User>().Remove(user);
        }

        public void Update(DalUser entity)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.Id);
            if (user != null)
            {
                user.UserId = entity.Id;
                user.Email = entity.Email;
                user.Login = entity.Login;
                user.Password = entity.Password;
                user.FirstName = entity.LastName;
                user.LastName = entity.LastName;
                user.Money = entity.Money;
            }
        }

        public void DepositMoney(decimal amount, int userId)
        {
            var user = context.Set<User>().FirstOrDefault(profile => profile.UserId == userId);
            if (user != null)
                user.Money += amount;
        }

        public void WithDrawMoney(decimal amount, int userId)
        {
            var user = context.Set<User>().FirstOrDefault(profile => profile.UserId == userId);
            if (user != null)
                user.Money -= amount;
        }

        public DalUser GetUserByEmail(string email)
        {
            return context.Set<User>().FirstOrDefault(u => u.Email.ToLower() == email.ToLower())?.ToDalUser();
        }

        public DalUser GetUserByLogin(string login)
        {
            return context.Set<User>().FirstOrDefault(u => u.Login.ToLower() == login.ToLower())?.ToDalUser();
        }                
    }
}
