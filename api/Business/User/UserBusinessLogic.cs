using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using area.Configuration;
using area.Contexts;
using area.Entities;
using area.Helpers;
using area.Models.User;
using area.Repositories.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace area.Business.User
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
		private readonly IUserRepository _repository;
		private readonly AppSettings _appSettings;

		public UserBusinessLogic(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
			_repository = new UserRepository(context);
        }
        
        public UserPublicModel AddNewUser(UserCreationModel newUser)
        {
	        var user = new UserModel {Email = newUser.Email, Password = newUser.Password, Username = newUser.Username};

	        var (byName, byEmail) = (_repository.GetUserByUsername(newUser.Username), _repository.GetUserByEmail(
		        newUser.Email));
	        if (byName != null || byEmail != null)
		        return null;

	        return _repository.AddNewUser(user) == 1 ? _repository.GetUserByUsername(user.Username) : null;
        }

        public int DeleteUserById(int id, uint userId)
        {
			if (id < 0)
				return 0;

			var user = _repository.GetUserById(id);

			if (user == null)
				return 0;
			return user.Id != userId ? 2 : _repository.DeleteUser(user);
        }

        public UserPublicModel GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
			var publicUser = new UserPublicModel();

			if (user == null) {
				return null;
			}
			publicUser.Id = user.Id;
			publicUser.Username = user.Username;
			publicUser.Email = user.Email;
			publicUser.Date = user.Date;
			return publicUser;
        }

        public UserPublicModel[] GetUsers(int offset, int limit)
        {
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

            return _repository.GetUsers(offset, limit);
        }

        public int UpdateUserById(int id, UserUpdateModel updatedUser, uint userId)
        {
			var target = _repository.GetUserById(id);

			if (target == null)
				return 2;
			if (updatedUser == null)
				return 0;

			if (updatedUser.Username == null)
				updatedUser.Username = target.Username;
			if (updatedUser.Password == null)
				updatedUser.Password = target.Password;
			if (updatedUser.Email == null) {
				updatedUser.Email = target.Email;
			}

			return _repository.UpdateUser(updatedUser, target);
        }

        public UserEntity Authenticate(UserAuthModel authUser)
        {
	        var user = _repository.GetUserByCredentials(authUser.Username, authUser.Password);
				
			if (user == null)
				return null;

			var userEntity = new UserEntity();

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim("Username", user.Username),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim("DateOfJoin", user.Date.ToString("yyyy-MM-dd hh:mm:ss")),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			userEntity.Token = tokenHandler.WriteToken(token);
			userEntity.Username = user.Username;
			userEntity.Id = user.Id;
			userEntity.Email= user.Email;

			return userEntity;
        }

        public UserPublicModel GetCurrentUser(ClaimsPrincipal user)
        {
	        var currentUser = new UserPublicModel
	        {
		        Id = uint.Parse(user.FindFirst(ClaimTypes.Name)?.Value),
		        Username = user.FindFirst("Username")?.Value,
		        Email = user.FindFirst(ClaimTypes.Email)?.Value,
		        Date = DateTimeOffset.Parse(user.FindFirst("DateOfJoin")?.Value)
	        };


	        return currentUser;
        }

        public UserPublicModel GetUserByUsername(string username)
        {
	        return _repository.GetUserByUsername(username);
        }

        public UserPublicModel[] SearchUserByEmail(string email, int offset, int limit)
        {
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return _repository.SearchUserByEmail(email, offset, limit);
        }

        public UserPublicModel[] SearchUserByUsername(string username, int offset, int limit)
        {
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);
			
			return _repository.SearchUserByUsername(username, offset, limit);
        }

        public UserPublicModel GetUserByEmail(string email)
        {
	        return _repository.GetUserByEmail(email);
        }
    }
}
