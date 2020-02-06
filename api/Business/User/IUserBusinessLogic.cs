using System.Security.Claims;
using area.Entities;
using area.Models;

namespace area.Business.User
{
    public interface IUserBusinessLogic
    {
		UserPublicModel AddNewUser(UserCreationModel user);
		int DeleteUserById(int id, int userId);
		UserPublicModel GetUserById(int id);
		UserPublicModel[] GetUsers(int offset, int limit);
		int UpdateUserById(int id, UserUpdateModel user, int userId);
		UserEntity Authenticate(UserAuthModel user);
		UserPublicModel GetCurrentUser(ClaimsPrincipal user);
		UserPublicModel GetUserByUsername(string username);
		UserPublicModel[] SearchUserByEmail(string email, int offset, int limit);
		UserPublicModel[] SearchUserByUsername(string username, int offset, int limit);
    }
}