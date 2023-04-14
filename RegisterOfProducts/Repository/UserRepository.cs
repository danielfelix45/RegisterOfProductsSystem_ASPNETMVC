using RegisterOfProducts.Data;
using RegisterOfProducts.Models;

namespace RegisterOfProducts.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductDbContext _context;
        public UserRepository(ProductDbContext context)
        {
            _context = context;
        }

        public List<UserModel> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserModel GetByEmailAndLogin(string email, string login)
        {
            return _context.Users.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UserModel GetByLogin(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UserModel GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserModel ToAdd(UserModel user)
        {
            user.DateRegister = DateTime.Now;
            user.SetPasswordHash(); 
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserModel ToUpdate(UserModel user)
        {
            UserModel userDB = GetById(user.Id);

            if (userDB == null) throw new Exception("There was an error updating the user");

            userDB.Name = user.Name;
            userDB.Email = user.Email;
            userDB.Login = user.Login;
            userDB.Profile = user.Profile;
            userDB.DateUpdate = DateTime.Now;

            _context.Users.Update(userDB);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            UserModel userDB = GetById(id);

            if (userDB == null) throw new Exception("There was an error deleting the user");

            _context.Users.Remove(userDB);
            _context.SaveChanges();
            return true;
        }
        public UserModel ChangePassword(ChangePasswordModel changePasswordModel)
        {
            UserModel? userDB = GetById(changePasswordModel.Id);
            if (userDB == null) throw new Exception("There was an error updating the password, user not found!");
            if (!userDB.ValidPassword(changePasswordModel.CurrentPassword)) throw new Exception("Current password does not match!");
            if (userDB.ValidPassword(changePasswordModel.NewPassword)) throw new Exception("New password must be different from current password");

            userDB.SetNewPassword(changePasswordModel.NewPassword);
            userDB.DateUpdate = DateTime.Now;

            _context.Users.Update(userDB);
            _context.SaveChanges();
            return userDB;
        }
    }
}
