using System;
using System.Linq;
using System.Text.Json;

using area.Contexts;
using area.Models.About;
using area.Repositories.Service;
using area.Repositories.Widget;
using area.Repositories.WidgetParam;

namespace area.Business.About
{
    public class AboutBusinessLogic : IAboutBusinessLogic
    {
		private readonly IServiceRepository _serviceRepository;
		private readonly IWidgetRepository _widgetRepository;
		private readonly IWidgetParamRepository _widgetParamRepository;

		public AboutBusinessLogic(AreaContext context)
		{
			_serviceRepository = new ServiceRepository(context);
			_widgetRepository = new WidgetRepository(context);
			_widgetParamRepository = new WidgetParamRepository(context);
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
			var servicesEnumerable = _serviceRepository.GetAllServices();
			return servicesEnumerable.Select(service => new AboutServiceModel
			{
				Name = service.Name,
				Widgets = _widgetRepository.GetWidgetsByServiceId(service.Id, 0, 100)
					.Select(widget => new AboutWidgetModel
					{
						Name = widget.Name,
						Params = _widgetParamRepository.GetWidgetParamByWidgetId(widget.Id).Param
					}).ToArray()
			}).ToArray();
		}
    }
}