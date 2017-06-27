using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models;
using BLL.Interface.Entities;

namespace MvcPL.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static RegisterViewModel ToMvcUser(this UserEntity user)
        {
            RegisterViewModel regUser = new RegisterViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return regUser;
        }

        public static UserEntity ToBllUser(this RegisterViewModel user)
        {
            UserEntity bllUser = new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Money = 0
            };

            return bllUser;
        }
    }
}