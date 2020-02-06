using area.Models.Service;

namespace area.Business.Service
{
    public interface IServiceBusinessLogic
    {
        ServiceModel[] GetServices(int offset, int limit);
        ServiceModel GetServiceById(int id);
        ServiceModel[] SearchServiceByName(string name, int offset, int limit);
    }
}