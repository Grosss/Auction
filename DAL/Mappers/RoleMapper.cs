using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    public static class RoleMapper
    {
        public static DalRole ToDalRole(this Role role)
        {
            DalRole dalRole = new DalRole
            {
                Id = role.RoleId,
                Name = role.Name
            };

            return dalRole;
        }

        public static Role ToOrmRole(this DalRole role)
        {
            Role ormRole = new Role
            {
                RoleId = role.Id,
                Name = role.Name,
            };

            return ormRole;
        }
    }
}
