using System.Web.Mvc;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.Web.Components.Providers;
using EasyPlan.Web.ViewModels;

namespace EasyPlan.Web.Controllers
{
    public class AccountController : DefaultController
    {
        private readonly IMembershipProvider _membershipProvider;
        private readonly IBoardRepository _boardRepository;
        private readonly IUserRepository _userRepository;

        public AccountController(IMembershipProvider membershipProvider, IUserRepository userRepository, IBoardRepository boardRepository)
        {
            _membershipProvider = membershipProvider;
            _boardRepository = boardRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Authorize]
        public ActionResult GetUserData()
        {
            var user = _userRepository.FindUserByEmail(User.Identity.Name);
            
            return JsonSuccess(UserMapper.Map(user));
        }
        
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.FindUserByEmail(model.Email);
                if (user != null)
                {
                    if (_membershipProvider.ValidateUser(user, model.Password))
                    {
                        _membershipProvider.Authorize(model.Email, model.RememberMe);

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
                        ModelState.AddModelError("", "Incorrect password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect email");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            _membershipProvider.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }
        
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.FindUserByEmail(model.Email) == null)
                {
                    _userRepository.Add(new User(model.FullName, model.Email, model.Password));

                    _membershipProvider.Authorize(model.Email, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Current email is used by another user.");
                }
            }

            return View(model);
        }
    }
}