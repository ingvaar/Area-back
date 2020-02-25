using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace area.Repositories.Worker
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly HttpClient _client;

        public WorkerRepository()
        {
            _client = new HttpClient();
        }
        
        public async Task<string> Get(string route)
        {
            return await _client.GetStringAsync(route);
        }

        public async Task<string> Post(string route, Dictionary<string, string> form)
        {
            var content = new FormUrlEncodedContent(form);

            var response = await _client.PostAsync(route, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}