using System.Security.Claims;

using area.Models;
using area.Entities;

namespace area.Repositories
{
	public interface IUserRepository
	{
		UserPublicModel AddNewUser(UserCreationModel user);
		int DeleteUserById(int id, int userId);
		UserPublicModel GetUserById(int id);
		UserPublicModel[] GetUsers(int offset, int limit);
		int UpdateUserById(int id, UserUpdateModel user, int userId);
		UserEntity Authenticate(UserAuthModel user);
		UserPublicModel GetCurrentUser(ClaimsPrincipal user);
		UserPublicModel[] GetUserByUsername(string username, int offset, int limit);
		UserPublicModel[] GetUserByEmail(string email, int offset, int limit);
	}
}
