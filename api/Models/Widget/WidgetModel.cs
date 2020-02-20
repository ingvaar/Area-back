using System.ComponentModel.DataAnnotations.Schema;
using area.Models.Service;
using area.Models.WidgetParam;

namespace area.Models.Widget
{
	public class WidgetModel
	{
		public uint Id { get; set; }
		public string Name { get; set; }
		public uint ServiceId { get; set; }
		[ForeignKey("ServiceForeignKey")]
		public ServiceModel Service { get; set; }
		public uint ParamId { get; set; }
		[ForeignKey("WidgetForeignKey")]
		public WidgetParamModel WidgetParam { get; set; }
	}
}
