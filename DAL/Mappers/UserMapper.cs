using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this User user)
        {            
            DalUser dalUser = new DalUser
            {
                Id = user.UserId,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Money = user.Money
            };

            return dalUser;
        }

        public static User ToOrmUser(this DalUser user)
        {
            User ormUser = new User
            {
                UserId = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Money = user.Money
            };

            return ormUser;
        }
    }
}
