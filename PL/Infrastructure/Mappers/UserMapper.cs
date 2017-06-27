using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Models;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace PL.Infrastructure.Mappers
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

        public static EditUserViewModel ToMvcEditUser(this UserEntity user, IRoleService roleService)
        {
            EditUserViewModel edUser = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roleService.GetUserRoles(user.Id).Select(r => r.Name)
            };

            return edUser;
        }

        public static UserEntity ToBllUser(this EditUserViewModel user, IUserService userService)
        {
            UserEntity bllUser = new UserEntity
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Password = userService.GetUserById(user.Id).Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Money = 0
            };

            return bllUser;
        }
    }
}