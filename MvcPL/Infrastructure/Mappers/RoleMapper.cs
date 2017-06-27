using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models;
using BLL.Interface.Entities;

namespace MvcPL.Infrastructure.Mappers
{
    public static class RoleMapper
    {
        public static RoleViewModel ToMvcRole(this RoleEntity role)
        {
            RoleViewModel vmRole = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return vmRole;
        }

        public static RoleEntity ToBllRole(this RoleViewModel role)
        {
            RoleEntity bllRole = new RoleEntity
            {
                Id = role.Id,
                Name = role.Name,
            };

            return bllRole;
        }
    }
}
