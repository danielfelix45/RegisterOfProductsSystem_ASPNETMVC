using Microsoft.AspNetCore.Mvc;
using RegisterOfProducts.Helper;
using RegisterOfProducts.Models;
using RegisterOfProducts.Repository;

namespace RegisterOfProducts.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;
        public LoginController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository; 
            _userSession = userSession;
        }
        public IActionResult Index()
        {
            // If user is logged, redirect to Home page
            if (_userSession.GetUserSession() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult ToEnter(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UserModel user = _userRepository.GetByLogin(loginModel.Login);
                    if (user != null)
                    {
                        if(user.ValidPassword(loginModel.Password))
                        {
                            _userSession.CreateUserSession(user);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["ErrorMessagwe"] = "User password is invalid";
                    }
                    TempData["ErrorMessage"] = "User and/or password is invalid!";
                }
                return View("Index");
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"Unable to login, please try again. {e.Message}";
                return RedirectToAction("Index");
            }
     
        }

        public IActionResult Logout()
        {
            _userSession.RemoveUserSession();
            return RedirectToAction("Index", "Login");
        }
    }
}
