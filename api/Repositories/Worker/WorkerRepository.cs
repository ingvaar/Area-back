using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;


namespace area.Repositories.Worker
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly HttpClient _client;

        public WorkerRepository()
        {
            _client = new HttpClient();
        }
        
        public string Get(string route, string token)
        {
            try {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
                var response = _client.GetStringAsync(route);

                response.Wait();
                return response.Result;
            } catch (InvalidOperationException) {
                return "{\"error\": \"Invalid conf or URL\"}";
            }
        }

        public string Post(string route, Dictionary<string, string> form, string token)
        {
            try {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
                var content = new FormUrlEncodedContent(form);

                var response = _client.PostAsync(route, content);

                response.Wait();

                var result = response.Result.Content.ReadAsStringAsync();
                result.Wait();

                return result.Result;
            } catch (InvalidOperationException) {
                return "{\"error\": \"Invalid conf or URL\"}";
            }
            
        }
    }
}