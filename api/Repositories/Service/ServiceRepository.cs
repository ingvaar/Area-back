using System.Collections.Generic;
using System.Linq;
using area.Contexts;
using area.Models.Service;

namespace area.Repositories.Service
{
	public class ServiceRepository : IServiceRepository
	{
		private readonly AreaContext _context;

		public ServiceRepository(AreaContext context)
		{
			_context = context;
		}

		public ServiceModel GetServiceById(int id)
		{
			return id < 0 ? null : _context.Service.SingleOrDefault(a => a.Id == id);
		}

		public ServiceModel[] GetServices(int offset, int limit)
		{
			return _context.Service.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public ServiceModel[] SearchServiceByName(string name, int offset, int limit)
		{
			return _context.Service.OrderBy(p => p.Id)
				.Where(p => p.Name.Contains(name))
				.Skip(offset)
				.Take(limit)
				.ToArray();
		}

		public IEnumerable<ServiceModel> GetAllServices()
		{
			return _context.Service.OrderBy(p => p.Id)
				.ToArray();
		}
	}
}

