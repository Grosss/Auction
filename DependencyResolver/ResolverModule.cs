using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL;
using DAL.Concrete;
using DAL.Interface;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;
using ORM.Entities;

namespace DependencyResolver
{
    public static class ResolverModule
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<AuctionContext>().InRequestScope();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<ILotRepository>().To<LotRepositpry>();
            kernel.Bind<IBidRepository>().To<BidRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<ILotService>().To<LotService>();
            kernel.Bind<IBidService>().To<BidService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();        
        }
    }
}
