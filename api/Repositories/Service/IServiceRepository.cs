using System.Collections.Generic;
using area.Models.Service;

namespace area.Repositories.Service
{
	public interface IServiceRepository
	{
		ServiceModel[] GetServices(int offset, int limit);
		ServiceModel GetServiceById(int id);
		ServiceModel[] SearchServiceByName(string name, int offset, int limit);
		IEnumerable<ServiceModel> GetAllServices();
	}
}
