using System.Collections.Generic;

namespace area.Repositories.Worker
{
    public interface IWorkerRepository
    {
        public string Get(string route, string token);
        public string Post(string route, Dictionary<string, string> form, string token);
    }
}