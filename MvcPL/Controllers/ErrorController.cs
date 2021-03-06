﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        public ViewResult Forbidden()
        {
            Response.StatusCode = 403;
            return View("BadRequest");
        }

        public ViewResult BadRequest()
        {
            Response.StatusCode = 400;
            return View("BadRequest");
        }
    }
}