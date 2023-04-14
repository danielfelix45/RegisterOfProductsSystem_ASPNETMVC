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
        private readonly IEmail _email;
        public LoginController(IUserRepository userRepository, IUserSession userSession, IEmail email)
        {
            _userRepository = userRepository; 
            _userSession = userSession;
            _email = email;
        }
        public IActionResult Index()
        {
            // If user is logged, redirect to Home page
            if (_userSession.GetUserSession() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinePassword()
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
        public IActionResult SendLinkToRedefinePassword(RedefinePasswordModel redefinePasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepository.GetByEmailAndLogin(redefinePasswordModel.Email, redefinePasswordModel.Login);
                    if (user != null)
                    {
                        string newPassword = user.GenerateNewPassword();
                        string message = $"Your new password is: {newPassword}";

                        bool emailSent = _email.Sent(user.Email, "Register of Products - New Password", message);
                        if (emailSent)
                        {
                            _userRepository.ToUpdate(user);
                            TempData["SuccessMessage"] = "We have sent a new password to your registered email.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"It was not possible to send the email. Please try again.";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["ErrorMessage"] = "We were unable to redefine your password, please verify the information you entered.";
                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Oops, Couldn't redefine your password, try again, error detail: {e.Message}";
                return RedirectToAction("Index");
            }
           
        }


    }
}
