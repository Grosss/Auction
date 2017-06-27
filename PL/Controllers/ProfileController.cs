using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using PL.Infrastructure.Mappers;
using PL.Models;
using PL.Providers;

namespace PL.Controllers
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

        public ActionResult MyProfile()
        {
            var userModel = userService.GetUserByLogin(User.Identity.Name).ToMvcEditUser(roleService);
            return View(userModel);
        }

        public ActionResult MyPurchases(int? page)
        {
            if (page == null || page < 1)
                page = 1;

            int userId = userService.GetUserByLogin(User.Identity.Name).Id;
            var expiredLots = lotService.GetAllLots()
                .Where(l => l.ExpirationTime <= DateTime.Now && l.UserId != userId)
                .Where(l => bidService.GetLastBidForLot(l.Id) != null
                && bidService.GetLastBidForLot(l.Id).UserId == userId);

            int totalPages = expiredLots.Count();

            var purchasedLots = expiredLots
                .OrderBy(l => l.Name)
                .Select(l => l.ToMvcLot())
                .Skip((page.Value - 1) * pageSize).Take(pageSize);
            var model = new ResultListViewModel<LotViewModel>(purchasedLots, (int)Math.Ceiling(totalPages / (double)pageSize), page.Value);

            if (purchasedLots.Count() == 0)
                ViewBag.NoPurchases = "There are no purchases";

            return View(model);
        }

        public ActionResult MyBids(int? page)
        {
            if (page == null || page < 1)
                page = 1;

            int userId = userService.GetUserByLogin(User.Identity.Name).Id;
            int totalPages = bidService.GetAllBidsForUser(userId).Count();
            int pageSize = 10;

            var bids = bidService.GetAllBidsForUser(userId)
                .OrderByDescending(b => b.DateOfBid)
                .Select(b => b.ToMvcBid(userService, lotService))
                .Skip((page.Value - 1) * pageSize).Take(pageSize);

            var model = new ResultListViewModel<BidViewModel>(bids, (int)Math.Ceiling(totalPages / (double)pageSize), page.Value);
            ViewBag.Roles = roleService.GetUserRoles(userId).Select(r => r.Name);

            if (bids.Count() == 0)
                ViewBag.NoBids = "There are no bids";

            return View(model);
        }

        public ActionResult MyLots(int? page)
        {
            if (page == null || page < 1)
                page = 1;

            int userId = userService.GetUserByLogin(User.Identity.Name).Id;
            int totalPages = lotService.GetAllLotsForUser(userId).Count();

            var lots = lotService.GetAllLotsForUser(userId)
                .OrderBy(l => l.ExpirationTime)
                .Select(l => l.ToMvcLot())
                .Skip((page.Value - 1) * pageSize).Take(pageSize);
            var model = new ResultListViewModel<LotViewModel>(lots, (int)Math.Ceiling(totalPages / (double)pageSize), page.Value);

            if (lots.Count() == 0)
                ViewBag.NoLots = "There are no lots";

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetAllUsers(int? page)
        {
            if (page == null || page < 1)
                page = 1;

            int userId = userService.GetUserByLogin(User.Identity.Name).Id;
            int totalPages = userService.GetAllUsers().Count();
            int pageSize = 7;

            var users = userService.GetAllUsers()
                .OrderBy(u => u.Login)
                .Select(u => u.ToMvcEditUser(roleService))
                .Skip((page.Value - 1) * pageSize).Take(pageSize);

            var model = new ResultListViewModel<EditUserViewModel>(users, (int)Math.Ceiling(totalPages / (double)pageSize), page.Value);

            if (users.Count() == 0)
                ViewBag.NoUsers = "There are no users";

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteUser(int userId = 0)
        {
            var userModel = userService.GetUserById(userId)?.ToMvcEditUser(roleService);
            if (userModel == null)
                return RedirectToAction("NotFound", "Error");

            if (userModel.Id != userService.GetUserByLogin(User.Identity.Name).Id && !User.IsInRole("admin"))
                return RedirectToAction("Forbidden", "Error");

            if (userModel.Id == userService.GetUserByLogin(User.Identity.Name).Id)
                ViewBag.Message = "Do you really want to delete your profile?";
            else
                ViewBag.Message = $"Are really you sure you want to delete user {userService.GetUserById(userId).Login}?";

            return View(userModel);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int userId = 0)
        {
            bool isHimself = (userId == userService.GetUserByLogin(User.Identity.Name).Id);

            var expiredLots = lotService.GetAllLots()
                .Where(l => l.ExpirationTime <= DateTime.Now && l.UserId != userId);
            foreach (var lot in expiredLots)
            {
                var lastBid = bidService.GetLastBidForLot(lot.Id);
                if (lastBid != null && lastBid.UserId == userId)
                {
                    lotService.DeleteLot(lot);
                }
            }

            var userLots = lotService.GetAllLotsForUser(userId);
            foreach (var lot in userLots)
                lotService.DeleteLot(lot);

            var notExpiredLots = lotService.GetAllLots().Where(l => l.ExpirationTime > DateTime.Now);
            foreach (var lot in notExpiredLots)
            {
                var lastBid = bidService.GetLastBidForLot(lot.Id);
                if (lastBid != null && lot.Price > lastBid.Price)
                {
                    lot.Price = lastBid.Price;
                    lotService.UpdateLot(lot);
                }
            }

            var userModel = userService.GetUserById(userId);
            userService.DeleteUser(userModel);

            if(isHimself)
                return RedirectToAction("LogOff", "Account");
            else
                return RedirectToAction("GetAllUsers", "Profile");
        }

        [HttpGet]
        public ActionResult CreateLot()
        {
            var categories = categoryService.GetAllCategories()
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            ViewBag.Categories = categories;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLot(LotViewModel lotModel, HttpPostedFileBase uploadImage)
        {
            var categories = categoryService.GetAllCategories()
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            ViewBag.Categories = categories;

            DateTime minimumDate = DateTime.Now;
            if (minimumDate > lotModel.ExpirationTime)
            {
                ModelState.AddModelError("", "Enter the expiration time in future");
                return View(lotModel);
            }

            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    lotModel.Image = imageData;
                }
                lotModel.UserId = userService.GetUserByLogin(User.Identity.Name).Id;
                lotService.CreateLot(lotModel.ToBllLot());

                return RedirectToAction("MyLots", "Profile");
            }
            return View(lotModel);
        }

        [HttpGet]
        public ActionResult EditLot(int lotId = 0)
        {
            var lotModel = lotService.GetLotById(lotId)?.ToMvcLot();
            if (lotModel == null)
                return RedirectToAction("NotFound", "Error");

            if (lotModel.UserId != userService.GetUserByLogin(User.Identity.Name).Id)
                return RedirectToAction("Forbidden", "Error");
            
            if (DateTime.Now > lotModel.ExpirationTime)
                return RedirectToAction("BadRequest", "Error");

            var categories = categoryService.GetAllCategories()
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            ViewBag.Categories = categories;

            return View(lotModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLot(LotViewModel lotModel, HttpPostedFileBase uploadImage)
        {
            var categories = categoryService.GetAllCategories()
                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            ViewBag.Categories = categories;

            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    lotModel.Image = imageData;
                }
                lotService.UpdateLot(lotModel.ToBllLot());

                return RedirectToAction("Index", "Auction");
            }
            return View(lotModel);
        }

        [HttpGet]
        public ActionResult DeleteLot(int lotId = 0)
        {
            var lotModel = lotService.GetLotById(lotId)?.ToMvcLot();
            if (lotModel == null)
                return RedirectToAction("NotFound", "Error");

            if (lotModel.UserId != userService.GetUserByLogin(User.Identity.Name).Id && !User.IsInRole("admin"))
                return RedirectToAction("Forbidden", "Error");
            
            return View(lotModel);
        }

        [HttpPost, ActionName("DeleteLot")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLotConfirmed(int lotId = 0)
        {
            var lotModel = lotService.GetLotById(lotId);
            lotService.DeleteLot(lotModel);
            return RedirectToAction("MyLots", "Profile");
        }
    }
}