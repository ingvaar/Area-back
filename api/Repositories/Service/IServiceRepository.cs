using area.Models;

namespace area.Repositories
{
	public interface IServiceRepository
	{
		AccountModel AddNewService(AccountCreationModel newProv, int userId);
		int DeleteServiceById(int id, int userId);
		int UpdateServiceById(int id, AccountUpdateModel prov, int userId);
		AccountModel[] GetServices(int offset, int limit, int userId);
		AccountModel GetServiceById(int id);
	}
}
