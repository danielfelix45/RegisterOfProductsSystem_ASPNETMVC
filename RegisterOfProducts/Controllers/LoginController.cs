using Microsoft.AspNetCore.Mvc;
using RegisterOfProducts.Models;
using RegisterOfProducts.Repository;

namespace RegisterOfProducts.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }
        public IActionResult Index()
        {
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
    }
}
