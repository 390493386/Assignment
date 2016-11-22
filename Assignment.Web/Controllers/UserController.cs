using Assignment.Core.Domain;
using Assignment.Core.Service;
using Assignment.Services;
using Assignment.Web.Identity;
using Assignment.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IUserService userService = new UserService();
            var user = userService.GetUserByAccount(model.Account);
            if (user == null)
            {
                ModelState.AddModelError("Account", "用户名不存在");
                return View(model);
            }
            if (!userService.ValidatePsd(user, model.Password))
            {
                ModelState.AddModelError("Password", "用户密码错误");
                return View(model);
            }

            var identity = user.CreateIdentity();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, identity);
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //判断account是否存在
                UserService userService = new UserService();
                if (userService.GetUserByAccount(model.Account) != null)
                {
                    ModelState.AddModelError("Account", "用户名已存在");
                    return View(model);
                }
                else
                {
                    User user = new User()
                    {
                        Account = model.Account,
                        UserName = model.Name,
                        Email = model.Email,
                        Password = model.Password
                    };
                    userService.CreateUser(user);
                    userService.AddUserToGroup(user, "user");
                    return RedirectToAction("Index", "Assignment");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Show(int? id)
        {
            return View();
        }
    }
}