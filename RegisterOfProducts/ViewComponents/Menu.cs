using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RegisterOfProducts.Models;

namespace RegisterOfProducts.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("loggedUserSession");
             
            if (string.IsNullOrEmpty(userSession)) return null;
            
            UserModel user = JsonConvert.DeserializeObject<UserModel>(userSession);
            return View(user);
        }
    }
}
