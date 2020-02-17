using System.ComponentModel.DataAnnotations.Schema;
using area.Models.Service;
using area.Models.WidgetParam;

namespace area.Models.Widget
{
	public partial class WidgetModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ServiceId { get; set; }
		[ForeignKey("ServiceForeignKey")]
		public ServiceModel Service { get; set; }
		public int ParamId { get; set; }
		[ForeignKey("WidgetForeignKey")]
		public WidgetParamModel WidgetParam { get; set; }
	}
}
