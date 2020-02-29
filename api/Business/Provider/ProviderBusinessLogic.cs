using area.Contexts;
using area.Helpers;
using area.Models.Provider;
using area.Repositories.Provider;

namespace area.Business.Provider
{
    public class ProviderBusinessLogic : IProviderBusinessLogic
    {
		private readonly IProviderRepository _repository;

		public ProviderBusinessLogic(AreaContext context)
		{
			_repository = new ProviderRepository(context);
        }
		
        public ProviderModel[] GetProviders(int offset, int limit)
        {
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return _repository.GetProviders(offset, limit);
        }

        public ProviderModel GetProviderById(uint id)
        {
	        return _repository.GetProviderById(id);
        }

        public ProviderModel[] SearchProviderByName(string name, int offset, int limit)
        {
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return _repository.GetProviderByName(name, offset, limit);
        }
    }
}