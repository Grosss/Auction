using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using PL.Infrastructure.Mappers;
using PL.Models;

namespace PL.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ILotService lotService;
        private readonly IBidService bidService;
        private readonly ICategoryService categoryService;
        private readonly int pageSize = 3;

        public AuctionController(IUserService userService, IRoleService roleService,
            ILotService lotService, IBidService bidService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.lotService = lotService;
            this.bidService = bidService;
            this.categoryService = categoryService;
        }

        public ActionResult Index(int? page, string searchResult = "")
        {
            if (page == null || page < 1)
                page = 1;

            int totalPages = lotService.GetAllLots()
                .Where(l => l.Name.Contains(searchResult))
                .Count();

            var lots = lotService.GetAllLots()
                .Where(l => l.Name.Contains(searchResult))
                .OrderByDescending(l => l.ExpirationTime)
                .Select(l => l.ToMvcLot())
                .Skip((page.Value - 1) * pageSize).Take(pageSize);

            var model = new ResultListViewModel<LotViewModel>(lots, (int)Math.Ceiling(totalPages / (double)pageSize), page.Value);

            ViewBag.SearchResultString = searchResult;
            if (lots.Count() == 0)
                ViewBag.NoLots = "Nothing was found";

            return View(model);
        }

        public ActionResult Details(int lotId = 0)
        {
            var lotModel = lotService.GetLotById(lotId)?.ToMvcLot();
            if (lotModel == null)
                return RedirectToAction("NotFound", "Error");

            var bidsForLot = bidService.GetAllBidsForLot(lotId)
                .OrderByDescending(b => b.DateOfBid)
                .Select(b => b.ToMvcBid(userService, lotService));

            ViewBag.Bids = bidsForLot;
            ViewBag.CurrentUserId = userService.GetUserByLogin(User.Identity.Name)?.Id;
            if (bidsForLot.Count() == 0)
                ViewBag.NoBids = "There are no bids yet";

            return View(lotModel);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeBid(int? lotId , decimal bidValue = 0)
        {            
            if (lotId == null || lotId < 1)
                return RedirectToAction("Index", "Auction");

            var lotModel = lotService.GetLotById(lotId.Value);

            if (lotModel.Price > bidValue || bidValue > 10000000)
            {
                ModelState.AddModelError("bidValue", "Wrong range of value");
                return RedirectToAction("Details", "Auction", new { lotId = lotId.Value});
            }

            if (ModelState.IsValid)
            {
                bidService.CreateBid(new BidEntity()
                {
                    Price = bidValue,
                    DateOfBid = DateTime.Now,
                    UserId = userService.GetUserByLogin(User.Identity.Name).Id,
                    LotId = lotId.Value
                });
                lotModel.Price = bidValue;
                lotService.UpdateLot(lotModel);
                
                return RedirectToAction("Details", new { lotId = lotId.Value });
            }

            return RedirectToAction("Details", new { lotId = lotId.Value });
        }
    }
}