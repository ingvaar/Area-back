using area.Models.User;

namespace area.Business.Worker
{
    public interface IWorkerBusinessLogic
    {
        string GetData(int confId, UserPublicModel currentUser);
    }
}