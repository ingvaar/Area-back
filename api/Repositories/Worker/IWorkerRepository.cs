using System.Collections.Generic;
using System.Threading.Tasks;

namespace area.Repositories.Worker
{
    public interface IWorkerRepository
    {
        public Task<string> Get(string route);
        public Task<string> Post(string route, Dictionary<string, string> form);
    }
}