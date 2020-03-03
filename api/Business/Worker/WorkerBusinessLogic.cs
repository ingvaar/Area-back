using System.Collections.Generic;
using area.Contexts;
using area.Models.Provider;
using area.Models.User;
using area.Repositories.Provider;
using area.Repositories.Service;
using area.Repositories.Widget;
using area.Repositories.WidgetConf;
using area.Repositories.Worker;
using Newtonsoft.Json;

namespace area.Business.Worker
{
    public class WorkerBusinessLogic : IWorkerBusinessLogic
    {
        private readonly IWidgetConfRepository _widgetConfRepository;
        private readonly IWidgetRepository _widgetRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IWorkerRepository _workerRepository;
        
        public WorkerBusinessLogic(AreaContext context)
        {
            _widgetConfRepository = new WidgetConfRepository(context);
            _widgetRepository = new WidgetRepository(context);
            _serviceRepository = new ServiceRepository(context);
            _providerRepository = new ProviderRepository(context);
            _workerRepository = new WorkerRepository();
        }
        
        public string GetData(int confId, UserPublicModel currentUser)
        {
            var conf = _widgetConfRepository.GetWidgetConfById(confId);
            if (conf == null || conf.UserId != currentUser.Id) 
                return null;
            var widget = _widgetRepository.GetWidgetById(conf.WidgetId);
            if (widget == null)
                return null;
            var service = _serviceRepository.GetServiceById(widget.ServiceId);
            if (service == null)
                return null;
            var provider = _providerRepository.GetProviderById(service.ProvId);
            if (provider == null)
                return null;
            var (route, method, form, token) = ParsConf(conf.Conf, provider);
            if (route == null || method == null)
                return null;

            return method switch
            {
                "get" => _workerRepository.Get(route, token),
                "post" => _workerRepository.Post(route, form, token),
                _ => null
            };
        }

        private (string, string, Dictionary<string, string>, string) ParsConf(string conf, ProviderModel provider)
        {
            var jToken = JsonConvert.DeserializeObject<Dictionary<string, object>>(conf);

            if (!jToken.ContainsKey("data")) return (null, null, null, null);
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jToken["data"].ToString());

            if (!data.ContainsKey("route") || !data.ContainsKey("method"))
                return (null, null, null, null);

            // Disabling local assignment warning
            #pragma warning disable S1854
            var route = data["route"].Replace("{api-key}", provider.Key);
            data.Remove("route");
            var method = data["method"];
            data.Remove("method");
            var token = data.ContainsKey("token") ? data["token"] : null;
            if (token != null)
                data.Remove("token");
            #pragma warning restore S1854  
            return (provider.Url + route, method, data, token);
        }
    }
}