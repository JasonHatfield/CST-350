using LoginForm.Models;

namespace LoginForm.Services
{
    public class SecurityService
    {
        //List<UserModel> knownUsers = new List<UserModel>();
        SecurityDAO securityDAO = new SecurityDAO();

        /*public SecurityService() { 
            knownUsers.Add(new UserModel { Id = 0, UserName = "username", Password = "Password" });
        }*/
        public bool IsValid(UserModel user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
            //return knownUsers.Any(x => x.UserName == user.UserName && x.Password == user.Password);
        }
    }
}
