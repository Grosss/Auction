using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Drawing.Imaging;
using MvcPL.Models;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using MvcPL.Providers;
using MvcPL.Infrastructure;
using MvcPL.Infrastructure.Mappers;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ILotService lotService;

        public AccountController(IUserService userService, IRoleService roleService, ILotService lotService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.lotService = lotService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginModel.Email, loginModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginModel.Email, loginModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or login");
                }
            }
            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if (registerModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
                {
                    ModelState.AddModelError("Captcha", "Incorrect input.");
                    return View(registerModel);
                }

                var anyUser = userService.GetUserByLogin(registerModel.Login);

                if (anyUser != null)
                {
                    ModelState.AddModelError("", "User with this login has already registered.");
                    return View(registerModel);
                }

                if (ModelState.IsValid)
                {
                    var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                        .CreateUser(registerModel.Email, registerModel.Login, registerModel.Password);

                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(registerModel.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error registration.");
                    }
                }
                return View(registerModel);
            }

            return View(registerModel);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }
    }
}