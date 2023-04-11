using Newtonsoft.Json;
using RegisterOfProducts.Models;

namespace RegisterOfProducts.Helper
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CreateUserSession(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("loggedUserSession", value);
        }

        public UserModel GetUserSession()
        {
            string userSession = _httpContext.HttpContext.Session.GetString("loggedUserSession");
            if (string.IsNullOrEmpty(userSession)) return null;
            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }

        public void RemoveUserSession()
        {
            _httpContext.HttpContext.Session.Remove("loggedUserSession");
        }
    }
}
