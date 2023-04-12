using Microsoft.AspNetCore.Mvc;
using RegisterOfProducts.Filters;
using RegisterOfProducts.Models;
using RegisterOfProducts.Repository;

namespace RegisterOfProducts.Controllers
{
    [AdminRestrictedPage]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _userRepository.GetAll();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            UserModel user = _userRepository.GetById(id);
            return View(user);
        }

        public IActionResult ConfirmDelete(int id)
        {
            UserModel user = _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.ToAdd(user);
                    TempData["SuccessMessage"] = "User successfully registered";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"We were unable to register the user, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Edit(UserWithoutPasswordModel userWithoutPasswordModel)
        {
            try
            {
                UserModel user = null;
                if (ModelState.IsValid)
                {

                    user = new UserModel()
                    {
                        Id = userWithoutPasswordModel.Id,
                        Name = userWithoutPasswordModel.Name,
                        Email = userWithoutPasswordModel.Email,
                        Login = userWithoutPasswordModel.Login,
                        Profile = userWithoutPasswordModel.Profile
                    };
                    user = _userRepository.Update(user);
                    TempData["SuccessMessage"] = "User successfully altered";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"We were unable to altered the user, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _userRepository.Delete(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Successfully deleted user";
                }
                else
                {
                    TempData["ErrorMessage"] = "User not deleted";
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"We were unable to delete the user, please try again, error detail: {e.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
