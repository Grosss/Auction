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
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().ToList().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int id)
        {
            return context.Set<Role>().FirstOrDefault(r => r.RoleId == id)?.ToDalRole();
        }

        public void Create(DalRole entity)
        {
            context.Set<Role>().Add(entity.ToOrmRole());
        }

        public void Delete(DalRole entity)
        {
            var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == entity.Id);
            context.Set<Role>().Remove(role);
        }

        public void Update(DalRole entity)
        {
            var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == entity.Id);
            if (role != null)
            {
                role.Name = entity.Name;
            }
        }

        public void AddRoleToUser(int userId, int roleId)
        {
            var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == roleId);
            context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Roles.Add(role);
        }

        public void RemoveRoleFromUser(int userId, int roleId)
        {
            var role = context.Set<Role>().FirstOrDefault(r => r.RoleId == roleId);
            context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Roles.Remove(role);
        }

        public IEnumerable<DalRole> GetUserRoles(int userId)
        {
            return context.Set<User>().FirstOrDefault(user => user.UserId == userId)?.Roles.Select(role => role.ToDalRole());
        }
    }
}
