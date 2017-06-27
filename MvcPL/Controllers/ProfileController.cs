using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ILotService lotService;
        private readonly IBidService bidService;
        private readonly ICategoryService categoryService;
        private readonly int pageSize = 3;

        public ProfileController(IUserService userService, IRoleService roleService, 
            ILotService lotService, IBidService bidService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.lotService = lotService;
            this.bidService = bidService;
            this.categoryService = categoryService;
        }

        public ActionResult MyBids(int? page)
        {
            if (page == null || page < 1)
                page = 1;

            int userId = userService.GetUserByEmail(User.Identity.Name).Id;
            
            var bids = bidService.GetAllBidsForUser(userId)
                .OrderByDescending(b => b.DateOfBid)
                .Select(b => b.ToMvcBid(userService, lotService))
                .Skip((page.Value - 1) * pageSize).Take(pageSize);

            var model = new ResultViewModel<BidViewModel>(bids, bids.Count() / pageSize, page.Value);

            if (bids == null)
                ViewBag.NoBids = "There is no bids yet";

            return View(model);
        }

        public ActionResult MyLots(int? page)
        {
            if (page == null || page < 1)
                page = 1;

            int userId = userService.GetUserByEmail(User.Identity.Name).Id;

            var lots = lotService.GetAllLotsForUser(userId)
                .OrderBy(l => l.ExpirationTime)
                .Select(l => l.ToMvcLot())
                .Skip((page.Value - 1) * pageSize).Take(pageSize);

            var model = new ResultViewModel<LotViewModel>(lots, lots.Count() / pageSize, page.Value);

            if (lots == null)
                ViewBag.NoLots = "There is no bids yet";

            return View(model);
        }
    }
}