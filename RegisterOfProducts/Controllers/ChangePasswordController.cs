using Microsoft.AspNetCore.Mvc;
using RegisterOfProducts.Helper;
using RegisterOfProducts.Models;
using RegisterOfProducts.Repository;

namespace RegisterOfProducts.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;
        public ChangePasswordController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Change(ChangePasswordModel changePasswordModel)
        {
            try
            {

                UserModel loggedUser = _userSession.GetUserSession();
                changePasswordModel.Id = loggedUser.Id;
                if (ModelState.IsValid)
                {
                    _userRepository.ChangePassword(changePasswordModel);
                    TempData["SuccessMessage"] = "Password changed successfully.";
                    return View("Index", changePasswordModel);
                }
                return View("Index", changePasswordModel);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Unable to change your password, please try again, error detail: {e.Message}";
                return View("Index", changePasswordModel);
            }
        }
    }
}
