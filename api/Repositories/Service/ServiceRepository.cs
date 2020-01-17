using System;
using System.Linq;

using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Models;

namespace area.Repositories
{
	public class ServiceRepository : IServiceRepository
	{
		private AreaContext _context;
		private readonly AppSettings _appSettings;

		public ServiceRepository(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public AccountModel AddNewService(AccountCreationModel newAcc, int userId)
		{
			if (userId != newAcc.UserId)
				return null;
			var acc = new AccountModel();

			acc.Token = newAcc.Token;
			acc.ProvId = newAcc.ProvId;
			acc.UserId = newAcc.UserId;
			try {
				_context.Account.Add(acc);
				_context.SaveChanges();
				return _context.Account.SingleOrDefault(p => p.ProvId == newAcc.ProvId && p.UserId == p.UserId);
			} catch(Exception) {
				return null;
			}
		}

		public int DeleteServiceById(int id, int userId)
		{
			if (id < 0)
				return 0;

			var acc = _context.Account.SingleOrDefault(a => a.Id == id);

			if (acc == null || acc.UserId != userId)
				return 0;
			_context.Account.Remove(acc);
			return _context.SaveChanges();
		}

		public AccountModel GetServiceById(int id)
		{
			if (id < 0)
				return null;
			return _context.Account.SingleOrDefault(a => a.Id == id);
		}

		public AccountModel[] GetServices(int offset, int limit, int userId)
		{
			return _context.Account.OrderBy(p => p.Id)
				.Where(p => p.UserId == userId)
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public int UpdateServiceById(int id, AccountUpdateModel update, int userId)
		{
			int updateSuccess = 0;
			var target = _context.Account.SingleOrDefault(a => a.Id == id);

			if (target == null || target.UserId != userId)
				return 0;
			else if (update == null)
				return 0;

			if (update.Token == null)
				update.Token = target.Token;
			if (update.ProvId == null)
				update.ProvId = target.ProvId;

			_context.Entry(target).CurrentValues.SetValues(update);
			updateSuccess =_context.SaveChanges();
			return updateSuccess;
		}
	}
}

