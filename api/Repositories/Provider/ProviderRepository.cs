using System;
using System.Linq;

using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Models;

namespace area.Repositories
{
	public class ProviderRepository : IProviderRepository
	{
		private AreaContext _context;
		private readonly AppSettings _appSettings;

		public ProviderRepository(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public ProviderModel AddNewProvider(ProviderCreationModel newProv)
		{
			var prov = new ProviderModel();

			prov.Name = newProv.Name;
			prov.Url = newProv.Url;
			try {
				_context.Provider.Add(prov);
				_context.SaveChanges();
				return _context.Provider
					.Select(p => new ProviderModel {
						Id = p.Id,
						Name = p.Name,
						Url = p.Url
					})
					.SingleOrDefault(p => p.Name == newProv.Name);
			} catch(Exception) {
				return null;
			}
		}

		public int DeleteProviderById(int id)
		{
			if (id < 0)
				return 0;

			var prov = _context.Provider.SingleOrDefault(a => a.Id == id);

			if (prov == null)
				return 0;
			_context.Provider.Remove(prov);
			return _context.SaveChanges();
		}

		public ProviderModel GetProviderById(int id)
		{
			if (id < 0)
				return null;
			return _context.Provider.SingleOrDefault(a => a.Id == id);
		}

		public ProviderModel[] GetProviders(int offset, int limit)
		{
			return _context.Provider.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.ToArray();

		}

		public int UpdateProviderById(int id, ProviderUpdateModel prov)
		{
			int updateSuccess = 0;
			var target = _context.Provider.SingleOrDefault(a => a.Id == id);

			if (target == null)
				return 0;
			else if (prov == null)
				return 0;

			if (prov.Name == null)
				prov.Name = target.Name;
			if (prov.Url == null)
				prov.Url = target.Url;

			_context.Entry(target).CurrentValues.SetValues(prov);
			updateSuccess =_context.SaveChanges();
			return updateSuccess;
		}

		public ProviderModel[] GetProviderByName(string name, int offset, int limit)
		{
			return _context.Provider
				.Where(i => i.Name.Contains(name))
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}
	}
}

