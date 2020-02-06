using area.Models.Provider;

namespace area.Repositories.Provider
{
	public interface IProviderRepository
	{
		ProviderModel GetProviderById(int id);
		ProviderModel[] GetProviders(int offset, int limit);
		ProviderModel[] GetProviderByName(string name, int offset, int limit);
	}
}
