using System.Linq;
using area.Contexts;
using area.Models;
using area.Models.User;

namespace area.Repositories.User
{
	public class UserRepository : IUserRepository
	{
		private readonly AreaContext _context;

		public UserRepository(AreaContext context)
		{
			_context = context;
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

		public UserModel GetUserById(int id)
		{
			return _context.User.SingleOrDefault(a => a.Id == id);
		}

		public int UpdateUser(UserUpdateModel updatedUser, UserModel target)
		{
			_context.Entry(target).CurrentValues.SetValues(updatedUser);
			return _context.SaveChanges();
		}

		public int AddNewUser(UserModel user)
		{
			_context.User.Add(user);
			return _context.SaveChanges();
		}

		public int DeleteUser(UserModel user)
		{
			_context.User.Remove(user);
			return _context.SaveChanges();
		}

		public UserModel GetUserByCredentials(string username, string password)
		{
			return _context.User
				.SingleOrDefault(x => x.Username == username
								&& x.Password == password);
		}

		public UserPublicModel[] SearchUserByUsername(string username, int offset, int limit)
		{
			return _context.User.OrderBy(p => p.Id)
				.Select(p => new UserPublicModel {
						Id = p.Id,
						Username = p.Username,
						Email = p.Email,
						Date = p.Date
					})
				.Where(p => p.Username.Contains(username))
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public UserPublicModel[] SearchUserByEmail(string email, int offset, int limit)
		{
			return _context.User.OrderBy(p => p.Id)
				.Select(p => new UserPublicModel {
						Id = p.Id,
						Username = p.Username,
						Email = p.Email,
						Date = p.Date
					})
				.Where(p => p.Email.Contains(email))
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public UserPublicModel GetUserByUsername(string username)
		{
			return _context.User
				.Select(p => new UserPublicModel
				{
					Id = p.Id,
					Username = p.Username,
					Email = p.Email,
					Date = p.Date
				})
				.FirstOrDefault(p => p.Username.Contains(username));
		}

		public UserPublicModel GetUserByEmail(string email)
		{
			return _context.User
				.Select(p => new UserPublicModel
				{
					Id = p.Id,
					Username = p.Username,
					Email = p.Email,
					Date = p.Date
				})
				.FirstOrDefault(i => i.Email.Contains(email));
		}
	}
}

