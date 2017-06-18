using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllRoleMapper
    {
        public static RoleEntity ToBllRole(this DalRole role)
        {
            RoleEntity bllRole = new RoleEntity
            {
                Id = role.Id,
                Name = role.Name,
            };

            return bllRole;
        }

        public static DalRole ToDalRole(this RoleEntity role)
        {
            DalRole dalRole = new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };

            return dalRole;
        }        
    }
}
