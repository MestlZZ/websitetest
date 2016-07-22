using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.Web.ViewModels;
using System.Web.Security;

namespace EasyPlan.Web.Controllers
{
    public class AccountController : DefaultController
    {
        private readonly IMembershipProvider _membershipProvider;
        private readonly IRoleProvider _roleProvider;
        private readonly IBoardRepository _boardRepository;

        public AccountController(IMembershipProvider membershipProvider, IRoleProvider roleProvider, IBoardRepository boardRepository)
        {
            _membershipProvider = membershipProvider;
            _roleProvider = roleProvider;
            _boardRepository = boardRepository;
        }

        [HttpPost]
        [Authorize]
        public ActionResult GetUserData()
        {
            var user = _membershipProvider.FindUserByEmail(HttpContext.User.Identity.Name);

            return JsonSuccess(UserMapper.Map(user));
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_membershipProvider.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _membershipProvider.CreateUser(model.Email, model.Password);

                if (user != null)
                {         
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Registration error");
                }
            }

            return View(model);
        }
    }
}