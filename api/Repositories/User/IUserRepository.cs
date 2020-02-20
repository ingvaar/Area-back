using area.Models.User;

namespace area.Repositories.User
{
	public interface IUserRepository
	{
		int AddNewUser(UserModel user);
		int DeleteUser(UserModel user);
		UserModel GetUserById(int id);
		UserPublicModel[] GetUsers(int offset, int limit);
		int UpdateUser(UserUpdateModel updatedUser, UserModel target);
		UserPublicModel GetUserByUsername(string username);
		UserPublicModel GetUserByEmail(string email);
		UserModel GetUserByCredentials(string username, string password);
		UserPublicModel[] SearchUserByUsername(string username, int offset, int limit);
		UserPublicModel[] SearchUserByEmail(string email, int offset, int limit);
	}
}
