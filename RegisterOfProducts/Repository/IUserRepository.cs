using RegisterOfProducts.Models;

namespace RegisterOfProducts.Repository
{
    public interface IUserRepository
    {
        List<UserModel> GetAll();
        UserModel GetByEmailAndLogin(string email, string login);
        UserModel GetByLogin(string login);
        UserModel GetById(int id);
        UserModel ToAdd(UserModel user);
        UserModel Update(UserModel user);
        bool Delete(int id);
    }
}