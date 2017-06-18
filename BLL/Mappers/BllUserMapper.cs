using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllUserMapper
    {
        public static UserEntity ToBllUser(this DalUser user)
        {
            UserEntity bllUser = new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Money = user.Money
            };

            return bllUser;
        }

        public static DalUser ToDalUser(this UserEntity user)
        {
            DalUser dalUser = new DalUser
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Money = user.Money
            };

            return dalUser;
        }        
    }
}
