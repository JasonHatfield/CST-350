using CST_350_Milestone.Models;

namespace CST_350_Milestone.Services;

public class SecurityService
{
	//List<UserModel> knownUsers = new List<UserModel>();
	private readonly SecurityDAO securityDAO = new();

	/*public SecurityService() { 
	    knownUsers.Add(new UserModel { Id = 0, UserName = "username", Password = "Password" });
	}*/
	public bool IsValid(UserModel user)
	{
		return securityDAO.FindUserByNameAndPassword(user);
		//return knownUsers.Any(x => x.UserName == user.UserName && x.Password == user.Password);
	}

	public int GetUserIdByUsername(string username)
	{
		return securityDAO.GetUserIdByUsername(username);
	}
}