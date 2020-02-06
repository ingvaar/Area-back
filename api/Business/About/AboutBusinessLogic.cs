using System;
using System.Linq;
using System.Text.Json;

using area.Contexts;
using area.Models.About;
using area.Repositories.Service;

namespace area.Business.About
{
    public class AboutBusinessLogic : IAboutBusinessLogic
    {
		private readonly IServiceRepository _service;

		public AboutBusinessLogic(AreaContext context)
		{
			_service = new ServiceRepository(context);
		}

		public string About()
		{
			return JsonSerializer.Serialize(GetModel());
		}

		private AboutModel GetModel()
		{
			return new AboutModel {Client = GetClient(), Server = GetServer()};
		}

		private static AboutClientModel GetClient()
		{
			return new AboutClientModel {Host = "127.0.0.1"};
		}

		private AboutServerModel GetServer()
		{
			return new AboutServerModel
			{
				CurrentTime = (int) DateTimeOffset.UtcNow.ToUnixTimeSeconds(), Services = GetServices()
			};
		}

		private AboutServiceModel[] GetServices()
		{
			var services = _service.GetAllServices();
			return services.Select(service => new AboutServiceModel {Name = service.Name}).ToArray();
		}
    }
}