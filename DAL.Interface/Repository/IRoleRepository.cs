using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        void AddRoleToUser(int userId, int roleId);
        void RemoveRoleFromUser(int userId, int roleId);
        IEnumerable<DalRole> GetUserRoles(int userId);
    }
}