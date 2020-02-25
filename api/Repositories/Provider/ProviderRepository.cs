using System.Linq;
using area.Contexts;
using area.Models.Provider;

namespace area.Repositories.Provider
{
	public class ProviderRepository : IProviderRepository
	{
		private readonly AreaContext _context;

		public ProviderRepository(AreaContext context)
		{
			_context = context;
		}


		public ProviderModel GetProviderById(uint id)
		{
			return _context.Provider.SingleOrDefault(a => a.Id == id);
		}

		public ProviderModel[] GetProviders(int offset, int limit)
		{
			return _context.Provider.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.ToArray();
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

