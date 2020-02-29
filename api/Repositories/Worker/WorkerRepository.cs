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
        
        public string Get(string route)
        { 
            var response = _client.GetStringAsync(route);

            response.Wait();
            return response.Result;
        }

        public string Post(string route, Dictionary<string, string> form)
        {
            var content = new FormUrlEncodedContent(form);

            var response = _client.PostAsync(route, content);
            
            response.Wait();
            
            var result = response.Result.Content.ReadAsStringAsync();
            result.Wait();
            
            return result.Result;
        }
    }
}