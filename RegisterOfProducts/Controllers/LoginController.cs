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

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _userSession.RemoveUserSession();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
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
                        TempData["ErrorMessage"] = "User password is invalid";
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

        [HttpPost]
        public IActionResult SendLinkToResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepository.GetByEmailAndLogin(resetPasswordModel.Email, resetPasswordModel.Login);
                    if (user != null)
                    {
                        string newPassword = user.GenerateNewPassword();
                        _userRepository.Update(user);
                        TempData["SuccessMessage"] = "We have sent a new password to your registered email.";
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["ErrorMessage"] = "We were unable to reset your password, please verify the information you entered.";
                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Oops, Couldn't reset your password, try again, error detail: {e.Message}";
                return RedirectToAction("Index");
            }
           
        }


    }
}
