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
    public class HomeController : Controller
    {
        private readonly ILotService lotService;
        private readonly ICategoryService categoryService;
        private readonly int pageSize = 3;

        public HomeController(ILotService lotService, ICategoryService categoryService)
        {
            this.lotService = lotService;
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var lots = lotService.GetAllLots().Select(l => l.ToMvcLot());
            return View(lots);
        }
    }
}