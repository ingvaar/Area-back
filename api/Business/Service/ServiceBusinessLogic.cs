using area.Contexts;
using area.Helpers;
using area.Models.Service;
using area.Repositories.Service;

namespace area.Business.Service
{
    public class ServiceBusinessLogic : IServiceBusinessLogic
    {
		private readonly IServiceRepository _repository;

		public ServiceBusinessLogic(AreaContext context)
		{
			_repository = new ServiceRepository(context);
        }

        public ServiceModel[] GetServices(int offset, int limit)
        {
            (offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

            return _repository.GetServices(offset, limit);
        }

        public ServiceModel GetServiceById(uint id)
        {
            return _repository.GetServiceById(id);
        }

        public ServiceModel[] SearchServiceByName(string name, int offset, int limit)
        {
            (offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

            return _repository.SearchServiceByName(name, offset, limit);
        }
    }
}