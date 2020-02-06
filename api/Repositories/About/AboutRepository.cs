using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using area.Configuration;
using area.Contexts;
using area.Models.About;
using Microsoft.Extensions.Options;

namespace area.Repositories.About
{
	public class AboutRepository : IAboutRepository
	{
		private AreaContext _context;
		private readonly AppSettings _appSettings;

		public AboutRepository(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public string About()
		{
			return JsonSerializer.Serialize(GetModel());
		}

		private AboutModel GetModel()
		{
			var model = new AboutModel();

			model.Client = GetClient();
			model.Server = GetServer();
			return model;
		}

		private AboutClientModel GetClient()
		{
			var model = new AboutClientModel();

			model.Host = "127.0.0.1";
			return model;
		}

		private AboutServerModel GetServer()
		{
			var model = new AboutServerModel();

			model.CurrentTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			model.Services = GetServices();
			return model;
		}

		private AboutServiceModel[] GetServices()
		{
			var services = _context.Provider
				.OrderBy(p => p.Id)
				.ToArray();
			List<AboutServiceModel> results = new List<AboutServiceModel>();
			foreach (var service in services)
			{
				var aboutSer = new AboutServiceModel();
				aboutSer.Name = service.Name;
				results.Add(aboutSer);
			}
			return results.ToArray();
		}
	}
}
