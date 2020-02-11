using area.Models.Service;
using area.Models.WidgetParam;

namespace area.Models.Widget
{
	public abstract class WidgetModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ServiceId { get; set; }
		public ServiceModel Service { get; set; }
		public int ParamId { get; set; }
		public WidgetParamModel WidgetParam { get; set; }
	}
}
