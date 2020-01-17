using area.Models;

namespace area.Repositories
{
	public interface IProviderRepository
	{
		ProviderModel AddNewProvider(ProviderCreationModel newProv);
		int DeleteProviderById(int id);
		ProviderModel GetProviderById(int id);
		ProviderModel[] GetProviders(int offset, int limit);
		int UpdateProviderById(int id, ProviderUpdateModel prov);
		ProviderModel[] GetProviderByName(string name, int offset, int limit);
	}
}
