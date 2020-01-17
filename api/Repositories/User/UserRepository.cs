using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Models;
using area.Entities;

namespace area.Repositories
{
	public class UserRepository : IUserRepository
	{
		private AreaContext _context;
		private readonly AppSettings _appSettings;

		public UserRepository(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public UserPublicModel[] GetUsers(int offset, int limit)
		{
			return _context.User.OrderBy(p => p.Id)
				.Select(p => new UserPublicModel {
						Id = p.Id,
						Username = p.Username,
						Email = p.Email,
						Date = p.Date
					})
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public UserPublicModel GetUserById(int id)
		{
			var user = _context.User.SingleOrDefault(a => a.Id == id);
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

		public int UpdateUserById(int id, UserUpdateModel updatedUser, int userId)
		{
			int updateSuccess = 0;
			var target = _context.User.SingleOrDefault(a => a.Id == id);

			if (target == null)
				return 2;
			else if (updatedUser == null)
				return 0;

			if (updatedUser.Username == null)
				updatedUser.Username = target.Username;
			if (updatedUser.Password == null)
				updatedUser.Password = target.Password;
			if (updatedUser.Email == null)
				updatedUser.Email = target.Email;

			if (target.Id == userId) {
				_context.Entry(target).CurrentValues.SetValues(updatedUser);
				updateSuccess =_context.SaveChanges();
			}
			return updateSuccess;
		}

		public UserPublicModel AddNewUser(UserCreationModel newUser)
		{
			var user = new UserModel();

			user.Email = newUser.Email;
			user.Password = newUser.Password;
			user.Username = newUser.Username;
			try {
				_context.User.Add(user);
				_context.SaveChanges();
				return _context.User
					.Select(p => new UserPublicModel {
						Id = p.Id,
						Username = p.Username,
						Email = p.Email,
						Date = p.Date
					})
					.SingleOrDefault(p => p.Username == newUser.Username);
			} catch(Exception) {
				return null;
			}
		}

		public int DeleteUserById(int id, int userId)
		{
			if (id < 0)
				return 0;

			var user = _context.User.SingleOrDefault(a => a.Id == id);

			if (user == null)
				return 0;
			if (user != null && user.Id != userId)
				return 2;
			_context.User.Remove(user);
			return _context.SaveChanges();
		}

		public UserEntity Authenticate(UserAuthModel authUser)
		{
			var user = _context.User
				.SingleOrDefault(x => x.Username == authUser.Username
								&& x.Password == authUser.Password);

			if (user == null)
				return null;

			var userEntity = new UserEntity();

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
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
			var currentUser = new UserPublicModel();

			currentUser.Id = int.Parse(user.FindFirst(ClaimTypes.Name)?.Value);
			currentUser.Username = user.FindFirst("Username")?.Value;
			currentUser.Email = user.FindFirst(ClaimTypes.Email)?.Value;
			currentUser.Date = DateTimeOffset.Parse(user.FindFirst("DateOfJoin")?.Value);

			return currentUser;
		}

		public UserPublicModel[] GetUserByUsername(string username, int offset, int limit)
		{
			return _context.User
				.Select(p => new UserPublicModel {
						Id = p.Id,
						Username = p.Username,
						Email = p.Email,
						Date = p.Date
					})
				.Where(i => i.Username.Contains(username))
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public UserPublicModel[] GetUserByEmail(string email, int offset, int limit)
		{
			return _context.User
				.Select(p => new UserPublicModel {
						Id = p.Id,
						Username = p.Username,
						Email = p.Email,
						Date = p.Date
					})
				.Where(i => i.Email.Contains(email))
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}
	}
}

