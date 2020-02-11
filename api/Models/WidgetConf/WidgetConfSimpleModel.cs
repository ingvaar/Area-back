using System;
using area.Models.Widget;

namespace area.Models.WidgetConf
{
	public abstract class WidgetConfSimpleModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int WidgetId { get; set; }
		public string Conf { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
