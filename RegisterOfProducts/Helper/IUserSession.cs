using RegisterOfProducts.Models;

namespace RegisterOfProducts.Helper
{
    public interface IUserSession
    {
        public void CreateUserSession(UserModel user);
        public void RemoveUserSession();
        public UserModel GetUserSession();
    }
}
