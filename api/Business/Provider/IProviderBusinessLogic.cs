using area.Models.Provider;

namespace area.Business.Provider
{
    public interface IProviderBusinessLogic
    {
        ProviderModel[] GetProviders(int offset, int limit);
        ProviderModel GetProviderById(int id);
        ProviderModel[] SearchProviderByName(string name, int offset, int limit);
    }
}